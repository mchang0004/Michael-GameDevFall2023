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
		triggered = false;

	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!triggered && questManager != null && other.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			questManager.GiveQuestItem(item);
			triggered = true;
		}
	}
}
