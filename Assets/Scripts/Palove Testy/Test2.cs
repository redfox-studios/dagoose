using System;
using UnityEngine;

public class Test2 : MonoBehaviour
{
	int addTwoNumbers(int myInteger1, int myInteger2)
	{
		int total = myInteger1 + myInteger2;

		return total;
	}

	void Start()
	{
		int myInteger1 = 10;
		int myInteger2 = 8;

		addTwoNumbers(myInteger1, myInteger2);

		int total = addTwoNumbers(myInteger1, myInteger2);

		Debug.Log(total);
	}
}
