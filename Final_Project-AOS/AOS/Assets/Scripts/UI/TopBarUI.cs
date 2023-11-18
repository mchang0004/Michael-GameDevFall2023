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
	public PlayerSaveStats playerSaveStats;
	public SaveLoad saveLoad;



	void Start()
	{
		ShowPanel("home");
		playerSaveStats = FindAnyObjectByType<PlayerSaveStats>();
		saveLoad = FindAnyObjectByType<SaveLoad>();

		UIcardManager = GameObject.Find("UICardManager").GetComponent<UICardManager>();
		shellCountUI = GameObject.Find("ShellUI").GetComponent<TextMeshProUGUI>();

	}

	void Update()
	{

		if (shellCountUI != null)
		{
			setShellUIText(playerSaveStats.totalShells);
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

	public void ExitTheGame()
	{
		saveLoad.SavePlayer();
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false; 
		#else
			Application.Quit();
		#endif
	}

}
