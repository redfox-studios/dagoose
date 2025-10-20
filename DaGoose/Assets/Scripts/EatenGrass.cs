using UnityEngine;

public class EatenGrass : MonoBehaviour
{
	[Header("Regrow Settings")]
	[SerializeField] float regrowTimeMin = 5f;
	[SerializeField] float regrowTimeMax = 10f;

	void Start()
	{
		float regrowTime = Random.Range(regrowTimeMin, regrowTimeMax);
		Destroy(gameObject, regrowTime);
	}
}
