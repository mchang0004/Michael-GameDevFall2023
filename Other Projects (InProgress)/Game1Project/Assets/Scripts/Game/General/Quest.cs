using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Scriptable object/Quest")]
public class Quest : ScriptableObject
{
	public string questTitle;
	public string questText;

	public List<QuestItem> questItems;

	public DialogueNode turnInDialogue;

	public bool allQuestItemsObtained;
	public bool questComplete;

	public QuestManager questManager;


	//True if item is for this quest
	public bool IsItemObjective(QuestItem item)
	{
		return questItems.Contains(item);
	}

	public void updateQuestStatus()
	{
		questManager = FindAnyObjectByType<QuestManager>();

		bool itemCollected = true; //if the player doesn't have one of the quest items then it sets it to false.
		foreach (QuestItem item in questItems)
		{
			if (!questManager.questItemInventory.Contains(item))
			{
				itemCollected = false;
				break;
			}

		}

		allQuestItemsObtained = itemCollected;

	}

	public void Complete()
	{
		questComplete = true;
		Debug.Log("# Quest is Complete: Give Loot/Bonus Here");
	}

	public void Reset()
	{
		questComplete = false;
		allQuestItemsObtained = false;
	}


}
