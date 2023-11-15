using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CI_Artifact : CollectableItem
{

	public Artifact artifact;

	public GameManager gameManager;
	public MapManager mapManager;

	public List<Artifact> possibleArtifacts;

	public PlayerStats playerStats;

	public CI_Artifact()
	{
	}


	void Start()
	{
		gameManager = GameManager.Instance;
		playerStats = FindAnyObjectByType<PlayerStats>();
		mapManager = FindAnyObjectByType<MapManager>();
		possibleArtifacts = mapManager.currentPossibleArtifacts;

		artifact = selectRandomArtifact();


	}


	public override void Collect()
	{
		if (gameManager != null)
		{
			gameManager.HasArtifact = true;
		}

		//base.Collect(); 
		Debug.Log("Artifact collected: " + artifact.itemName);
		//must separate because im bad at coding lmao
		gameManager.increaseStat("w", -1);
		gameManager.increaseStat("w", -1);
		gameManager.increaseStat("w", -1);


		playerStats.totalAshes += artifact.value;

		Destroy(gameObject);
	}

	public Artifact selectRandomArtifact()
	{
		int randomIndex = Random.Range(0, possibleArtifacts.Count);
		Artifact randomArtifact = possibleArtifacts[randomIndex];
		return randomArtifact;
	}

}


