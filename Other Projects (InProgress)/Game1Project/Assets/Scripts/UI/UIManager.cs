using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    public Image healthBar;
    public Player player;
    public PlayerMovement playerMovement;
	public float healthAmount;
	public float maxHealth;
    public InventoryManager inventoryManager;
    public bool isChatting = false;

    public TextMeshProUGUI healthText;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        healthAmount = player.currentHP;
		maxHealth = player.maxHP;
        refreshHealthUI();
       
	}

    //Health UI

    public void updateHealthText()
    {
        healthText.text = healthAmount.ToString() + " / " + maxHealth.ToString();

    }

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
		updateHealthText();
	}

    //this method disables attacking and movement so that the UI Interface can be accessed
    //Both functions currently intefere too much. 
    /*public void disableAll()
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
    */


}
