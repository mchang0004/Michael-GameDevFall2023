using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.SceneManagement;
using TMPro;


/*
 * This class handles the player character:
 * There is a separate class for the inventory, the swinging of the equipped weapon, and player movement.
 * This class handles attacking, health, player stats, etc.
 */

public class Player : MonoBehaviour
{
	[Header("HP & Defense")]
	public float currentHP = 100f;
	public float maxHP = 100f;
	public float currentMaxHP;
	public float invincibilityDuration = 0.75f;
	private bool isInvincible;

	[Header("Player Stats")]

	public float Strength = 0f;
	public float Dexterity = 0f;
	public float Constitution = 0f;
	public float Intelligence = 0f;
	public float Wisdom = 0f;
	public float Charisma = 0f;

	[Header("General")]
	public int level;
	public int gold;
	public PlayerMovement playerMovement; //PlayerMovement Script for Direction

	public GameObject playerLight;
	public GameObject disablePlayerLight;

	public List<int> KilledEnemyIDs;
	public List<int> activeQuestIDs;
	public List<int> completedQuestIDs;
	public List<int> obtainedQuestItemIDs;
	public List<int> submittedQuestItemIDs;

	public Rigidbody2D playerRB;

	public Vector2 playerDirection { get; private set; } = Vector2.right;

	[SerializeField] public Transform dropPoint;

	public CapsuleCollider2D playerCollider;

	[Header("Managers")]
	public UIManager uiManager;
	public InventoryManager inventoryManager;
	public GameManager gameManager;

	public QuestManager questManager;
	public SwingItem swingItem;

	[Header("Combat")]
	[HideInInspector]
	public bool allowRepeatSwing; //controlled by the weapon
	public EnemyController rangedAttackTarget; //ranged attack target for arrow
	public Item equipped_item; //pass item that is selected to use image and get stats
	public GameObject equippedItemObject;
	public InputActionReference attack;
	public GameObject arrow;    //arrow game object
	public Item arrowItem;
	public int enemiesKilled;

	public bool canAttack = false; //should be accessed in SwingItem.cs for the animation

	public Transform attackPoint;


	public float baseAttackDamage = 0f;
	public float baseAttackPointOffset = 0.0f;
	public float baseAttackRange = 0.0f;
	public float baseAttackSpeed = 3f;      //Base Attack Speed should be the slowest attack possible
	public float nextAttackTime;

	public float knockbackForce = 10f;
	[HideInInspector]
	public float attackDamage, attackRangedDamage, attackPointOffset, attackRange, attackSpeed;


	public LayerMask enemyLayers;
	[Header("Equipment")]
	private float equipmentHealthBonus;
	private float equipmentMeleeDamageBonus;
	private float equipmentRangedDamageBonus;



	[Header("Audio")]
	public AudioSource weaponAudio;
	public AudioSource damageAudio;
	public AudioSource inventoryAudio;
	public AudioSource questAudio;
	public AudioClip currentWeaponAudio;
	public AudioClip damageSound;
	public AudioClip inventorySound;
	public AudioClip questSound;


	[Header("UI")]


	public TextMeshProUGUI MeleeDamageText;
	public TextMeshProUGUI RangedDamageText;
	public TextMeshProUGUI EnemiesKilledText;

	[Header("Scene")]
	public string currentScene;

	/*public PlayerStats playerStats;


	public float Strength = playerStats.Strength;
	public float Dexterity = playerStats.Dexterity;
	public float Constitution = playerStats.Constitution;
	public float Intelligence = playerStats.Intelligence;
	public float Wisdom = playerStats.Wisdom;
	public float Charisma = playerStats.Charisma;
*/

	//[Header("Toggles")]


	//Attacking:


	//Melee Weapon Swing




	//Swing Melee Combat 




	//scene management:

	private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    void Start()
	{
		

		activeQuestIDs.Clear();
		completedQuestIDs.Clear();

		currentMaxHP = maxHP + equipmentHealthBonus;
		uiManager.maxHealth = currentMaxHP;
		uiManager.healthAmount = currentHP;

		GameObject weaponObject = transform.Find("Equipped Weapon").gameObject;

		swingItem = weaponObject.GetComponent<SwingItem>();
		uiManager.healthAmount = currentMaxHP;

		questManager = FindAnyObjectByType<QuestManager>();

		gameManager = FindAnyObjectByType<GameManager>();

		playerLight = GameObject.Find("PlayerVision(Light)");

		damageAudio.clip = damageSound;
		inventoryAudio.clip = inventorySound;
		questAudio.clip = questSound;	
	}

	void Update()
	{
		if(MeleeDamageText != null)
		{
			MeleeDamageText.text = "Melee Damage: " + attackDamage;
		}
		
		if(RangedDamageText != null)
		{
			RangedDamageText.text = "Ranged Damage: " + attackRangedDamage;
		}
	  
		if(EnemiesKilledText != null) 
		{
			EnemiesKilledText.text = "Enemies Killed: " + enemiesKilled;
		}

		weaponAudio.clip = currentWeaponAudio;

		currentScene = SceneManager.GetActiveScene().name;

		//Debug.Log("HP # " + currentMaxHP);
		currentMaxHP = maxHP + equipmentHealthBonus;

		equipmentBuff();

		//Debug.Log("equipmentMeleeDamageBonus: " + equipmentMeleeDamageBonus + " |  equipmentRangedDamageBonus: " + equipmentRangedDamageBonus);

		//Debug.Log("damage: " + attackDamage + " | Ranged damage: " + attackRangedDamage);

		disablePlayerLight = GameObject.Find("DisablePlayerLight");

		if (disablePlayerLight == null)
		{
			playerLight.SetActive(true);

		} else
		{
			playerLight.SetActive(false);
		}
		//Health:
		uiManager.maxHealth = currentMaxHP;
		uiManager.healthAmount = currentHP;

		

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

			if (equipped_item.GetItemType() == ItemType.Weapon)
			{
				attackDamage = equipped_item.attackDamage + baseAttackDamage + equipmentMeleeDamageBonus;

			}
			else
			{
				attackDamage = baseAttackDamage + equipmentRangedDamageBonus;
			}
			
			if(equipped_item.GetItemType() == ItemType.Ranged_Weapon)
			{
				attackRangedDamage = equipped_item.attackDamage + baseAttackDamage + equipmentRangedDamageBonus;

			} else
			{
				attackRangedDamage = baseAttackDamage + equipmentRangedDamageBonus;
			}

			allowRepeatSwing = equipped_item.allowRepeatSwing;


			//set audio clip
			currentWeaponAudio = equipped_item.mainAudio;
		}
		else
		{
			allowRepeatSwing = false;
			attackPointOffset = attackRange = attackDamage = attackRangedDamage = 0;
			currentWeaponAudio = null;

			//disable audio clip
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
					weaponAudio.Play();
					nextAttackTime = Time.time + attackSpeed;
				}
				else if (attack.action.triggered && weaponEquipped() && !allowRepeatSwing)
				{
					Attack();
					weaponAudio.Play();
					nextAttackTime = Time.time + attackSpeed;
				}
			
		
			//if item is a rnaged weapon

			

				if (equipped_item != null && attack.action.triggered && equipped_item.GetItemType() == ItemType.Ranged_Weapon)
				{

					if (inventoryManager.fireArrow())
					{
						Vector3 mousePosition = Input.mousePosition;
						Vector3 directionMouse = (mousePosition - Camera.main.WorldToScreenPoint(transform.position)).normalized;
						float angle = Mathf.Atan2(directionMouse.y, directionMouse.x);

						GameObject arrowObject = Instantiate(arrow, equippedItemObject.transform.position, Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg));

						Rigidbody2D rb = arrowObject.GetComponent<Rigidbody2D>();

						Vector2 velocity = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * 15f;

						rb.velocity = velocity;
						//attack speed is reset with the actual ranged attack 

					}



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

	public void setRangedAttackTarget(EnemyController enemy)
	{
		rangedAttackTarget = enemy;
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
		checkHP();
		equipped_item = inventoryManager.GetSelectedItem(true);
		giveHealth(equipped_item.HPBonus);
		Debug.Log("This item was consumed giving: " + equipped_item.HPBonus);
		checkHP();

	}

	//Attack Math (Currently only Melee Swing)
	void Attack()
	{
	
		//Set Up IF to check type of weapon equipped to determine type of attack;
		Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
		float damage = attackDamage; //tempfloat for swingDamage maybe change to just using attackDamage variable

		Vector2 knockbackDirection = playerDirection;

		//Enemies in range of attack?
		foreach (Collider2D enemy in hitEnemies)
		{
			//Debug.Log("Hit: " + enemy.name);
			EnemyController enemyHealth = enemy.GetComponent<EnemyController>();
			if (enemyHealth != null)
			{
				enemyHealth.TakeDamage(damage);

				Rigidbody2D enemyRigidbody = enemy.GetComponent<Rigidbody2D>();

				if (enemyRigidbody != null) { enemyRigidbody.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);  }

			}
			//uiManager.reduceHealthUI(1f);
		}
	}

	//called in arrow class
	public void RangedAttack(EnemyController enemy)
	{


		//Debug.Log(enemy.maxHealth + " HIT #####");
		float damage = attackRangedDamage;

		enemy.TakeDamage(damage);
		nextAttackTime = Time.time + attackSpeed; //attack speed

		


	}

	//draws the area that the attack can hit
	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireSphere(attackPoint.position, attackRange);
	}


	public void takeDamage(float amount)
	{
		checkHP();

		if (isInvincible)
			return;

		currentHP -= amount;
		//Debug.Log("You took " + amount + " of damage! You are now at: " + currentHP);
		damageAudio.Play();
		StartCoroutine(invincibleAndHighlight());
		if (currentHP <= 0)
		{
			//dead
			SceneManager.LoadScene("Game Over");
			gameManager.disableGameElements();
			Debug.Log("DEAD!");
		}

		checkHP();
		/*if(amount > 0)
		{
			//uiManager.refreshHealthUI();
		}*/

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

	public void equipmentBuff()
	{
		float tempHealthBonus = 0;
		float tempMeleeDamageBonus = 0;
		float tempRangedDamageBonus = 0;

		foreach (Item item in inventoryManager.equippedItems)
		{
			//Debug.Log(item);
			tempHealthBonus += item.healthBonus;
			tempMeleeDamageBonus += item.meleeDamageBonus;
			tempRangedDamageBonus += item.rangedDamageBonus;
		}

		equipmentHealthBonus = tempHealthBonus;
		equipmentMeleeDamageBonus = tempMeleeDamageBonus;
		equipmentRangedDamageBonus = tempRangedDamageBonus;

	}

	public void checkHP()
	{
		if (currentHP > currentMaxHP)
		{
			Debug.Log("# CURRENT HP SET");
			currentMaxHP = maxHP + equipmentHealthBonus;
			currentHP = currentMaxHP;
		}
	}

}
