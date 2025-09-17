using UnityEngine;
using System.Collections;

public class Sheep : MonoBehaviour
{

	[SerializeField]
	Rigidbody2D rb;
	[SerializeField]
	private Vector2 direction;

	void Start()
	{
		float RandomX = Random.Range(-1f, 1f);
		float RandomY = Random.Range(-1f, 1f);

		direction = new Vector2(RandomX, RandomY);
		rb.linearVelocity = direction;

		StartCoroutine(MyCorot());
	}

	void Update()
	{
		
	}

	IEnumerator MyCorot()
	{
		Debug.Log("hi");
		yield return new WaitForSeconds(2f);
		Debug.Log("bye");
	}
}
