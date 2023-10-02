using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
//using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting;

/*
 * https://www.youtube.com/watch?v=6uMFEM-napE
 */
public static class SaveSystem
{

	public static void SavePlayerData(Player player, ItemDatabase itemDatabase)
	{
		PlayerData data = new PlayerData(player);

		data.inventoryData.Clear();

		foreach (InventorySlot slot in player.inventoryManager.inventorySlots)
		{
			InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
			if (itemInSlot != null)
			{
				int slotIndex = player.inventoryManager.GetSlotIndex(slot);
				Item item = itemDatabase.GetItemByName(itemInSlot.item.itemName);

				if (item != null)
				{
					InventoryItemData itemData = new InventoryItemData(item, itemInSlot.count, slotIndex);
					data.inventoryData.Add(itemData);
				}
			}
		}

		string json = JsonUtility.ToJson(data);
		File.WriteAllText(Application.dataPath + "/saveData.txt", json);
		Debug.Log(json);
	}

	public static PlayerData LoadPlayerData(Player player, ItemDatabase itemDatabase)
	{
		if (File.Exists(Application.dataPath + "/saveData.txt"))
		{
			string saveString = File.ReadAllText(Application.dataPath + "/saveData.txt");
			PlayerData data = JsonUtility.FromJson<PlayerData>(saveString);

			string json = JsonUtility.ToJson(data);
			Debug.Log(json);

			return data;
		}

		

		return null;
	}


}