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

	[Header("Delay Parameters")]
	[SerializeField] // wait time is the time before moving the sheep after starting the coroutine (MainCorot)
	float waitTimeMin = 1;
	[SerializeField]
	float waitTimeMax = 2;

	[SerializeField] // eat time
	float eatTimeMin = 2;
	[SerializeField]
	float eatTimeMax = 3;

	// -*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-

	float returnRandomWaitTime()
	{
		float randomWaitTime = Random.Range(waitTimeMin, waitTimeMax);
		randomWaitTime = Mathf.Round(randomWaitTime * 100) / 100; // round to 2 decimal numbers

		return randomWaitTime;
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
			Vector2 normalizedDirection = returnRandomVector();

			// -*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-
			
			// wait
			Debug.Log("Waiting for " + randomWaitTime + " seconds.");
			yield return new WaitForSeconds(randomWaitTime);

			// move
			rb.linearVelocity = normalizedDirection * speed;

			// wait 2
			Debug.Log("Waiting again for " + randomWaitTime + " seconds.");
			yield return new WaitForSeconds(randomWaitTime);

			StartCoroutine(EatingCorot());
		}
	}

	void Start()
	{

		StartCoroutine(MainCorot());

	}
}
