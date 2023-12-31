using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Formatter:
 * https://www.youtube.com/watch?v=XOjd_qU2Ido
 * 
 * JSON:
 * https://medium.com/@gsuryateja9030/displaying-a-game-object-using-json-file-in-unity-a-step-by-step-guide-d8df7d094220
 * https://docs.unity3d.com/ScriptReference/JsonUtility.FromJson.html
 * https://docs.unity3d.com/2020.1/Documentation/Manual/JSONSerialization.html
 * 
 */

[Serializable]
public class PlayerData
{
    public int playerLevel;
    public float playerCurrentHP;
    public int playerGold;
    public float[] position;
	public List<InventoryItemData> inventoryData;
	public List<int> killedEnemies;
	public List<int> activeQuestIDs;
	public List<int> completedQuestIDs;
	public List<int> obtainedQuestItemIDs;
	public List<int> submittedQuestItemIDs;

	public string currentScene;
	public int enemiesKilled;

	//public List<int>


	public PlayerData(Player player)
    {
		//player saving

        playerLevel = player.level;
        playerCurrentHP = player.currentHP;
        playerGold = player.gold;

		position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

		killedEnemies = player.KilledEnemyIDs;

		activeQuestIDs = player.activeQuestIDs;
		completedQuestIDs = player.completedQuestIDs;
		obtainedQuestItemIDs = player.obtainedQuestItemIDs;
		submittedQuestItemIDs = player.submittedQuestItemIDs;

		currentScene = player.currentScene;
		enemiesKilled = player.enemiesKilled;
		//inventory saving

		inventoryData = new List<InventoryItemData>();



		for (int i = 0; i < player.inventoryManager.inventorySlots.Length; i++)
		{
			InventorySlot slot = player.inventoryManager.inventorySlots[i];
			InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

			if (itemInSlot != null)
			{
				Item item = itemInSlot.item;
				int count = itemInSlot.count;

				inventoryData.Add(new InventoryItemData(item, count, i));
			}
		}

		//quest saving

		/*activeQuestIndex = new List<int>();
		completedQuestIndex = new List<int>();

		foreach (Quest quest in player.questManager.activeQuests)
		{
			activeQuestSaved.Add(quest.GetInstanceID());
		}

		foreach (Quest quest in player.questManager.completedQuests)
		{
			completedQuestIndex.Add(quest.GetInstanceID());
		}*/

	}

}
