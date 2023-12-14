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

	private bool inRange = false;
	

	// Start is called before the first frame update
	void Start()
	{
		inRange = false;
		//dialogueManager.inDialogue = false;
		dialogueManager = GameObject.FindAnyObjectByType<DialogueManager>();
		questManager = GameObject.FindAnyObjectByType<QuestManager>();
		Debug.Log("# Submitted Quest with All items");

		dialogueQuest.allQuestItemsObtained = false;




	}

	void Update()
	{
		if (inRange)
		{
			Debug.Log("IN Range");
		} else
		{
			Debug.Log("Out of range");
		}
	}
	



	private void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.CompareTag("Player"))
		{
			Debug.Log("@ Enter");
			if (questManager.activeQuests.Contains(dialogueQuest) || questManager.completedQuests.Contains(dialogueQuest))
			{
				if (checkQuestItems())
				{
					Debug.Log("@@ Submmited!");
					dialogueQuest.Complete();
				}

				if (dialogueQuest.questComplete && !questManager.completedQuests.Contains(dialogueQuest))
				{
					dialogueManager.startDialogue(completedDialogue);

					//markQuestComplete();

				}
				else
				{
					startingDialogue = dialogueManager.nothingText;

				}

			}
			else
			{

				if (startingDialogue != null && dialogueManager != null && other.gameObject.layer == LayerMask.NameToLayer("Player"))
				{

					dialogueManager.startDialogue(startingDialogue);



				}
			}
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			inRange = false;
			Debug.Log("@ Exit");
		}
	}

	public bool checkQuestItems()
    {

		if (dialogueQuest != null && dialogueQuest.allQuestItemsObtained)
		{
			return true;
		}

		return false;
	}

	
	
	

}
