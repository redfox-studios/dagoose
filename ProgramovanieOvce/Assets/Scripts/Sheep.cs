using UnityEngine;
using System.Collections;

public class Sheep : MonoBehaviour
{
	[Header("Main")]
	[SerializeField]
	Rigidbody2D rb;

	[Header("Moevement parameters")]
	[SerializeField]
	private Vector2 direction;
	[SerializeField]
	float speed;

	[Header("Delay Parameters")]
	[SerializeField]
	float waitTimeMin;
	[SerializeField]
	float waitTimeMax;
	[SerializeField]
	float restTimeMin;
	[SerializeField]
	float restTimeMax;

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

	// -*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-

	// preco pouzivame coroutines?
	// Je to preto lebo wait time nemozeme pouzivat v normalnych funkciach
	// to by potom cely engine "afkoval"
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

			rb.linearVelocity = normalizedDirection * speed;

			// rest
			Debug.Log("Resting for " + randomRestTime + " seconds.");
			yield return new WaitForSeconds(randomWaitTime);

			rb.linearVelocity = new Vector2(0, 0);
		}
	}

	void Start()
	{

		StartCoroutine(MainCorot());

	}
}
