using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadTester : MonoBehaviour
{
	public Player player;
	public InventoryManager inventoryManager;

	[SerializeField]
	private ItemDatabase itemDatabase;

	
	void Start()
	{
		player = FindAnyObjectByType<Player>();
		inventoryManager = FindAnyObjectByType<InventoryManager>();
		itemDatabase = Resources.Load<ItemDatabase>("ItemDatabase");
	

	}
	public void SavePlayer()
	{
		SaveSystem.SavePlayerData(player, itemDatabase);
		

	}

	public void LoadPlayer()
	{
		//delete everything in the inventory before loading new items
		player.inventoryManager.ClearInventory();

		PlayerData data = SaveSystem.LoadPlayerData(player, itemDatabase);


		if (data != null)
		{
			//player data
			player.level = data.playerLevel;
			player.currentHP = data.playerCurrentHP;
			player.gold = data.playerGold;

			Vector3 position;
			position.x = data.position[0];
			position.y = data.position[1];
			position.z = data.position[2];
			player.transform.position = position;

			//inventory loading/saving
			if (player.inventoryManager != null)
			{
				foreach (InventoryItemData itemData in data.inventoryData)
				{

					Item item = itemDatabase.GetItemByName(itemData.itemName);
					if (item != null)
					{
						
						player.inventoryManager.SpawnNewItem(item, inventoryManager.inventorySlots[itemData.slotIndex], itemData.count);
						Debug.Log("Count " + inventoryManager.inventorySlots[itemData.slotIndex].GetComponentInChildren<InventoryItem>().count);
						
					}
				}
			}


			//other:


		}
	}



}
