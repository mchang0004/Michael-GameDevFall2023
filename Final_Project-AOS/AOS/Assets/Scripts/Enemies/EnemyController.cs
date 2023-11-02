using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
	public float aggroRange = 20f;
	public float deaggroRange = 25f;
	public float attackRange = 4f;
	public float moveSpeed = 3f;

	public Animator animator;
	public Transform player;
	public Transform playerTrack;
	public Transform enemyHead;

	private NavMeshAgent navMeshAgent;
	public Rigidbody enemyRigidbody;

	private Rigidbody playerRigidbody;
	private bool enableDamage = false;
	private bool swordIsSolid = false;

	private PlayerController playerController;

	public Collider swordCollider;
	public Collider swordCoreCollider;


	private bool isWalking = false;
	private bool isAttacking = false;
	private bool isIdle = true;

	private float lostSightTimer = 0f;
	private float delayBeforeIdle = 5f;

	float distanceToPlayer; 

	private float timer = 0f;
	private float messageInterval = 0.1f;

	public LayerMask playerLayer;

	void Start()
	{

	
		enemyRigidbody = GetComponent<Rigidbody>();
		playerTrack = GameObject.Find("PlayerTrack").GetComponent<Transform>();
		navMeshAgent = GetComponent<NavMeshAgent>();
		playerController = GameObject.Find("Player").GetComponent<PlayerController>();
		playerRigidbody = player.GetComponent<Rigidbody>();
		enemyHead = GameObject.Find("EnemyHead").GetComponent<Transform>();

		distanceToPlayer = Vector3.Distance(transform.position, player.position);


	}

	void Update()
	{
		distanceToPlayer = Vector3.Distance(transform.position, player.position);

		//DrawRaycastToPlayer();

		EnemyPathing();

	
	}



	bool DrawRaycastToPlayer()
	{
	
		
		Vector3 enemyHeadPos = enemyHead.position;
	

		Vector3 direction = playerTrack.position - enemyHeadPos;

		Ray ray = new Ray(enemyHeadPos, direction);
		LayerMask obstructionLayer = LayerMask.GetMask("Walls");

		Debug.DrawRay(enemyHeadPos, direction, Color.red);

		RaycastHit hit;
		bool canSeePlayer = true; 

		if (Physics.Raycast(ray, out hit, direction.magnitude, obstructionLayer))
		{
			canSeePlayer = false;
		}

		if (canSeePlayer)
		{
			return true;
			Debug.Log("Enemy can see the player");
		}
		else
		{
			return false;
			Debug.Log("Enemy cannot see the player");
		}

	}



	public enum EnemyState
	{
		Idle,
		Walking,
		Attacking
	}

	private EnemyState state = EnemyState.Idle;

	void EnemyPathing()
	{
		float distanceToPlayer = Vector3.Distance(transform.position, player.position);
		bool canSeePlayer = DrawRaycastToPlayer();

		if (distanceToPlayer <= aggroRange && canSeePlayer)
		{
			if (distanceToPlayer > attackRange)
			{
				state = EnemyState.Walking;
				navMeshAgent.SetDestination(player.position);
				navMeshAgent.isStopped = false;
			}
			else
			{
				state = EnemyState.Attacking;
				navMeshAgent.isStopped = true;
			}

			LookAtPlayer();

			lostSightTimer = 0f;
		}
		else if (distanceToPlayer > deaggroRange)
		{
			state = EnemyState.Idle;
			navMeshAgent.isStopped = true;
		} 
		else
		{
			//state = EnemyState.Idle;
			//navMeshAgent.isStopped = true;

			lostSightTimer += Time.deltaTime;
			navMeshAgent.SetDestination(player.position);

			if (lostSightTimer >= delayBeforeIdle || !navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
			{
				state = EnemyState.Idle;
				navMeshAgent.isStopped = true;
			}
		}
		

		UpdateAnimatorAndCombat();
	}


	void UpdateAnimatorAndCombat()
	{
		isAttacking = state == EnemyState.Attacking;
		isWalking = state == EnemyState.Walking;
		isIdle = state == EnemyState.Idle;

		animator.SetBool("IsWalking", isWalking);
		animator.SetBool("IsAttacking", isAttacking);
		animator.SetBool("IsIdle", isIdle);
		if(swordCoreCollider != null)
		{
			if (swordIsSolid)
			{
				swordCoreCollider.isTrigger = false;
			}
			else
			{
				swordCoreCollider.isTrigger = true;
			}
		}
		

		if (isAttacking && swordCollider != null && swordCollider.isTrigger)
		{
			if (timer >= messageInterval && swordCollider.bounds.Intersects(player.GetComponent<Collider>().bounds) && enableDamage)
			{
				if (playerController != null)
				{
					playerController.TakeDamage(7);
				}

				Debug.Log("Sword hit the player!");
				timer = 0f;
			}
			else
			{
				timer += Time.deltaTime;
			}
		}
	}

	void LookAtPlayer()
	{
		if (player != null)
		{
			Vector3 directionToPlayer = player.position - transform.position;
			directionToPlayer.y = 0; 
			Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 25);
		}
	}		







	public void EnableDamageEvent()
	{
		enableDamage = true;
	}

	public void DisableDamageEvent()
	{
		enableDamage = false;
	}

	public void EnableSolidSword()
	{
		swordIsSolid = true;
	}

	public void DisableSolidSword()
	{
		swordIsSolid = false;
	}
}