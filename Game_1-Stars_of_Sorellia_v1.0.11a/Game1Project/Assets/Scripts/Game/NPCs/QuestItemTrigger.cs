using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItemTrigger : MonoBehaviour
{

	public QuestManager questManager;

	private bool triggered;

	public QuestItem item;

	void Start()
    {
		questManager = FindAnyObjectByType<QuestManager>();
		//triggered = false;

	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (questManager != null && other.gameObject.layer == LayerMask.NameToLayer("Player") && !questManager.questItemInventory.Contains(item))
		{
			questManager.GiveQuestItem(item);
			//triggered = true;
		}
	}
}
