using UnityEngine;

public class Test1 : MonoBehaviour
{
	Vector2 generateRandomVector()
	{
		Vector2 myVector;

		float randomX = Random.Range(-10f, 10f);
		float randomY = Random.Range(-10f, 10f);

		myVector.x = randomX;
		myVector.y = randomY;

		return myVector.normalized;
	}

	private void Start()
	{
		Vector2 myVector = generateRandomVector();

		Debug.Log(myVector);
	}
}
