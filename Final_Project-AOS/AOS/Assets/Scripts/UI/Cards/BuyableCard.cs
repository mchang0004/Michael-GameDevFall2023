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

	// Start is called before the first frame update
	void Start()
    {
		uiShop = FindAnyObjectByType<UIShop>();
		card = uiShop.shopCardList[index];
		setPriceGold();
		setPriceAsh();
	}

    // Update is called once per frame
    void Update()
    {
        
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
