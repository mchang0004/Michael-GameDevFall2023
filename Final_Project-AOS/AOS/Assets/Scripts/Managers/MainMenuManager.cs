using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    public string scene1;
    void Start()
    {
    }

    void Update()
    {
        
    }

    public void loadNewSceneByName(string scene)
    {
        SceneManager.LoadScene(scene);

    }

    
}
