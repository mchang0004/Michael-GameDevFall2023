using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{

	public int totalShells;

	public List<int> IDPlayerDeck;

	public List<int> IDCardInventory;




	public PlayerData(PlayerSaveStats player)
	{
		totalShells = player.totalShells;
		IDCardInventory = player.uiCardManager.getCardInventoryByID();
		IDPlayerDeck = player.uiCardManager.getDeckByID();
	}
}

