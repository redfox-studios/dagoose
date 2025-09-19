using Unity.VisualScripting;
using UnityEngine;

public class Test8 : MonoBehaviour
{
	[SerializeField]
	string myName = "Miso";
	[SerializeField]
	int age = 16;

	bool wrongAgeEntered = true;

	void printAgeSpecificString()
	{
		if (age >= 0 && age <= 12)
		{
			Debug.Log($"Hello {myName}, your age is {age}");
			Debug.Log("Si dieta");

		}
		else if (age >= 13 && age <= 17)
		{
			Debug.Log($"Hello {myName}, your age is {age}");
			Debug.Log("Si pubertak");
		}
		else
		{
			Debug.Log($"Hello {myName}, your age is {age}");
			Debug.Log("Si dospely");
		}
	}

	void Update()
	{
		if (wrongAgeEntered == true)
		{
			if (age < 0 || age > 100)
			{
				Debug.LogWarning("Wrong age entered");
			}
			else
			{
				Debug.Log("age je dobry");
				wrongAgeEntered = false;
				printAgeSpecificString();
			}

		}

	}
}

/*
void Update()
	{
		//if (wrongAgeEntered == true)
		if (wrongAgeEntered == false)
		{
			return;
		}

		if (age < 0 || age > 100)
		{
			Debug.LogWarning("Wrong age entered");
		}
		else
		{
			Debug.Log("age je dobry");
			wrongAgeEntered = false;
			printAgeSpecificString();
		}
	}
*/
