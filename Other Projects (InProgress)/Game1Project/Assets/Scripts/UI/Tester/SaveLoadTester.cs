using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadTester : MonoBehaviour
{
	public Player player;
	public InventoryManager inventoryManager;

	[SerializeField]
	private ItemDatabase itemDatabase;


	void Awake()
	{
		player = FindAnyObjectByType<Player>();
		inventoryManager = FindAnyObjectByType<InventoryManager>();
		itemDatabase = Resources.Load<ItemDatabase>("ItemDatabase");

		DontDestroyOnLoad(this);
	}

	void Start()
	{


		//Debug.Log("TEST");
	}
	public void SavePlayer()
	{
		SaveSystem.SavePlayerData(player, itemDatabase);
		

	}
	//remeber to load the player when starting each scene to reset enemy spawns
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


			//enemy IDs 
			player.KilledEnemyIDs = data.killedEnemies;


			//need to add a way to respawn enemies that weren't killed yet in previous saves

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
			Loot[] lootItems = FindObjectsOfType<Loot>();

			foreach (Loot lootItem in lootItems)
			{
				Destroy(lootItem.gameObject);
			}

		}
	}



}
