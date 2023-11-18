using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShop : MonoBehaviour
{
	public List<Card> shopCardList;
	public PlayerSaveStats playerSaveStats;
	public UICardManager cardManager;
	public bool randomizeSet = false;

	public List<Card> possibleCards;

	public SaveLoad saveLoad;


	void Awake()
	{
		playerSaveStats = FindAnyObjectByType<PlayerSaveStats>();
		cardManager = FindAnyObjectByType<UICardManager>();
		saveLoad = FindAnyObjectByType<SaveLoad>();

		if (randomizeSet)
		{
			randomizeCards();
		}

	}


	void randomizeCards()
	{
		List<int> usedIndexes = new List<int>();
		System.Random random = new System.Random();

		for (int i = 5; i < 9; i++)
		{
			int randomIndex = random.Next(0, possibleCards.Count);

			while (usedIndexes.Contains(randomIndex))
			{
				randomIndex = random.Next(0, possibleCards.Count);
			}

			shopCardList.Insert(i, possibleCards[randomIndex]);
			usedIndexes.Add(randomIndex);
		}

	}

	public void buyCard(int cardIndex)
	{
		saveLoad.SavePlayer();

		Card selectedCard = shopCardList[cardIndex];
		int cardCost = selectedCard.GetGoldCost();
		Debug.Log("Button Pressed");
		if (playerSaveStats != null)
		{
			if (playerSaveStats.totalShells >= cardCost)
			{
				playerSaveStats.removeShells(cardCost);
				cardManager.addCardToInventory(selectedCard);

				Debug.Log("You bought " + selectedCard.GetName());
			}
			else
			{
				Debug.Log("Not enough shells to buy " + selectedCard.GetName());
			}
		}
		



	}

	public void buyCardWithAshes(int cardIndex)
	{
		saveLoad.SavePlayer();

		Card selectedCard = shopCardList[cardIndex];
		int cardCost = selectedCard.GetAshCost();
		Debug.Log("Button Pressed");
		if (playerSaveStats != null)
		{
			if (playerSaveStats.totalAshes >= cardCost)
			{
				playerSaveStats.removeAshes(cardCost);
				cardManager.addCardToInventory(selectedCard);

				Debug.Log("You bought " + selectedCard.GetName());
			}
			else
			{
				Debug.Log("Not enough ashes to buy " + selectedCard.GetName());
			}
		}




	}



}
