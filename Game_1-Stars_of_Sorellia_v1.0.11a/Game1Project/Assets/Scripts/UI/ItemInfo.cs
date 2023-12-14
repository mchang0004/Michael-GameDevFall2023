using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ItemInfo : MonoBehaviour
{

	public TextMeshProUGUI itemNameText;
	public TextMeshProUGUI itemInfoText;
	public TextMeshProUGUI itemStatText;
	public TextMeshProUGUI itemGoldText;

	public Item currentItem;

	public void ShowItemInfo()
	{
		if (currentItem != null)
		{
			itemNameText.text = currentItem.itemName;
			itemInfoText.text = currentItem.itemInfo;
			itemStatText.text = currentItem.itemStat;
			itemGoldText.text = currentItem.itemGold + " Gold";

			if (currentItem.GetItemType() == ItemType.Weapon)
			{
				
				itemStatText.text = currentItem.attackDamage.ToString() + " Damage";
			} else if (currentItem.GetItemType() == ItemType.Consumable)
			{
				
				itemStatText.text = currentItem.HPBonus.ToString() + " Health";
			} else if (currentItem.GetItemType() == ItemType.Armor)
			{
				//change itemStat to armorBonus later
				itemStatText.text = currentItem.itemStat.ToString() + " ";
			} else
			{
				
				itemStatText.text = currentItem.itemStat.ToString() + " ";
			}

		}


		gameObject.SetActive(true);
	}

	public void HideItemInfo()
	{
		gameObject.SetActive(false);
	}

	void Update()
	{
		ShowItemInfo();
	}
}
