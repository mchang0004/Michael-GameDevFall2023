using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

/*
 * This class handles the player character:
 * There is a separate class for the inventory, the swinging of the equipped weapon, and player movement.
 * This class handles attacking, health, player stats, etc.
 */

public class Player : MonoBehaviour
{
	[Header("HP Stats")]
	public float currentHP = 25f;
	public float maxHP = 100f;
	public UIManager uiManager;

	public float invincibilityDuration = 1f;
	private bool isInvincible;

	public SwingItem swingItem;

	[HideInInspector]
	public bool allowRepeatSwing; //controlled by the weapon


	public Item equipped_item; //pass item that is selected to use image and get stats
	

	public InventoryManager inventoryManager;

	[SerializeField]
	public PlayerMovement playerMovement; //PlayerMovement Script for Direction

	[SerializeField]
	public InputActionReference attack;

	[Header("Player Stats")]
	public float Strength = 1f;
	public float Dexterity = 1f;
	public float Constitution = 1f;
	public float Intelligence = 1f;
	public float Wisdom = 1f;
	public float Charisma = 1f;


	//[Header("Toggles")]
	public bool canAttack = false; //should be accessed in SwingItem.cs for the animation

	
	//Attacking:


	//Melee Weapon Swing
	public Vector2 playerDirection { get; private set; } = Vector2.right;

	[Header("Attacking")]
	public Transform attackPoint;
	[SerializeField] public Transform dropPoint;

	//Swing Melee Combat 
	public float baseAttackDamage = 1f;
	public float baseAttackPointOffset = 0.0f;
	public float baseAttackRange = 0.0f;
	public float baseAttackSpeed = 3f;      //Base Attack Speed should be the slowest attack possible
	public float nextAttackTime;

	[HideInInspector]
	public float attackDamage, attackPointOffset, attackRange, attackSpeed;


	public LayerMask enemyLayers;
	public CapsuleCollider2D playerCollider;




	void Start()
	{
		
		uiManager.maxHealth = maxHP;
		uiManager.healthAmount = currentHP;

		GameObject weaponObject = transform.Find("Equipped Weapon").gameObject;

		swingItem = weaponObject.GetComponent<SwingItem>();
		uiManager.healthAmount = maxHP;
	}

	void Update()
	{

		//Health:
		/*uiManager.maxHealth = maxHP;
		uiManager.healthAmount = currentHP;*/
		if(currentHP > maxHP)
		{
			currentHP = maxHP;
		}

		GameObject weaponObject = transform.Find("Equipped Weapon").gameObject;

		swingItem = weaponObject.GetComponent<SwingItem>();

		Vector2 direction = playerMovement.Direction;
		equipped_item = inventoryManager.GetSelectedItem(false);



		//Weapon Hit for Melee: Change code to allow for different types
		if (equipped_item != null)
		{
			attackSpeed = baseAttackSpeed - equipped_item.attackSpeed;
			

			attackRange = equipped_item.attackRange + baseAttackRange;
			attackPointOffset = equipped_item.attackPointOffset + baseAttackPointOffset;
			attackDamage = equipped_item.attackDamage + baseAttackDamage;

			allowRepeatSwing = equipped_item.allowRepeatSwing;

		}
		else
		{
			allowRepeatSwing = false;
			attackPointOffset = attackRange = attackDamage = 0;
			
		}
		//if weapon is swapped to new weapon
		

		//ALL COMBAT:
		if (canAttack)
		{

		//"Left Click" Checks:





		//Melee Swinging Combat:
			if (Time.time >= nextAttackTime && equipped_item != null)
			{
				if (attack.action.ReadValue<float>() > 0.0f && weaponEquipped() && allowRepeatSwing)
				{
					Attack();
					nextAttackTime = Time.time + attackSpeed;
				}
				else if (attack.action.triggered && weaponEquipped() && !allowRepeatSwing)
				{
					Attack();
					nextAttackTime = Time.time + attackSpeed;
				}
			}
		//if item is a consumable
			if (equipped_item != null && attack.action.triggered && equipped_item.GetItemType() == ItemType.Consumable)
			{
				Consume();
			}

		}

		//Handles Swinging 8-Directions
		if (direction != Vector2.zero)
		{
			playerDirection = direction;
			Vector3 attackPointDirection = new Vector3(direction.x, direction.y, 0);
			attackPoint.position = playerCollider.bounds.center + (attackPointDirection * attackPointOffset);
			dropPoint.position = playerCollider.bounds.center + attackPointDirection * 1f;
		} else
		{
			//Swing Animation & Direction when not moving (4-directions):
			attackPoint.position = playerCollider.bounds.center + (playerMovement.getLastDirection() * attackPointOffset);
			playerDirection = playerMovement.getLastDirection();
		}

		

	}


	IEnumerator invincibleAndHighlight()
	{

		isInvincible = true;
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
		Color originalColor = spriteRenderer.color;
		spriteRenderer.color = Color.red;

		// Wait 1 second and reset states
		yield return new WaitForSeconds(invincibilityDuration);
		spriteRenderer.color = originalColor;
		isInvincible = false;
	}



	//gets the type of weapon (currently only melee)
	bool weaponEquipped()
	{
		if (equipped_item != null && equipped_item.GetItemType() == ItemType.Weapon)
		{
			return true;
		}
		return false;
	}

	//Use Consumable Item Check
	void Consume()
	{
		equipped_item = inventoryManager.GetSelectedItem(true);
		giveHealth(equipped_item.HPBonus);
		Debug.Log("This item was consumed giving: " + equipped_item.HPBonus);
	}

	//Attack Math (Currently only Melee Swing)
	void Attack()
	{
	
		//Set Up IF to check type of weapon equipped to determine type of attack;
		Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
		float damage = attackDamage; //tempfloat for swingDamage maybe change to just using attackDamage variable
		
		//Enemies in range of attack?
		foreach (Collider2D enemy in hitEnemies)
		{
			Debug.Log("Hit: " + enemy.name);
			EnemyController enemyHealth = enemy.GetComponent<EnemyController>();
			if (enemyHealth != null)
			{
				enemyHealth.TakeDamage(damage);
			}
			uiManager.reduceHealthUI(1f);
		}


	}

	//draws the area that the attack can hit
	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireSphere(attackPoint.position, attackRange);
	}


	public void takeDamage(float amount)
	{
		if (isInvincible)
			return;

		currentHP -= amount;
		Debug.Log("You took " + amount + " of damage! You are now at: " + currentHP);
		StartCoroutine(invincibleAndHighlight());
		if (currentHP <= 0)
		{
			//dead
			Debug.Log("DEAD!");
		}
		if(amount > 0)
		{
			//uiManager.refreshHealthUI();
		}

	}

	public void giveHealth(float amount)
	{
		currentHP += amount;
		Debug.Log("Current HP: " + currentHP);
		//check for max health
		//uiManager.gainHealthUI(amount);

	}

	//currently only used in the Inventory Manager
	public Vector3 GetDropPointPosition()
	{
		return dropPoint.position;
	}

	//converst playerDirection to Vector3 for inventory manager class
	public Vector3 GetPlayerDirection() 
	{
		return transform.forward;
	}

}
