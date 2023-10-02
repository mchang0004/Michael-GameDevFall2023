using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItemData
{
	public string itemName;
	public int count;
	public int slotIndex;

	public InventoryItemData(Item item, int count, int slotIndex)
	{
		this.itemName = item.itemName;
		this.count = count;
		this.slotIndex = slotIndex;
	}
}
