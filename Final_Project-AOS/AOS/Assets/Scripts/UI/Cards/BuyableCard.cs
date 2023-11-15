using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuyableCard : MonoBehaviour
{

	public TextMeshProUGUI cardPriceText;
    public Card card;

	public int index;

	public UIShop uiShop;

	public bool useAshCost = true;

	public TextMeshProUGUI cardNameText;
	public TextMeshProUGUI Card_Description;

	// Start is called before the first frame update
	void Start()
    {

		uiShop = FindAnyObjectByType<UIShop>();
		card = uiShop.shopCardList[index];
		setPrice();


		cardNameText.text = card.name;
		Card_Description.text = card.description;
	}

   

	void setPrice()
	{
		if (!useAshCost)
		{
			setPriceGold();
		}
		else if (useAshCost)
		{
			setPriceAsh();
		}
	}

	void setPriceGold()
	{
		cardPriceText.text = card.GetGoldCost().ToString();

	}

	void setPriceAsh()
	{
		cardPriceText.text = card.GetAshCost().ToString();

	}
}
