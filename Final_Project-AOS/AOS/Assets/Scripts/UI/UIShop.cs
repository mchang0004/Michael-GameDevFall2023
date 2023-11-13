using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShop : MonoBehaviour
{
	public List<Card> shopCardList;
	public PlayerStats playerStats;
	public UICardManager cardManager;

	void Start()
	{
		playerStats = FindAnyObjectByType<PlayerStats>();
		cardManager = FindAnyObjectByType<UICardManager>();

	}



	



	public void buyCard(int cardIndex)
	{

		Card selectedCard = shopCardList[cardIndex];
		int cardCost = selectedCard.GetGoldCost();
		Debug.Log("Button Pressed");
		if (playerStats != null)
		{
			if (playerStats.totalShells >= cardCost)
			{
				playerStats.removeShells(cardCost);
				cardManager.addCardToInventory(selectedCard);

				Debug.Log("You bought " + selectedCard.GetName());
			}
			else
			{
				Debug.Log("Not enough shells to buy " + selectedCard.GetName());
			}
		}
		



	}

	


	
}
