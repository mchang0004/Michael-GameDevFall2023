using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    public string scene1;
    public PlayerSaveStats playerSaveStats;
    void Start()
    {
		playerSaveStats = FindAnyObjectByType<PlayerSaveStats>();

		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;

	}

	void Update()
    {
        
    }

    public void loadNewSceneByName(string scene)
    {
        SceneManager.LoadScene(scene);

    }

    public void clearAshes()
    {
		playerSaveStats.clearAshes();

	}
}
