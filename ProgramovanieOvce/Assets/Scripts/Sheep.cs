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

	[SerializeField] // rest time is the time before looping again
	float restTimeMin = 1;
	[SerializeField]
	float restTimeMax = 2;

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
		Debug.Log("papam travu");
		yield return new WaitForSeconds(1f);
		Debug.Log("dojedla som");
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
			yield return new WaitForSeconds(randomWaitTime);

			stopWalking();
			StartCoroutine(EatingCorot());
		}
	}

	void Start()
	{

		StartCoroutine(MainCorot());

	}
}
