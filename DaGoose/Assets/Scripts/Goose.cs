using UnityEngine;
using System.Collections;
// using System.Numerics; - FUCK YOU, WHY DID YOU EVEN PUT YOURSELF AUTOMATICALLY HERE
using System.Globalization;

public class Goose : MonoBehaviour
{
	[Header("Main")]
	[SerializeField] Rigidbody2D rb;
	[SerializeField] public Animator Visuals;       // Usage example: Visuals.SetBool("eat", false); - Currently have: eat, walk, idle
	[SerializeField] int gooseType = 1;     // ZATIAL NA TOM NEBUDEM PRACOVAT,
						// ALE TYPES SU:
						// 0 = secret (rainbow :O)
						// 1 = normal
						// 2 = evil

	[Header("Movement parameters")] // movement (obviously)
	[SerializeField] private Vector2 direction;
	[SerializeField] float speed = 0.5f;

	[Header("Wait time Parameters")] // wait time is the time before moving the goose after starting the coroutine (MainCorot)
	[SerializeField] float waitTimeMin = 1;
	[SerializeField] float waitTimeMax = 2;

	[Header("Move time Parameters")] // move time, okay, maybe it was actually useful for something (originally restTime)
	[SerializeField] float moveTimeMin = 1;
	[SerializeField] float moveTimeMax = 2;

	[Header("Eating Parameters")] // eat time
	[SerializeField] float eatTimeMin = 2;
	[SerializeField] float eatTimeMax = 3;

	float returnRandomWaitTime()
	{
		float randomWaitTime = Random.Range(waitTimeMin, waitTimeMax);
		randomWaitTime = Mathf.Round(randomWaitTime * 100) / 100; // round to 2 decimal numbers

		return randomWaitTime;
	}

	float returnRandomMoveTime()
	{
		float randomMoveTime = Random.Range(moveTimeMin, moveTimeMax);
		randomMoveTime = Mathf.Round(randomMoveTime * 100) / 100; // round to 2 decimal numbers

		return randomMoveTime;
	}

	float returnRandomEatTime()
	{
		float randomEatTime = Random.Range(eatTimeMin, eatTimeMax);
		randomEatTime = Mathf.Round(randomEatTime * 100) / 100; // round to 2 decimal numbers

		return randomEatTime;
	}

	Vector2 returnRandomVector()
	{
		float randomX = Random.Range(-1f, 1f);
		randomX = Mathf.Round(randomX * 100) / 100; // round to 2 decimal numbers

		float randomY = Random.Range(-1f, 1f);
		randomY = Mathf.Round(randomY * 100) / 100; // round to 2 decimal numbers

		direction.x = randomX;
		direction.y = randomY;

		Vector2 normalizedDirection = direction.normalized;

		return normalizedDirection;
	}

	Vector2 startWalking()
	{
		Vector2 normalizedDirection = returnRandomVector();
		return rb.linearVelocity = normalizedDirection * speed;
	}

	Vector2 stopWalking()
	{
		return rb.linearVelocity = new Vector2(0, 0);
	}

	IEnumerator EatingCorot()
	{
		float randomEatTime = returnRandomEatTime();

		stopWalking();
		Debug.Log("Eating for " + randomEatTime + " seconds.");
		yield return new WaitForSeconds(randomEatTime);
		Debug.Log("finished eating");

		yield break;    // finally found out how to do it
				// idk why, but my intellisense doesnt work. Unity sucks imo, let's switch to UE
	}

	IEnumerator MainCorot()
	{
		while (true)
		{
			// variables
			float randomWaitTime = returnRandomWaitTime();
			float randomMoveTime = returnRandomMoveTime();

			// wait
			Debug.Log("Waiting for " + randomWaitTime + " seconds.");
			yield return new WaitForSeconds(randomWaitTime);

			// move
			startWalking();
			Debug.Log("Moving for " + randomMoveTime + " seconds.");
			yield return new WaitForSeconds(randomMoveTime);

			// stop walking
			stopWalking();

			// eat
			yield return EatingCorot();
		}
	}

	void Start()
	{
		StartCoroutine(MainCorot());

		switch (gooseType)
		{
			case 1:
				Debug.Log("This is a normal goose.");
				break;
			case 2:
				Debug.Log("This is an evil goose.");
				break;
			default:
				Debug.Log("Error: Invalid goose type.");
				break;
		}
	}
}

// |==============================================================================================================================================================|
// |============================================================== [ POZNAMKY, A INE VECI ] ======================================================================|
// |==============================================================================================================================================================|

/*
Poznamky:

- preco pouzivame coroutines?
	- Je to preto lebo wait time (WaitForSeconds) nemozeme pouzivat v normalnych funkciach
	- to by potom cely engine "afkoval"

- Je tu extremne velky bordel a bolo by dobre to tu upratat
*/

/*
Ulohy:

Úloha 1 - random pohyb keď sa spustí hra
V skripte v metóde Start si najskôr získaj ľubovoľným spôsobom odkaz na to rigidbody. Sprav, že keď sa zapne hra, nech sa ovca hýbe v náhodnom smere donekonečna, pomocou rigidbody.
Tip: Vytvor, si Vector2, ktorého x aj y sú obe náhodné.

Úloha 1.5 (bonus) - pohyb s konzistentnou rýchlosťou
Pohyb z predošlej úlohy bude niekedy rýchlejší a niekedy pomalší. Skús to opraviť.
Tip: Dôvod pre toto zvláštne správanie súvisí s dĺžkou vektorov. Dĺžku vektoru viete nastaviť na 1 pomocou normalizácie

Úloha 2 - random pohyb s čakaním
Pomocou Coroutine sprav, že keď sa hra spustí, tak najskôr sa čaká náhodný čas a až potom sa ovcu rozhýbe. Nech je ten náhodný čas vždy v rozmedzí 2 premenných, ktoré sa dajú nastaviť priamo z Unity Editoru: walkTimeMin a walkTimeMax.

Úloha 3 - pohyb dookola a ohrada
Sprav, aby sa to vyššie dialo v korutine dookola. Čiže dookola sa najskôr čaká a potom udelí náhodnú rýchlosť.
Aby ti ovca neodkráčala preč z obrazovky, jej sprav ohradu a zaisti pomocou colliderov, aby z nej nevedela vyjsť.

Úloha 4 - striedanie pohybu a oddychu
Pohyb ovce je teraz už ok, avšak ovca sa neustále hýbe, nikdy si neoddýchne. Sprav, aby sa ovca dookola náhodnú chvíľu hýbala a potom sa náhodnú chvíľu nehýbala.
Na toto budeš potrebovať ďalšie 2 premenné nastaviteľné z editoru:  restTimeMin a restTimeMax.

Úloha 5 - jedenie trávy
Sprav, že keď ovca oddychuje, nech je trávu. Pri jedení trávy sa najskôr pozícia jej hlavy posunie nadol, potom sa 1 sekundu čaká a na záver sa hlava posunie naspäť nahor.
Keďže tu opäť potrebujeme čakať, a to 1 sekundu, vytvorte si na toto ďaľšiu korutinu. V tejto korutine opäť budeme čakať pomocou yield return new WaitForSeconds(1f)

Úloha 6 - podmienečné jedenie trávy
Teraz ovca už pri každom oddychu je trávu. Avšak sprav to tak, aby nejedla trávu pri každom oddychu, no len niekedy, náhodne. Využi na to novú premennú chanceToEat, ktorá bude opäť nastaviteľná z editoru.
Tá premenná má kvázi odpovedať na otázku “aká je šanca počas oddychu ovce, že bude jesť trávu”.

Úloha 7 - spawn zjedenej trávy
Keď ovca zje trávu, tráva na tom mieste nezmizne. Poďme to opraviť.
Vytvor si nový gameobjekt s názvom EatenGrass, ktorému dáš vizuál diery. Stačí, ak to bude hnedý štvorec
Späť v skripte pre ovcu sprav, aby keď ovca dokončí jedenie, spawnla pod sebou nový výskyt (instance) EatenGrassu.

Úloha 8 - dorastenie trávy
Ovca teraz po sebe veselo zanecháva miesta so zjedenou trávou.  Ale chceli by sme, aby tráva časom dorástla. Teda, aby sa gameObjekt EatenGrass o nejaký čas zničil.
Na prefab EatenGrass teda dajte nový skript, ktorý náhodný čas čaká a následne ten gameobjekt zničí.
Na ničenie gameobjektov sa používa Destroy(), avšak má jednu fintu, vďaka ktorej nemusíme to náhodné čakanie robiť cez korutiny.

Úloha 9 - ostrihanie ovce a dorastenie vlny
Ak ste doposiaľ nemali pocit, že programujeme klon ovce z Minecraftu, tak teraz ho už budete mať. Spravíme si, že keď sa na ovcu klikne, zmizne jej vlna a keď sa naje, tak sa jej vlna znovu objaví.
Ako spravíte zmiznutie vlny, nechám na vás.
Ako však detekujeme, keď sa na ovcu klikne? Prísť na to bude vaša úloha.
Avšak vieme využiť fakt, že každý Collider 2D (okrem EdgeCollider) obsahuje metódu, ktorá vráti, či sa nejaký bod nachádza vo vnútri toho collidera
Okrem tohoto spravte, aby sa vlna obnovila, keď ovca doje.

Úloha 10 - nech ovca nevie zjesť zjedenú trávu
Keď ovca stojí na gameobjekte EatenGrass, nemala by sa vedieť najesť - veď pod ňou tráva nie je.
V úlohe 6 ste si zrejme spravili pomocou podmienky (if) to, aby ovca len niekedy jedla. Teraz tú podmienku doplňte o ďaľší check - či sa ovca dotýka EatenGrassu.
Na zistenie, či sa ovca dotýka EatenGrassu, potrebuje aj ovca a aj EatenGrass collider - totiž, kolízie 2 colliderov vieme v kóde zaznamenať pomocou metód OnCollisionX alebo OnTriggerX.
Sprav to tak, aby sa kolízie s EatenGrass v ovci detekovali, avšak aby ovca vedela cez EatenGrass prechádzať.

Keď toto všetko spravíš, naduplikuj si ovce a budeš mať stádo. A to je všetko!
*/

/*
Poznamky k uloham:

Vseobecne
	- nemam ovcu, ale mam hus. A ta hus je ako jeden prefab ktory ma animacie a je to pixelart

Úloha 7 - spawn zjedenej trávy
	- Mam uz hotovu EatenGrass ako prefab
	-

Úloha 9 - ostrihanie ovce a dorastenie vlny
	- Ako som uz spomenul, nemam ovcu ale mam hus. ALE, vymyslel som nieco namiesto tej vlny.
	  Hus niekedy vyprdne vajce, ALE aby to bolo zaujimavejsie tak toto vajce bude gambling,
	  a su tu rozne typy vajec - Default, Gold, Diamond, Ruby, Rainbow
*/
