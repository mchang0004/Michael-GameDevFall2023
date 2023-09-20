using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
	public LayerMask enemyLayer;
	public LayerMask playerLayer;
	public LayerMask wallLayer;
	public LayerMask defaultLayer;

	public bool damageDealt;
	private EnemyController enemy = null;
	private Player player; 
	

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}

	void Update()
	{
		
		// Debug.Log(clearHit);
		// setEnemyTarget(enemyObject);
		// Debug.Log("Enemy is: " + enemyObject);
	}

	

	private void OnTriggerEnter2D(Collider2D other)
	{
		

		if(other.gameObject.layer != LayerMask.NameToLayer("Enemy") || other.gameObject.layer != LayerMask.NameToLayer("Wall"))
		{
		
		}

		damageDealt = false;
		if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
		{
			
			enemy = other.gameObject.GetComponent<EnemyController>();

			if (enemy != null)
			{
				player.RangedAttack(enemy);
				damageDealt = true;

				//player.setRangedAttackTarget(enemy);
			}

			Destroy(gameObject);

		} else
		{
			player.nextAttackTime = Time.time + player.attackSpeed; //attack speed
		}
		
		Debug.Log(enemy);

		if (wallLayer == (wallLayer | (1 << other.gameObject.layer)))
		{
			Debug.Log("Wall");
			player.setRangedAttackTarget(null);
			Destroy(gameObject);
			player.nextAttackTime = Time.time + player.attackSpeed; //attack speed

		}


	}

}
