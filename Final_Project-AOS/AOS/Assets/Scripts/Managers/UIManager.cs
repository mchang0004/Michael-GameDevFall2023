using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
	#region References
	public GameManager gameManager;
	public CardManager cardManager;

	#endregion

	#region Trackers
	private int currentWarding_UI;
	private int currentStability_UI;
	private int currentAshes_UI;
	private int currentLoot_UI;

    private int maxWarding;
    private int maxStability;
    private int maxAshes;
    private int maxLoot;

	private TextMeshProUGUI wardingText;
	private TextMeshProUGUI stabilityText;
	private TextMeshProUGUI ashesText;
	private TextMeshProUGUI lootText;


	//debug:
	private TextMeshProUGUI timerText;
	private TextMeshProUGUI cardsRemainingText;

	#endregion


	void Start()
    {
		gameManager = GameManager.Instance;
		cardManager = GameObject.Find("Card Manager").GetComponent<CardManager>();

		maxWarding = GameManager.defaultMaxWarding;
		maxStability = GameManager.defaultMaxStability;
		maxAshes = GameManager.defaultMaxAshes;
		maxLoot = GameManager.defaultMaxLoot;

		wardingText = GameObject.Find("Warding").GetComponent<TextMeshProUGUI>();
		stabilityText = GameObject.Find("Stability").GetComponent<TextMeshProUGUI>();
		ashesText = GameObject.Find("Ashes").GetComponent<TextMeshProUGUI>();
		lootText = GameObject.Find("Loot").GetComponent<TextMeshProUGUI>();

		//debug:
		timerText = GameObject.Find("Time Since Last Card").GetComponent<TextMeshProUGUI>();
		cardsRemainingText = GameObject.Find("Cards Remaining").GetComponent<TextMeshProUGUI>();

	}

	void Update()
    {
        UpdateUI();
		UpdateDebugUI();
	}



	#region UI Update
	void UpdateUI()
    {

		currentWarding_UI = gameManager.currentWarding;
		currentStability_UI = gameManager.currentStability;
		currentAshes_UI = gameManager.currentAshes;
		currentLoot_UI = gameManager.currentLoot;

		if (TextIsNull())
		{

            if(currentWarding_UI < maxWarding)
            {
				wardingText.text = "Warding: " + currentWarding_UI;
			} 
            else
            {
				wardingText.text = "Warding: Max";
			}

			if (currentStability_UI < maxStability)
			{
				stabilityText.text = "Stability: " + currentStability_UI;
			}
			else
			{
				stabilityText.text = "Stability: Max";
			}

			if (currentAshes_UI < maxAshes)
			{
				ashesText.text = "Ashes: " + currentAshes_UI;
			}
			else
			{
				ashesText.text = "Ashes: Max";
			}

			if (currentLoot_UI < maxLoot)
			{
				lootText.text = "Loot: " + currentLoot_UI;
			}
			else
			{
				lootText.text = "Loot: Max";
			}


		}
	}

	bool TextIsNull()
	{
		if( wardingText == null || stabilityText == null || ashesText == null || lootText == null)
		{
			return false;
		}
		return true;
	}
	#endregion

	#region Card Draw Debug

	void UpdateDebugUI()
	{
		float timeRemaining = Mathf.Max(0f, cardManager.timeBetweenDraws - cardManager.GetTimeSinceLastDraw());
		timeRemaining -= Time.deltaTime;

		if (timerText != null && cardManager.loadedDeck.Count > 0)
		{
			timerText.text = $"Next Draw in: {timeRemaining:F1} seconds";
		} else
		{
            timerText.text = $"Next Draw in: No Cards to Draw";

        }


        int cardsRemaining = cardManager.loadedDeck.Count;

		if (cardsRemainingText != null)
		{
			cardsRemainingText.text = "Cards Remaining: " + cardsRemaining;
		}
	}


	#endregion

}
