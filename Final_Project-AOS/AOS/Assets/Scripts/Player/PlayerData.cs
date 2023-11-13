using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{

	public int totalShells;
	// Start is called before the first frame update


	public PlayerData(PlayerStats player)
	{
		totalShells = player.totalShells;
	}
}
