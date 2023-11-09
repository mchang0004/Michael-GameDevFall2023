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

	void Start()
	{
		ShowPanel("home");
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
}
