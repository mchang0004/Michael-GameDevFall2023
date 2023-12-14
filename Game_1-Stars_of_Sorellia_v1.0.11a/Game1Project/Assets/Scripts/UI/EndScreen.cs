using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
	public SaveLoadTester saveLoadTester;
	public GameManager gameManager;


	void Awake()
	{
		gameManager = FindAnyObjectByType<GameManager>();

	}

	public void mainMenu()
	{
	

		SceneManager.LoadScene("Main Menu");

	}
	
	public void loadGame()
	{
		
		gameManager.player.SetActive(true);
		//gameManager.player.swingItem.weaponSprite.ChangeSprite();

		gameManager.inventoryManager.SetActive(true);
		gameManager.dialogueManager.SetActive(true);
		gameManager.UICanvas.SetActive(true);
		gameManager.swingItem.enabled = true;
		SceneManager.LoadScene("Area 1");

		gameManager.saveLoadTester.LoadPlayer();
	}

	public void quitGame()
    {
		Application.Quit();
	}

}
