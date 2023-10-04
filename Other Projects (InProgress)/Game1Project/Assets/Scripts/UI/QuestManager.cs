using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{

	public List<Quest> activeQuests;
	public List<Quest> completedQuests;

	public List<QuestItem> questItemInventory;

	public GameObject questUI;
	public GameObject questUI_Quests;
	public GameObject questUI_QuestItems;

	public GameObject questPrefab;
	public GameObject questItemPrefab;

	private void Awake()
	{
		DontDestroyOnLoad(this);
	}

	// Start is called before the first frame update
	void Start()
	{
		//questUI = GameObject.Find("QuestUI");

		questUI.SetActive(false);

		//questUI.SetActive(true);
	}

	// Update is called once per frame
	void Update()
	{

		if (Input.GetKeyDown(KeyCode.F))
		{
			UpdateQuestUI();
			ToggleQuestUI();

		}

		List<Quest> questsToRemove = new List<Quest>();

		if (activeQuests.Count > 0)
		{
			{
				foreach (Quest quest in activeQuests)
				{
					quest.updateQuestStatus();
					if (quest.questComplete)
					{
						questsToRemove.Add(quest);
					}
				}

				foreach (Quest questToRemove in questsToRemove)
				{
					activeQuests.Remove(questToRemove);
					Debug.Log("# Quest was removed from Active Quests");

				}
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

			questUI.SetActive(true);
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
			activeQuests.Add(quest);
		}
	}


	public void GiveQuestItem(QuestItem item)
	{
		questItemInventory.Add(item);
	}
}