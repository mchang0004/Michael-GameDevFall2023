using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum ItemType
{ 
	Armor,
	Accessory,
	Boots,
	Consumable,
	Greave,
	Helmet,
	Weapon,
	QuestItem
}

public enum ActionType
{
	//if the item isBroken then use will repair the item
	Use
}

[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item : ScriptableObject
{
	[Header("General")]
	public Sprite image;
	public ItemType itemType; // Update the field type to be of type ItemType
	public ActionType actionType;
	public string itemName;
	public string itemInfo;
	public string itemStat;
	public string itemGold; 

	[Header(" ")]
	public bool stackable = true;
	[field: SerializeField]
	public int MaxStackSize { get; set; } = 1;
	public bool isBroken = false;
	public float itemCurrentHP;                //if HP = 0, the item isBroken = true;
	public float itemMaxHP;             //if maxHP = 0, the item has infinite durability
	public bool isCollectable = true; // Is the item collectable or not

	[Header("Market")]
	public float basePrice = 1;
	public float baseSellPrice = 0;
	public float marketQuantity = 0; //market quantity goes up or down depending on how many times the player has bought or sold the item. This will help determine the value over time in game. 

	[Header("Consumable")]
	public float HPBonus = 0;
	//public Effect effects[]; //create a script for effects

	[Header("Armor")]
	public float armorBonus = 0;
	public bool isEquipped = false;

	[Header("Weapon")]
	public float attackDamage;
	public float attackSpeed;
	public float attackRange;
	public float attackPointOffset;
	public bool allowRepeatSwing;







	public ItemType GetItemType()
	{
		return itemType;
	}

	public void SetBroken()
	{
		isBroken = true;
	}

	public void SetUnbroken()
	{
		isBroken = false;
	}


}
