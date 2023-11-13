using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;

public class SaveManager
{
	public static void SavePlayerData(PlayerStats player)
	{
		PlayerData data = new PlayerData(player);

		string json = JsonUtility.ToJson(data);
		File.WriteAllText(Application.dataPath + "/saveData.txt", json);
		Debug.Log("##### SAVING " + json);
	}

	public static PlayerData LoadPlayerData(PlayerStats player)
	{
		if (File.Exists(Application.dataPath + "/saveData.txt"))
		{
			string saveString = File.ReadAllText(Application.dataPath + "/saveData.txt");
			PlayerData data = JsonUtility.FromJson<PlayerData>(saveString);

			string json = JsonUtility.ToJson(data);
			Debug.Log("##### LOADING " + json);

			return data;
		}



		return null;
	}
}
