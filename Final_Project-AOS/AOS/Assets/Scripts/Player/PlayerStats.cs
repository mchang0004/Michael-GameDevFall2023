using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerStats : MonoBehaviour
{
	public int totalShells;
	public int totalAshes;
	public UICardManager uiCardManager;


	public int totalRuns;
	public int deaths;
	public int successes;
	public int artifactsCollected;
	public int totalShellsCollected;
	public int totalAshesCollected;
	public int totalKeysCollected;
	public int totalCoinsCollected;


	void Awake()
	{
		DontDestroyOnLoad(this);
		clearAshes();
		uiCardManager = FindAnyObjectByType<UICardManager>();

	}



	public void addAshes(int amount)
	{
		totalAshes += amount;
	}

	public void removeAshes(int amount)
	{
		Debug.Log("Ashed Removed : " + totalAshes + "  |  " + amount); ;
		totalAshes -= amount;	
	}

	public void clearAshes()
	{
		totalAshes = 0;
	}

	public void addShells(int amount)
	{
		totalShells += amount;
	}

	public void removeShells(int amount)
	{
		totalShells -= amount;
	}


}
