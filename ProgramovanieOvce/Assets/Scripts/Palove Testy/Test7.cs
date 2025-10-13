using UnityEngine;

public class Test7 : MonoBehaviour
{
	void printDivisible(uint number)
	{
		for (int i = 1; i < number; i++)
		{
			if (number % i == 0)
			{
				Debug.Log($"Cislo {number} je delitelne cislom {i}");
			}
		}
	}

	void Start()
	{
		uint number = 64;

		printDivisible(number);
	}
}
