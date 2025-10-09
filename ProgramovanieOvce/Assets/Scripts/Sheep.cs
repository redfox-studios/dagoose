using UnityEngine;
using System.Collections;
// using System.Numerics; - FUCK YOU, WHY DID YOU EVEN PUT YOURSELF AUTOMATICALLY HERE
using System.Globalization;

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

	[Header("Move time Parameters")]
	[SerializeField] // move time, okay, maybe it was actually useful for something (originally restTime)
	float moveTimeMin = 1;
	[SerializeField]
	float moveTimeMax = 2;

	[Header("Eating Parameters")]
	[SerializeField] // eat time
	float eatTimeMin = 2;
	[SerializeField]
	float eatTimeMax = 3;

	// [SerializeField]
	// int chanceToEat = 30; // (x / 100) - not needed since im doing it another way

	// -*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-

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

	// -*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-

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

	// -*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-

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
		while (true)
		{
			// variables
			float randomWaitTime = returnRandomWaitTime();
			float randomMoveTime = returnRandomMoveTime();

			// -*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-

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

	// -*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-

	void Start()
	{
		StartCoroutine(MainCorot());
	}
}
