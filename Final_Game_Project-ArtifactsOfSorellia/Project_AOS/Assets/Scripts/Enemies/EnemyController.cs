using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
	public float aggroRange = 10f;
	public float attackRange = 4f;
	public float moveSpeed = 3f;
	public Animator animator;
	public Transform player;

	private NavMeshAgent navMeshAgent;
	private Rigidbody playerRigidbody;
	private bool enableDamage = false;
	private PlayerController playerController;

	public Collider swordCollider;

	public float knockbackDuration = 0.5f;
	public float knockbackForce = 10f;

	private bool isWalking = false;
	private bool isAttacking = false;
	private bool isIdle = true;

	private float timer = 0f;
	private float messageInterval = 0.1f;

	void Start()
	{
		if (animator == null || player == null)
		{
			Debug.LogError("Animator and Player references are not set!");
			enabled = false;
		}

		navMeshAgent = GetComponent<NavMeshAgent>();
		playerController = GameObject.Find("Player").GetComponent<PlayerController>();
		playerRigidbody = player.GetComponent<Rigidbody>();
	}

	void Update()
	{
		float distanceToPlayer = Vector3.Distance(transform.position, player.position);

		if (isAttacking)
		{
			Vector3 direction = (player.position - transform.position).normalized;
			transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		}

		if (!isAttacking && distanceToPlayer <= attackRange)
		{
			isAttacking = true;
			isWalking = false;
			isIdle = false;

			animator.SetTrigger("IsAttacking");
			navMeshAgent.isStopped = true;
		}
		else if (isAttacking && distanceToPlayer > attackRange)
		{
			isAttacking = false;
			isWalking = true;
			isIdle = false;

			navMeshAgent.isStopped = false;
		}
		else if (isWalking && distanceToPlayer > aggroRange)
		{
			isWalking = false;
			isIdle = true;
			navMeshAgent.isStopped = true;
		}
		else if (isIdle && distanceToPlayer <= aggroRange)
		{
			isWalking = true;
			isIdle = false;

			animator.SetTrigger("IsWalking");
			navMeshAgent.isStopped = false;
		}

		if (isWalking)
		{
			navMeshAgent.SetDestination(player.position);
		}

		animator.SetBool("IsWalking", isWalking);
		animator.SetBool("IsAttacking", isAttacking);
		animator.SetBool("IsIdle", isIdle);

		if (swordCollider != null && swordCollider.isTrigger && isAttacking)
		{
			if (timer >= messageInterval)
			{
				if (swordCollider.bounds.Intersects(player.GetComponent<Collider>().bounds) && enableDamage)
				{
					if (playerController != null)
					{
						playerController.TakeDamage(7);
					}

					Debug.Log("Sword hit the player!");
					timer = 0f;
				}
			}
			else
			{
				timer += Time.deltaTime;
			}
		}
	}

	IEnumerator ApplyKnockback(Vector3 knockbackDirection)
	{
		float elapsedTime = 0;

		while (elapsedTime < knockbackDuration)
		{
			float force = Mathf.Lerp(0, knockbackForce, elapsedTime / knockbackDuration);

			playerRigidbody.AddForce(knockbackDirection * force);

			elapsedTime += Time.deltaTime;

			yield return null;
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
}