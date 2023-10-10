using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{


	public DialogueNode startingDialogue;
	public DialogueNode completedDialogue;
	public Quest dialogueQuest;

	public DialogueManager dialogueManager;
	public QuestManager questManager;


	private bool triggered;

	// Start is called before the first frame update
	void Start()
	{
		//dialogueManager.inDialogue = false;
		dialogueManager = GameObject.FindAnyObjectByType<DialogueManager>();
		questManager = GameObject.FindAnyObjectByType<QuestManager>();

		

		triggered = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
	{
		if (questManager.activeQuests.Contains(dialogueQuest) || questManager.completedQuests.Contains(dialogueQuest))
		{

			if (checkQuestItems())
			{
				dialogueManager.startDialogue(completedDialogue);
				//markQuestComplete();
				disableDialogue();

			}
			else
			{
				startingDialogue = dialogueManager.nothingText;

			}

		}
		else
		{

			if (!triggered && startingDialogue != null && dialogueManager != null && other.gameObject.layer == LayerMask.NameToLayer("Player"))
			{

				dialogueManager.startDialogue(startingDialogue);

	

			}
		}

		



	}

    public bool checkQuestItems()
    {
		//triggered = true;

		if (dialogueQuest != null && dialogueQuest.allQuestItemsObtained)
		{
			Debug.Log("# Submitted Quest with All items");
			dialogueQuest.Complete();
			return true;
		}

		return false;
	}

	
	public void disableDialogue()
	{
		triggered = true;
	}
	

}
