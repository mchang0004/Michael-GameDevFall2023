using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CI_Artifact : CollectableItem
{

	public Artifact artifact;

	public GameManager gameManager;

	public CI_Artifact()
	{
	}


	void Start()
	{
		gameManager = GameManager.Instance;

	}


	public override void Collect()
	{
		//base.Collect(); 
		Debug.Log("Artifact collected: " + artifact.itemName);
		//must separate because im bad at coding lmao
		gameManager.increaseStat("w", -1);
		gameManager.increaseStat("w", -1);
		gameManager.increaseStat("w", -1);
		Destroy(gameObject);
	}


}


