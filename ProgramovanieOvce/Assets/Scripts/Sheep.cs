using UnityEngine;
using System.Collections;

public class Sheep : MonoBehaviour
{

	[SerializeField]
	Rigidbody2D rb;
	[SerializeField]
	private Vector2 direction;
	[SerializeField]
	float speed;

	Vector2 returnRandomVector()
	{
		float randomX = Random.Range(-1f, 1f);
		float randomY = Random.Range(-1f, 1f);

		direction.x = randomX;
		direction.y = randomY;

		Vector2 normalizedDirection = direction.normalized;

		return normalizedDirection;
	}

	void Start()
	{
		Vector2 normalizedDirection = returnRandomVector();

		rb.linearVelocity = normalizedDirection * speed;
	}
}
