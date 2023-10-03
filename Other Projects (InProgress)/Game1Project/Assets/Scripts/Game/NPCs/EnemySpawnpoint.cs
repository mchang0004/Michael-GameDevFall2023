using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnpoint : MonoBehaviour
{

	public GameObject enemyPrefab;
	public EnemyController enemyController;

	public Vector3 position;

	public bool enableSpawn;
	public bool isSpawned;

	public int ID;



	void Start()
	{

		enemyController = enemyPrefab.GetComponent<EnemyController>();
		
		if (ID == enemyController.EnemyID)
		{

			isSpawned = false;

			if (enemyController.died)
			{
				enableSpawn = false;
			} else if(!isSpawned)
			{
				enableSpawn = true;	
			} else
			{
				enableSpawn = false;
			}

			position = transform.position;
		
		}
	}

	void Update()
	{
		if (enableSpawn && !isSpawned)
		{
			enemyPrefab.SetActive(true);
			//Instantiate(enemyPrefab, position, Quaternion.identity);
			isSpawned = true;
		}
	}

}
