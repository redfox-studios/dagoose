using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	[Header("Money Settings")]
	[SerializeField] int startingMoney = 0;
	private int currentMoney;

	[Header("Goose Settings")]
	[SerializeField] GameObject goosePrefab;
	[SerializeField] int gooseCost = 20;
	[SerializeField] Transform gooseSpawnArea; // Optional: define spawn bounds

	public int CurrentMoney => currentMoney;
	public int GooseCost => gooseCost;

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	void Start()
	{
		currentMoney = startingMoney;
		UpdateUI();
	}

	public void AddMoney(int amount)
	{
		currentMoney += amount;
		UpdateUI();
	}

	public bool CanAffordGoose()
	{
		return currentMoney >= gooseCost;
	}

	public void BuyGoose()
	{
		if (!CanAffordGoose())
		{
			Debug.Log("Not enough money!");
			return;
		}

		currentMoney -= gooseCost;
		UpdateUI();

		SpawnGoose();
	}

	void SpawnGoose()
	{
		if (goosePrefab == null)
		{
			Debug.LogError("Goose prefab not assigned!");
			return;
		}

		Vector3 spawnPos = Vector3.zero;

		if (gooseSpawnArea != null)
		{
			// Spawn near the spawn area
			spawnPos = gooseSpawnArea.position + new Vector3(
				Random.Range(-1f, 1f),
				Random.Range(-1f, 1f),
				0f
			);
		}
		else
		{
			// Random spawn
			spawnPos = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0f);
		}

		GameObject newGoose = Instantiate(goosePrefab, spawnPos, Quaternion.identity);

		Transform collection = GameObject.Find("GooseCollection")?.transform;
		if (collection != null)
			newGoose.transform.SetParent(collection);

		Debug.Log("Spawned new goose!");
	}

	void UpdateUI()
	{
		if (UIManager.Instance != null)
		{
			UIManager.Instance.UpdateMoneyDisplay(currentMoney);
		}
	}
}
