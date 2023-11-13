using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TopBarUI : MonoBehaviour
{
	public GameObject homePanel;
	public GameObject cardsPanel;
	public GameObject shopPanel;
	public GameObject settingsPanel;
	public GameObject exitPanel;
    private UICardManager UIcardManager;
	public TextMeshProUGUI shellCountUI;
	public PlayerStats playerStats;



	void Start()
	{
		ShowPanel("home");
		playerStats = FindAnyObjectByType<PlayerStats>();

		UIcardManager = GameObject.Find("UICardManager").GetComponent<UICardManager>();
		shellCountUI = GameObject.Find("ShellUI").GetComponent<TextMeshProUGUI>();

	}

	void Update()
	{

		if (shellCountUI != null)
		{
			setShellUIText(playerStats.totalShells);
		}

	}

	void setShellUIText(int amount)
	{
		shellCountUI.text = amount.ToString();
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
