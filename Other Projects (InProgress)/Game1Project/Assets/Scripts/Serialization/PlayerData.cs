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

	public PlayerData(Player player)
    {
        playerLevel = player.level;
        playerCurrentHP = player.currentHP;
        playerGold = player.gold;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;


		inventoryData = new List<InventoryItemData>();

		


		for (int i = 0; i < player.inventoryManager.inventorySlots.Length; i++)
		{
			InventorySlot slot = player.inventoryManager.inventorySlots[i];
			InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

			if (itemInSlot != null)
			{
				Item item = itemInSlot.item;
				int count = itemInSlot.count;

				// Pass the slot index as the third argument
				inventoryData.Add(new InventoryItemData(item, count, i));
			}
		}

	}

}
