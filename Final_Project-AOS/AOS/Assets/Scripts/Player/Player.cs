using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public string playerName = "Player";
	public int maxHealth = 10;
	public int healthRegenerationRate = 1;
	public int maxStamina = 100;

	private int currentHealth;
	private float currentStamina;
	private bool isRegeneratingHealth = false;

	private void Start()
	{
		currentHealth = maxHealth;
		currentStamina = maxStamina;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			RegenerateHealth(healthRegenerationRate);
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


}
