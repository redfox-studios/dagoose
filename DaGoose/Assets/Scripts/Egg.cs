using UnityEngine;

public enum EggType
{
	Default = 0,
	Gold = 1,
	Diamond = 2,
	Ruby = 3,
	Rainbow = 4
}

public class Egg : MonoBehaviour
{
	[Header("Egg Settings")]
	[SerializeField] EggType eggType = EggType.Default;

	private int[] eggValues = { 2, 5, 10, 20, 50 }; // Default, Gold, Diamond, Ruby, Rainbow

	void OnMouseDown()
	{
		CollectEgg();
	}

	void CollectEgg()
	{
		int value = eggValues[(int)eggType];

		if (GameManager.Instance != null)
		{
			GameManager.Instance.AddMoney(value);
			Debug.Log("Collected " + eggType + " egg for $" + value);
		}

		Destroy(gameObject);
	}
}
