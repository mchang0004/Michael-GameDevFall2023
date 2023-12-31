using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    public PlayerSaveStats player;
	// Start is called before the first frame update
	void Awake()
	{
		player = FindAnyObjectByType<PlayerSaveStats>();
		DontDestroyOnLoad(this);
	}

	public void SavePlayer()
	{
		SaveManager.SavePlayerData(player);
	}

	public void LoadPlayer()
	{
		 
		PlayerData data = SaveManager.LoadPlayerData(player);

		if (data != null)
		{
			player.totalShells = data.totalShells;
			player.uiCardManager.IDCardInventory = data.IDCardInventory;
			player.uiCardManager.IDPlayerDeck = data.IDPlayerDeck;

		
		}
	}

}
