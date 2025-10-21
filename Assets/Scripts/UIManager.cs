using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
	public static UIManager Instance;

	[Header("UI References")]
	[SerializeField] TextMeshProUGUI moneyText;
	[SerializeField] Button buyGooseButton;
	[SerializeField] TextMeshProUGUI buttonText;

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
		if (buyGooseButton != null)
		{
			buyGooseButton.onClick.AddListener(OnBuyGooseClicked);
		}

		if (buttonText != null && GameManager.Instance != null)
		{
			buttonText.text = "Buy Goose ($" + GameManager.Instance.GooseCost + ")";
		}
	}

	public void UpdateMoneyDisplay(int amount)
	{
		if (moneyText != null)
		{
			moneyText.text = "$" + amount.ToString();
		}

		UpdateButtonState();
	}

	void UpdateButtonState()
	{
		if (buyGooseButton != null && GameManager.Instance != null)
		{
			buyGooseButton.interactable = GameManager.Instance.CanAffordGoose();
		}
	}

	void OnBuyGooseClicked()
	{
		if (GameManager.Instance != null)
		{
			GameManager.Instance.BuyGoose();
		}
	}
}
