using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawningTester : MonoBehaviour
{
	public InventoryManager inventoryManager;
	public Item[] itemsToPickup;
	public Player player;

	public void PickupItem(int id)
	{
		if (!inventoryManager.CheckInventoryIsFull())
		{
			bool result = inventoryManager.AddItem(itemsToPickup[id]);
			if (result)
			{
				Debug.Log("Pick Up");
			}
			else
			{
				Debug.Log("Pick Up Error!");
			}
		}
		
	}

	public void GetSelectedItem()
	{
		Item receivedItem = inventoryManager.GetSelectedItem(false);
		if (receivedItem != null)
		{
			Debug.Log("Item: " + receivedItem);
		} else
		{
			Debug.Log("Item: Not Found");
		}
	}

	public void UseSelectedItem()
	{
		Item receivedItem = inventoryManager.GetSelectedItem(true);
		if (receivedItem != null)
		{
			if(receivedItem.GetItemType() == ItemType.Consumable)
			{
				player.giveHealth(receivedItem.HPBonus);
				Debug.Log("This item was consumed giving: " + receivedItem.HPBonus);
			}
			//Debug.Log("Used Item: " + receivedItem);
		}
		else
		{
			Debug.Log("Item: Not Found");
		}
	}
}
