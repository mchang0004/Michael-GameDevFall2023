using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue Node", menuName = "Scriptable object/Dialogue Node")]
public class DialogueNode : ScriptableObject
{
	[TextArea(4, 10)]
	public string text; // Text displayed
	public string buttonText; //text that will appear on the button
	public bool endAfter;   //should the dialogue end after this?

	public Quest quest;
	public QuestItem questItem;

	public QuestManager questManager;

	public bool giveQuest => quest != null; //giveQuest/giveQuestItem is set to true if there is a quest attached to this node
	public bool giveQuestItem => questItem != null;

	public int NodeID;

	public DialogueNode option1;
	public DialogueNode option2;
	public DialogueNode option3;
	public DialogueNode option4;


	public void triggerQuestUpdate()
	{
		questManager = FindAnyObjectByType<QuestManager>();
		//Debug.Log("Testing");
		if (giveQuest && (!questManager.activeQuests.Contains(quest) || !questManager.completedQuests.Contains(quest)))
		{
			questManager.GiveQuest(quest);
			Debug.Log("# Gave Quest " + quest);
		}

		if (giveQuestItem && !questManager.questItemInventory.Contains(questItem))
		{
			questManager.GiveQuestItem(questItem);
			Debug.Log("# Gave Quest Item " + questItem);
		}
	}
}