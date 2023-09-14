using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Image healthBar;
    public Player player;
    public PlayerMovement playerMovement;
	public float healthAmount;
	public float maxHealth;
    public InventoryManager inventoryManager;
    public bool isChatting = false;
    

    void Update()
    {
        healthAmount = player.currentHP;
		maxHealth = player.maxHP;
        refreshHealthUI();
        //This interferes too much with the overall game. Need to reimplement
        /*if (isChatting)
        {
			disableAll();
		}
		else
        {
			enableAll();
		}*/
	}

    //Health UI

    public void reduceHealthUI(float amount)
    {
        healthAmount -= amount;
		
		//healthBar.fillAmount = healthAmount / maxHealth;

	}

	public void gainHealthUI(float amount) { 
        healthAmount += amount;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);

        //healthBar.fillAmount = healthAmount / maxHealth;
    
    }

    public void refreshHealthUI()
    {
		healthBar.fillAmount = healthAmount / maxHealth;

	}

    //this method disables attacking and movement so that the UI Interface can be accessed
    //Both functions currently intefere too much. 
    public void disableAll()
    {
        inventoryManager.inventoryEnabled = false;
        player.canAttack = false;
		playerMovement.canMove = false;
	}

    public void enableAll() {
		inventoryManager.inventoryEnabled = true;
		player.canAttack = true;
        playerMovement.canMove = true;
	}



}
