using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/EnemyDatabase")]
public class EnemyDatabase : ScriptableObject
{
	public List<GameObject> enemies;

	public GameObject GetEnemyByID(int ID)
	{
		foreach (GameObject enemy in enemies)
		{
			EnemyController enemyController = enemy.GetComponent<EnemyController>();
			if (enemyController.EnemyID == ID)
			{
				return enemy;
			}
		}
		return null;
	}


}
