using UnityEngine;

public class Test5 : MonoBehaviour
{

	int myValue = 6;

	string myText = "hello world";

	int printManyTimes()
	{
		for (int i = myValue; i > 0; i--)
		{
			Debug.Log(myText);
		}

		return 0;
	}

	void Start()
	{
		printManyTimes();

	}
}
