using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestManager : MonoBehaviour
{
	public List<Quest> AllQuests;
	public List<QuestItem> AllQuestItems;

	public List<Quest> activeQuests;
	public List<Quest> completedQuests;
	

	public List<QuestItem> questItemInventory;

	public GameObject questUI;
	public GameObject questUI_Quests;
	public GameObject questUI_QuestItems;

	public GameObject questPrefab;
	public GameObject questItemPrefab;
	public TextMeshProUGUI questText;

	public Player player;




	private void Awake()
	{
		DontDestroyOnLoad(this);
		foreach(Quest quest in AllQuests)
		{
			quest.Reset();
		}

	}

	// Start is called before the first frame update
	void Start()
	{
		//questUI = GameObject.Find("QuestUI");

		questUI.SetActive(false);
		ResetAllQuests();
		activeQuests.Clear();
		completedQuests.Clear();
		

		//questUI.SetActive(true);
	}

	// Update is called once per frame
	void Update()
	{
		foreach(Quest quest in AllQuests)
		{
			quest.updateQuestStatus();
		}

		/*if(player != null)
		{
			Debug.Log("A int: " + player.activeQuestIDs);

			Debug.Log("C int: " + player.completedQuestIDs);

			Debug.Log("A : " + activeQuests);

			Debug.Log("C : " + completedQuests);
		}*/
		

		if (player == null) player = FindAnyObjectByType<Player>();


		if (Input.GetKeyDown(KeyCode.F))
		{
			UpdateQuestUI();
			ToggleQuestUI();

		}

		List<Quest> questsToRemove = new List<Quest>();

		if (activeQuests.Count > 0)
		{
			
			foreach (Quest quest in activeQuests)
			{
				
				//quest.updateQuestStatus();
				if (quest.questComplete)
				{
					Debug.Log("ISSUE HERE");
					questsToRemove.Add(quest);
					foreach(QuestItem item in quest.questItems)
					{
						questItemInventory.Remove(item);
						player.submittedQuestItemIDs.Add(item.questItemID);
						player.obtainedQuestItemIDs.Remove(item.questItemID);
						Debug.Log("# ADDED AND REMOVED QUESTS");
						//player.obtainedQuestItemIDs.Clear();		
					}

				}
			}
					
			foreach (Quest questToRemove in questsToRemove)
			{
				int questID = getQuestID(questToRemove);
				player.activeQuestIDs.Remove(questID);	
				player.completedQuestIDs.Add(questID);



				activeQuests.Remove(questToRemove);
				addQuestToCompleted(questToRemove);

				Debug.Log("# Quest was removed from Active Quests");

			}
			

		}
	}

	public void ToggleQuestUI()
	{
		questUI.SetActive(!questUI.activeSelf);
	}


	public void UpdateQuestUI()
	{
		ClearQuestUI();

		foreach (Quest quest in activeQuests)
		{

			GameObject questUI = Instantiate(questPrefab, questUI_Quests.transform);

			Transform questTextTransform = questUI.transform.Find("Quest Text"); 
			
			if (questTextTransform != null)
			{
				TextMeshProUGUI questText = questTextTransform.GetComponent<TextMeshProUGUI>();

				if (questText != null)
				{
					questText.text = quest.questText;
					//Debug.Log("### Quest Text Assigned");
				}
				
			}
			
		}
			
		

		foreach (QuestItem questItem in questItemInventory)
		{
			GameObject questItemUI = Instantiate(questItemPrefab, questUI_QuestItems.transform);

			Image questItemImage = questItemUI.GetComponent<Image>();

			if (questItemImage != null)
			{
				questItemImage.sprite = questItem.questItemImage;
			}
		}

	}

	public void ClearQuestUI()
	{
		foreach (Transform child in questUI_Quests.transform)
		{
			Destroy(child.gameObject);
		}

		foreach (Transform child in questUI_QuestItems.transform)
		{
			Destroy(child.gameObject);
		}

	}

	public void GiveQuest(Quest quest)
	{
		
		if (!activeQuests.Contains(quest) && !completedQuests.Contains(quest))
		{
			quest.Reset();
			activeQuests.Add(quest);

			int questID = getQuestID(quest);
			if (!player.activeQuestIDs.Contains(questID))
			{
				player.activeQuestIDs.Add(questID);	
			}

			player.questAudio.Play();
			UpdateQuestUI();
		}


	}




	public void GiveQuestItem(QuestItem item)
	{

		if (player.submittedQuestItemIDs.Contains(item.questItemID))
		{
			Debug.Log("######## Test!!!" + item.questItemID);
		}

		if (!questItemInventory.Contains(item))
		{
			questItemInventory.Add(item);
		}

		if (!player.obtainedQuestItemIDs.Contains(getQuestItemID(item)) )
		{
			player.obtainedQuestItemIDs.Add(getQuestItemID(item));
		}
		UpdateQuestUI();

	}


	public void addQuestToCompleted(Quest quest)
	{
		if (!completedQuests.Contains(quest) && !activeQuests.Contains(quest))
		{
			completedQuests.Add(quest);
		}
		UpdateQuestUI();
	}


	public int getQuestID(Quest quest)
	{
		return quest.questID;	
	}

	public int getQuestItemID(QuestItem questItem)
	{
		return questItem.questItemID;
	}

	public void addQuestToActiveByID(int questID)
	{
		Debug.Log("### ADDING ACTIVE");

		foreach (Quest quest in AllQuests)
		{

			if(quest.questID == questID)
			{
				GiveQuest(quest);
				//Debug.Log("############ ADDED QUEST BY ID");
				//player.questAudio.Play();

			}
		}
	}

	public void addQuestToCompletedByID(int questID)
	{
		Debug.Log("### ADDING COMPLETED");

		foreach (Quest quest in AllQuests)
		{
			if (quest.questID == questID)
			{
				addQuestToCompleted(quest);
				//completedQuests.Add(quest);
				//quest.questComplete = true;
			}
		}
	}

	public void addQuestItemByID(int questItemID)
	{
		foreach(QuestItem questItem in AllQuestItems)
		{
			if (questItem.questItemID == questItemID)
			{
				//Debug.Log("GIVING QUEST ITEM AFTER LOAD NEW");
				GiveQuestItem(questItem);
				player.questAudio.Play();
			}
		}
	}

	public void addAllQuestsAndQuestItems()
	{
		//Debug.Log("### ADDING ALL QUESTS AND QUEST ITEMS");


		List<int> activeQuestsTemp = new List<int>(player.activeQuestIDs);
		List<int> completedQuestsTemp = new List<int>(player.completedQuestIDs);
		List<int> obtainedQuestItemsTemp = new List<int>(player.obtainedQuestItemIDs);


		foreach (int questID in activeQuestsTemp)
		{
			if (questID != null)
			{
				addQuestToActiveByID(questID);
				Debug.Log("### Added active" + questID);
			}
		}

		foreach (int questID in completedQuestsTemp)
		{
			addQuestToCompletedByID(questID);
			Debug.Log("### Added completed" + questID);
		}

		foreach (int questItemID in obtainedQuestItemsTemp)
		{
			addQuestItemByID(questItemID);
			Debug.Log("### Added item" + questItemID);
		}


	}

	public void ResetAllQuests()
	{
		foreach (Quest quest in AllQuests)
		{
			quest.Reset();
		}
	}

	

}