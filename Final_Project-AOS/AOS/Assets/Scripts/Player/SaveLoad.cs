using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    public PlayerStats player;
	// Start is called before the first frame update
	void Awake()
	{
		player = FindAnyObjectByType<PlayerStats>();
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
		}
	}

}
