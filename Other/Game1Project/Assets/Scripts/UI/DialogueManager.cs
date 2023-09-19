using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DialogueManager : MonoBehaviour
{
	public bool inDialogue;
	public TextMeshProUGUI dialogueBox;
    public GameObject dialogueBoxArea;

    public DialogueNode currentText;
    public Button option1, option2, option3, option4;

    //Things to disable when in dialogue
	public PlayerMovement playerMovement; 
	public InventoryManager inventory; 
    //disable enemy's AI? 

    // Start is called before the first frame update
    void Start()
    {
		//setText(); //change later 
		setText();
		inDialogue = true;

	}

    // Update is called once per frame
    void Update()
    {
      

		//Show or hide the dialogue text area depending on current state:
		if (inDialogue)
		{
			dialogueBoxArea.SetActive(true);
			inventory.inventoryShown = playerMovement.canMove = false;

			//This shows the buttons for each option 
			if (currentText != null)
			{
				SetButton(option1, currentText.option1);
				SetButton(option2, currentText.option2);
				SetButton(option3, currentText.option3);
				SetButton(option4, currentText.option4);
			}
			else
			{
				option1.gameObject.SetActive(false);
				option2.gameObject.SetActive(false);
				option3.gameObject.SetActive(false);
				option4.gameObject.SetActive(false);
			}

		}
		else
		{
			dialogueBoxArea.SetActive(false);
			inventory.inventoryShown = playerMovement.canMove = true;

		}
	}



	//if there is no option, then don't set/show the text/button.  

	private void SetButton(Button button, DialogueNode option)
	{
		button.gameObject.SetActive(option != null);

		if (option != null)
		{
			TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>(); 
			
			if (buttonText != null)
			{
				buttonText.text = option.buttonText;
			}

			button.onClick.RemoveAllListeners();
			button.onClick.AddListener(() => nextNode(option));
		}
	}

		/*void getDialogue()
		{
			//finds the NPC/where the dialogue gameobject trigger
			if(dialogueTrigger != null)
			{

			} else
			{
				Debug.LogWarning("DIALOGUE TRIGGER NOT FOUND");
			}
		}*/

		//change later

		public void startDialogue(DialogueNode dialogueNode)
	{
		currentText = dialogueNode;
		inDialogue = true;
	}

	public void nextNode(DialogueNode nextNode)
	{
		if (currentText.endAfter)
		{
			endDialogue();
				
		} else
		{
			currentText = nextNode;
			setText();
		}
		
	}

	void setText()
	{
		Debug.Log(currentText.text);
		
		if (dialogueBox != null && currentText != null)
		{
			dialogueBox.text = currentText.text;
		}
		else
		{
			Debug.Log("dialogueBox or currentText is null");
		}
	}

	void endDialogue()
    {
        inDialogue = false;
        
    }
}
