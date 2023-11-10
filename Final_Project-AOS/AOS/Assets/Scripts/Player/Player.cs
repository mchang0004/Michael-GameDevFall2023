using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	public string playerName = "Player";
	public int maxHealth = 10;
	public int healthRegenerationRate = 1;
	public int maxStamina = 100;

	public int currentShells;
	public int currentCoins;
	public int currentAshes;
	public int currentKeys;

	private int currentHealth;
	private float currentStamina;
	private bool isRegeneratingHealth = false;

	public FirstPersonController fpController;

	bool isDead = false;

	public List<Item> lootInventory = new List<Item>();

    private void Start()
	{

		currentShells = currentCoins = currentAshes = currentKeys = 0;

		fpController.lockCursor = true;
		currentHealth = maxHealth;
		currentStamina = maxStamina;


	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			RegenerateHealth(healthRegenerationRate);
		}

		checkHP();

		if(isDead)
		{
			Cursor.lockState = CursorLockMode.Confined;
			SceneManager.LoadScene("DeadScreen");
		}
	}

	public void TakeDamage(int damage)
	{
		if (currentHealth > 0)
		{
			currentHealth -= damage;
			currentHealth = Mathf.Max(currentHealth, 0);
			Debug.Log(playerName + " took " + damage + " damage. Current health: " + currentHealth);
		}
	}

	public void Heal(int healing)
	{
		currentHealth += healing;
		currentHealth = Mathf.Min(currentHealth, maxHealth);
		Debug.Log(playerName + " healed for " + healing + " points. Current health: " + currentHealth);
	}

	public void RegenerateHealth(int amountToRegenerate)
	{
		currentHealth = Mathf.Min(currentHealth + amountToRegenerate, maxHealth);
		Debug.Log(playerName + " regenerated " + amountToRegenerate + " health. Current health: " + currentHealth);
	}

	public bool UseStamina(int staminaCost)
	{
		if (currentStamina >= staminaCost)
		{
			currentStamina -= staminaCost;
			return true;
		}
		else
		{
			Debug.Log(playerName + " does not have enough stamina to perform this action.");
			return false;
		}
	}

	public void RechargeStamina(float staminaRechargeRate)
	{
		currentStamina = Mathf.Min(currentStamina + staminaRechargeRate, maxStamina);
	}

	private void OnCollisionEnter(Collision collision)
	{
	
		if (collision.gameObject.CompareTag("Enemy"))
		{
			Debug.Log("Instant Kill Collision");
			TakeDamage(200);
		}
	}

	public void addItem(Item item)
	{
		lootInventory.Add(item);
		

    }

	public void calculateInventory()
	{
		foreach(Item item in lootInventory)
		{
			if(item.type == itemType.coin)
			{
				Debug.Log("Coin Picked Up And Current Coins added +1");
				currentCoins++;

            }
		}
	}

	private void checkHP()
	{
		if(currentHealth <= 0)
		
			isDead = true;
		
	}

	public void Win()
	{

		Cursor.lockState = CursorLockMode.Confined;
        calculateInventory();
        SceneManager.LoadScene("CardShop");
        
    }


}
