using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EnemyController : MonoBehaviour
{


	[Header("EnemyID")]
	public int EnemyID;

	[Header("Combat Stats")]
	public float maxHealth = 100;
	[SerializeField]
	public float currentHealth;
	public float attackDamage;
	public float resistance;
	public float magicResistance;
	public float knockback;
	public float knockbackResistance;
	public float attackSpeed;

	public bool increaseAgro = false;

	public bool died;

	public GameObject attackArea;
	public GameObject enemyCenter;

	[SerializeField]
	private bool meleeAttack;

	private bool canAttack = true;
	private float nextAttackTime;

	[Header("Invincibility")]
	public float invincibilityDuration = 1f;
	private bool isInvincible = false;

	[Header("Drops")]
	public Item[] itemDrops;
	public GameObject lootPrefab;
	public int killCountVal = 1;

	[Header("Movement Stats")]
	public float speed = 2f;
	public float distanceToStop = 0f;
	public float baseAggroRange = 5f;
	public float baseDeaggroRange = 7f;

	private float aggroRange;
	private float deaggroRange;


    public Player player;
	private Transform playerTransform;
	private Rigidbody2D rb;
	private bool isFollowingPlayer;
	public GameManager gameManager;

	[Header("Animation Stats")]
	public Animator animator;

	private Vector2 direction;

	public LayerMask playerLayer;



	void Start()
	{
		currentHealth = maxHealth;
		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
		player = GameObject.FindAnyObjectByType<Player>();
		rb = GetComponent<Rigidbody2D>();
		gameManager = FindAnyObjectByType<GameManager>();

		if (died)
		{
			gameObject.SetActive(false);
		}


	}

	void FixedUpdate()
	{

		dontSpawnEnemy();
		FollowPlayer();
		moveAttackArea();
		Vector2 movement = rb.velocity; 

		if(animator != null) {
		
			animator.SetFloat("Horizontal", movement.x);
			animator.SetFloat("Vertical", movement.y);
			animator.SetFloat("Speed", movement.sqrMagnitude);
		
		}

		if (increaseAgro)
		{
			aggroRange = baseAggroRange + 5f;
			deaggroRange = baseDeaggroRange + 5f;
		} else
		{
			aggroRange = baseAggroRange;
			deaggroRange = baseDeaggroRange;
        }
	

		if(canAttack && enemyCenter != null && Time.time >= nextAttackTime) //need to add a check to see if player is in radius before attacking 
		{
			//later add a check to see what weapon is equipped
			Attack();
			nextAttackTime = Time.time + attackSpeed;

		}
		
	}

	//temp function that handles the pathing. Will need to change later, but keep direction part
	void FollowPlayer() 
	{
		if (playerTransform == null)
			return;

		float distance = Vector2.Distance(transform.position, playerTransform.position);
		
		

        if (distance <= aggroRange)
		{
			isFollowingPlayer = true;
		}
		else if (distance > deaggroRange)
		{
			isFollowingPlayer = false;
		}

		//direction 
		if (isFollowingPlayer)
		{
			if (distance > distanceToStop)
			{
				direction = (playerTransform.position - transform.position).normalized;
               

                rb.velocity = direction * speed; //movement (will delete later)
			}
			else
			{
				rb.velocity = Vector2.zero;
			}
		}
		else
		{
			rb.velocity = Vector2.zero;
		}
	}



	public void TakeDamage(float damage)
	{
		//If enemy is hit, increase agro range (this is for ranged attacks that were made from far away)
		increaseAgro = true;

        Debug.Log("OW!");
		if (isInvincible)
			return;

		currentHealth -= damage;
		StartCoroutine(invincibleAndHighlight());

		if (currentHealth <= 0)
		{
			Die();
		}
	}

	void Die()
	{
		// Add death animation or particle effects here if needed
		gameObject.SetActive(false);
		died = true;

		// Drop loot
		foreach (var item in itemDrops)
		{
			GameObject lootObj = Instantiate(lootPrefab, transform.position, Quaternion.identity);
			Loot loot = lootObj.GetComponent<Loot>();
			loot.Initialize(item);


		}

		player.enemiesKilled += killCountVal;
		player.KilledEnemyIDs.Add(EnemyID);



		//gameObject.SetActive(false);
	}

	//checks if enemy is dead and whether or not it should be active:
	void dontSpawnEnemy()
	{
		if (died)
		{
			gameObject.SetActive(false);
		}
	}

	//Sets the enemy object to invincible for a short period of time and highlights them red
	IEnumerator invincibleAndHighlight()
	{
		
		isInvincible = true;
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
		Color originalColor = spriteRenderer.color;
		spriteRenderer.color = Color.red;

		//Wait 1 second and reset states
		yield return new WaitForSeconds(invincibilityDuration);
		spriteRenderer.color = originalColor;
		isInvincible = false;
	}

	void moveAttackArea()
	{
		
		//transform the attack area around the enemy so that it points towards the player.
		if (playerTransform != null && enemyCenter != null)
		{
			Vector3 attackDirection = (playerTransform.position - transform.position).normalized;
			Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, attackDirection);
			enemyCenter.transform.rotation = targetRotation;	//if errors happen, make sure each enemy has a enemyCenter gameObject. Some will not
		}
	}

	bool checkAttackRange()
	{
		if (player != null && attackArea != null)
		{
			BoxCollider2D attackCollider = attackArea.GetComponent<BoxCollider2D>();

			CapsuleCollider2D playerCollider = player.GetComponent<CapsuleCollider2D>();

			if (attackCollider != null && playerCollider != null)
			{
				bool isOverlapping = attackCollider.IsTouching(playerCollider);

				return isOverlapping;
			}
		}

		Debug.Log("CheckAttackRange: Not in range or references are null.");
		return false;
	}

	void Attack()
	{
		increaseAgro = false;

        if (meleeAttack)
		{
			if (checkAttackRange())
			{
				Debug.Log("Enemy Attacked!" + this);
				player.takeDamage(attackDamage);
			}
		} else //if attack is ranged (make sure to modify the attackArea box collider to be longer)
		{
			Debug.Log("Melee Attack is False (Ranged Attack)");
		}
		
		
		
	}

	//used to reset speed after pause/unpause in gameManger
	public Vector2 getSpeed()
	{
		return direction * speed;

    }

}
