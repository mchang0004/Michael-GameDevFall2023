using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopBarUI : MonoBehaviour
{
	public GameObject homePanel;
	public GameObject cardsPanel;
	public GameObject shopPanel;
	public GameObject settingsPanel;
	public GameObject exitPanel;
    private UICardManager UIcardManager;


    void Start()
	{
		ShowPanel("home");
        UIcardManager = GameObject.Find("UICardManager").GetComponent<UICardManager>();

    }

    void ShowPanel(string panelName)
	{
		homePanel.SetActive(panelName == "home");
		cardsPanel.SetActive(panelName == "cards");
		shopPanel.SetActive(panelName == "shop");
		settingsPanel.SetActive(panelName == "settings");
		exitPanel.SetActive(panelName == "exit");
	}

	public void ChangeCurrentMenu(string button)
	{
		ShowPanel(button);
	}

    public void loadCards()
    {
        UIcardManager.LoadCards();
    }

}
