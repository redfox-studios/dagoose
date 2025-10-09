using UnityEngine;
using System.Collections;

public class Sheep : MonoBehaviour
{
	[Header("Main")]
	[SerializeField]
	Rigidbody2D rb;

	[Header("Movement parameters")]
	[SerializeField]
	private Vector2 direction;
	[SerializeField]
	float speed = 0.5f;

	[Header("Wait time Parameters")]
	[SerializeField] // wait time is the time before moving the sheep after starting the coroutine (MainCorot)
	float waitTimeMin = 1;
	[SerializeField]
	float waitTimeMax = 2;

	[Header("Rest time Parameters")]
	[SerializeField] // rest time, okay, maybe it was actually useful for something
	float restTimeMin = 1;
	[SerializeField]
	float restTimeMax = 2;

	[Header("Eating Parameters")]
	[SerializeField] // eat time
	float eatTimeMin = 2;
	[SerializeField]
	float eatTimeMax = 3;
	[SerializeField]
	int chanceToEat = 30; // (x / 100)

	// -*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-

	float returnRandomWaitTime()
	{
		float randomWaitTime = Random.Range(waitTimeMin, waitTimeMax);
		randomWaitTime = Mathf.Round(randomWaitTime * 100) / 100; // round to 2 decimal numbers

		return randomWaitTime;
	}

	float returnRandomRestTime()
	{
		float randomRestTime = Random.Range(restTimeMin, restTimeMax);
		randomRestTime = Mathf.Round(randomRestTime * 100) / 100; // round to 2 decimal numbers

		return randomRestTime;
	}

	float returnRandomEatTime()
	{
		float randomEatTime = Random.Range(eatTimeMin, eatTimeMax);
		randomEatTime = Mathf.Round(randomEatTime * 100) / 100; // round to 2 decimal numbers

		return randomEatTime;
	}

	// -*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-

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

	Vector2 stopWalking()
	{
		return rb.linearVelocity = new Vector2(0, 0);
	}

	// -*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-

	// preco pouzivame coroutines?
	// Je to preto lebo wait time (WaitForSeconds) nemozeme pouzivat v normalnych funkciach
	// to by potom cely engine "afkoval"

	IEnumerator EatingCorot()
	{
		float randomEatTime = returnRandomEatTime();

		stopWalking();
		Debug.Log("Eating for " + randomEatTime + " seconds.");
		yield return new WaitForSeconds(randomEatTime);
		Debug.Log("finished eating");

		yield break;	// finally found out how to do it
				// idk why, but my intellisense doesnt work. Unity sucks imo
	}

	IEnumerator MainCorot()
	{
		while (true) {

			// variables
			float randomWaitTime = returnRandomWaitTime();
			float randomRestTime = returnRandomRestTime();
			Vector2 normalizedDirection = returnRandomVector();

			// -*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-
			
			// wait
			Debug.Log("Waiting for " + randomWaitTime + " seconds.");
			yield return new WaitForSeconds(randomWaitTime);

			// move
			rb.linearVelocity = normalizedDirection * speed;

			// rest
			Debug.Log("Resting for " + randomRestTime + " seconds.");
			yield return new WaitForSeconds(randomRestTime);

			StartCoroutine(EatingCorot());
		}
	}

	// -*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-

	void Start()
	{

		StartCoroutine(MainCorot());

	}
}
