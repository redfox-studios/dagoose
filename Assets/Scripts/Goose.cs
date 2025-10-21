using UnityEngine;
using System.Collections;

public class Goose : MonoBehaviour
{
	[Header("Main")]
	[SerializeField] Rigidbody2D rb;
	[SerializeField] public Animator Visuals;
	[SerializeField] int gooseType = 1;

	[Header("Audio")]
	[SerializeField] AudioClip honkSound;
	[SerializeField] AudioSource audioSource;

	[Header("Movement Parameters")]
	[SerializeField] private Vector2 direction;
	[SerializeField] float speed = 0.5f;

	[Header("Idle Time Parameters")]
	[SerializeField] float idleTimeMin = 1f;
	[SerializeField] float idleTimeMax = 2f;

	[Header("Move Time Parameters")]
	[SerializeField] float moveTimeMin = 1f;
	[SerializeField] float moveTimeMax = 2f;

	[Header("Eating Parameters")]
	[SerializeField] float eatTimeMin = 2f;
	[SerializeField] float eatTimeMax = 3f;

	[Header("Laying Parameters")]
	[SerializeField] float layChance = 0.3f;	// 30% chance to lay egg after idle
	[SerializeField] GameObject[] eggPrefabs; 	// Assign 5 egg prefabs (Default, Gold, Diamond, Ruby, Rainbow)

	[Header("Eaten Grass")]
	[SerializeField] GameObject eatenGrassPrefab;

	private bool isOnEatenGrass = false;
	private int eatenGrassLayerMask;
	private bool isDragging = false;
	private Vector3 offset;
	private Camera mainCamera;

	void Start()
	{
		StartCoroutine(MainCorot());

		if (audioSource == null)
			audioSource = GetComponent<AudioSource>();

		mainCamera = Camera.main;
		eatenGrassLayerMask = LayerMask.GetMask("EatenGrass");

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

	void OnMouseDown()
	{
		if (honkSound != null && audioSource != null)
		{
			audioSource.PlayOneShot(honkSound);
		}

		isDragging = true;
		Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
		offset = transform.position - new Vector3(mousePos.x, mousePos.y, transform.position.z);
	}

	void OnMouseDrag()
	{
		if (isDragging)
		{
			Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
			transform.position = new Vector3(mousePos.x + offset.x, mousePos.y + offset.y, transform.position.z);
		}
	}

	void OnMouseUp()
	{
		isDragging = false;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("EatenGrass"))
		{
			isOnEatenGrass = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("EatenGrass"))
		{
			isOnEatenGrass = false;
		}
	}

	float GetRandomTime(float min, float max)
	{
		return Mathf.Round(Random.Range(min, max) * 100f) / 100f;
	}

	Vector2 GetRandomDirection()
	{
		float randomX = Mathf.Round(Random.Range(-1f, 1f) * 100f) / 100f;
		float randomY = Mathf.Round(Random.Range(-1f, 1f) * 100f) / 100f;

		direction.x = randomX;
		direction.y = randomY;

		return direction.normalized;
	}

	void StartWalking()
	{
		Vector2 normalizedDirection = GetRandomDirection();
		rb.linearVelocity = normalizedDirection * speed;
		Visuals.SetBool("action-walk", true);
		Visuals.SetBool("action-idle", false);
	}

	void StopWalking()
	{
		rb.linearVelocity = Vector2.zero;
		Visuals.SetBool("action-walk", false);
		Visuals.SetBool("action-idle", true);
	}

	IEnumerator IdleCorot()
	{
		float idleTime = GetRandomTime(idleTimeMin, idleTimeMax);

		StopWalking();
		Debug.Log("Idling for " + idleTime + " seconds.");
		yield return new WaitForSeconds(idleTime);
		Debug.Log("Finished idling");
	}

	IEnumerator EatingCorot()
	{
		if (isOnEatenGrass)
		{
			Debug.Log("Can't eat - standing on eaten grass!");
			yield break;
		}

		float eatTime = GetRandomTime(eatTimeMin, eatTimeMax);

		StopWalking();
		Visuals.SetBool("action-eat", true);
		Debug.Log("Eating for " + eatTime + " seconds.");
		yield return new WaitForSeconds(eatTime);
		Visuals.SetBool("action-eat", false);
		Debug.Log("Finished eating");

		// Spawn eaten grass
		if (eatenGrassPrefab != null)
		{
			GameObject grass = Instantiate(eatenGrassPrefab, transform.position, Quaternion.identity);
			Transform collection = GameObject.Find("EatenGrassCollection")?.transform;

			if (collection != null)
			{
				grass.transform.SetParent(collection);
			}
		}
	}

	IEnumerator LayEggCorot()
	{
		Debug.Log("Laying an egg...");

		// Determine egg type based on chances
		float roll = Random.Range(0f, 100f);
		int eggIndex = 0; // Default

		if (roll < 1f) eggIndex = 4; // Rainbow 1%
		else if (roll < 5f) eggIndex = 3; // Ruby 4%
		else if (roll < 15f) eggIndex = 2; // Diamond 10%
		else if (roll < 40f) eggIndex = 1; // Gold 25%
		else eggIndex = 0; // Default 60%

		if (eggPrefabs != null && eggIndex < eggPrefabs.Length && eggPrefabs[eggIndex] != null)
		{
			GameObject egg = Instantiate(eggPrefabs[eggIndex], transform.position, Quaternion.identity);
			Debug.Log("Laid egg type: " + eggIndex);
		}

		yield return new WaitForSeconds(1f);
	}

	IEnumerator MainCorot()
	{
		while (true)
		{
			// Idle
			yield return IdleCorot();

			// Random chance to lay egg after idle
			if (Random.value < layChance)
			{
				yield return LayEggCorot();
			}

			// Walk
			float moveTime = GetRandomTime(moveTimeMin, moveTimeMax);
			StartWalking();
			Debug.Log("Moving for " + moveTime + " seconds.");
			yield return new WaitForSeconds(moveTime);

			// Eat
			yield return EatingCorot();
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

- future plany do hry:
	- z predanych vajec ziskavas peniaze (vajce predas tak ze na neho klinkes, taktiez kazde vajce ma niaku hodnotu)
	- za tieto peniaze si kupujes nove husy
	-
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
