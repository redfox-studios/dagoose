using UnityEngine;

public class Test4 : MonoBehaviour
{

	int printManyTimes(int myValue, string myText)
	{
		for (int i = myValue; i > 0; i--)
		{
			Debug.Log(myText);
		}

		return 0;
	}

	void Start()
	{
		int myValue = 6;

		string myText = "hello world";

		printManyTimes(myValue, myText);

	}
}
