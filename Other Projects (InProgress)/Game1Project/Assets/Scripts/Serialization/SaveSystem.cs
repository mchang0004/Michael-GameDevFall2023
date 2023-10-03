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



	
	/*public static void SaveEnemyDatabase(EnemyDatabase enemyDatabase)
	{
		List<EnemyData> enemyDataList = new List<EnemyData>();

		foreach (GameObject enemy in enemyDatabase.enemies)
		{
			EnemyController enemyController = enemy.GetComponent<EnemyController>();
			if (enemyController != null)
			{
				EnemyData enemyData = new EnemyData(enemyController);
				enemyDataList.Add(enemyData);

			}
		}

		string json = JsonUtility.ToJson(enemyDataList);
		File.WriteAllText(Application.dataPath + "/enemyData.txt", json);
		Debug.Log("### Enemy Save: " + json);
	}

	public static List<EnemyData> LoadEnemyDatabase(EnemyDatabase enemyDatabase)
	{
		if (File.Exists(Application.dataPath + "/enemyData.txt"))
		{
			string enemyDataString = File.ReadAllText(Application.dataPath + "/enemyData.txt");
			List<EnemyData> enemyDataList = JsonUtility.FromJson<List<EnemyData>>(enemyDataString);

			string json = JsonUtility.ToJson(enemyDataList);
			Debug.Log(json);

			return enemyDataList;
		}

		return null;
	}*/


}