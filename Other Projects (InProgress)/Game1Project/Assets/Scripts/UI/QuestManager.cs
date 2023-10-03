using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

    public List<Quest> activeQuests;
    public List<Quest> completedQuests;
    public List<Quest> canncledQuests;

    public List<QuestItem> questItemInventory;

    public GameObject questUI;
    

	private void Awake()
	{
		DontDestroyOnLoad(this);
	}

	// Start is called before the first frame update
	void Start()
    {
		questUI = GameObject.Find("QuestUI");
		//questUI.SetActive(false);

	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
