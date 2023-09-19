using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DialogueManager : MonoBehaviour
{
	public bool inDialogue;
	public TextMeshProUGUI dialogueBox;
    public GameObject dialogueArea;
    

    //Things to disable when in dialogue
	public PlayerMovement playerMovement; 
	public InventoryManager inventory; 
    //disable enemy's AI? 

    // Start is called before the first frame update
    void Start()
    {
        setText();
        inDialogue = false;

	}

    // Update is called once per frame
    void Update()
    {
        if (inDialogue)
        {
            dialogueArea.SetActive(true);
            inventory.inventoryShown = playerMovement.canMove = false;

		} else
        {
            dialogueArea.SetActive(false);
			inventory.inventoryShown = playerMovement.canMove = true;

		}


	}

    void setText()
    {
        dialogueBox.text = "Hello! This is a test.";
    }
}
