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


	Vector2 returnRandomVector()
	{
		float randomX = Random.Range(-1f, 1f);
		float randomY = Random.Range(-1f, 1f);

		direction.x = randomX;
		direction.y = randomY;

		Vector2 normalizedDirection = direction.normalized;

		return normalizedDirection;
	}

	float returnRandomWaitTime()
	{
		float randomWaitTime = Random.Range(waitTimeMin, waitTimeMax);
		randomWaitTime = Mathf.Round(randomWaitTime * 100) / 100; // keep only 2 decimal numbers for randomWaitTime

		return randomWaitTime;
	}

	float returnRandomRestTime()
	{
		float randomRestTime = Random.Range(restTimeMin, restTimeMax);
		randomRestTime = Mathf.Round(randomRestTime * 100) / 100; // keep only 2 decimal numbers for randomWaitTime

		return randomRestTime;
	}

	// preco pouzivame coroutines?
	// Je to preto lebo wait time nemozeme pouzivat v normalnych funkciach
	// to by potom cely engine "afkoval"
	IEnumerator MainCorot()
	{
		while (true) {

			// variables
			float randomWaitTime = returnRandomWaitTime();
			Vector2 normalizedDirection = returnRandomVector();

			Debug.Log("Before wait. Waiting: " + randomWaitTime + " seconds.");
			yield return new WaitForSeconds(randomWaitTime);

			rb.linearVelocity = normalizedDirection * speed;
		}
	}

	void Start()
	{

		StartCoroutine(MainCorot());

	}
}
