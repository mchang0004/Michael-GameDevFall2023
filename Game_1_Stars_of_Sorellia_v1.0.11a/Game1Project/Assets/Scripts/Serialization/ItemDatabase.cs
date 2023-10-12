using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Scriptable object/ItemDatabase")]
public class ItemDatabase : ScriptableObject
{
	public List<Item> items;

	public Item GetItemByName(string itemName)
	{
		foreach (Item item in items)
		{
			if (item.itemName == itemName)
			{
				return item;
			}
		}
		return null; 
	}
}
