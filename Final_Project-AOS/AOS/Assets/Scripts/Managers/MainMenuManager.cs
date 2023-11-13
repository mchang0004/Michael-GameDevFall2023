using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    public string scene1;
    public PlayerStats playerStats;
    void Start()
    {
		playerStats = FindAnyObjectByType<PlayerStats>();


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
        playerStats.clearAshes();

	}
}
