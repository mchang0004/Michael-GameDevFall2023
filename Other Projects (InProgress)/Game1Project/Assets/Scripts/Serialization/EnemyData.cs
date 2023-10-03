using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyData
{
	public bool died;
	public float[] position;
	public float enemyCurrentHP;
	public int enemyID;

	public EnemyData(EnemyController enemy)
    {
        died = enemy.died;
		
		position = new float[3];
		position[0] = enemy.transform.position.x;
		position[1] = enemy.transform.position.y;
		position[2] = enemy.transform.position.z;

		enemyCurrentHP = enemy.currentHealth;

		enemyID = enemy.EnemyID;

		}




}
