using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{

	public GameObject mainMenuObject;

	private void Awake()
    {
        DontDestroyOnLoad(this);
    }

		// Start is called before the first frame update
	void Start()
    {
		mainMenuObject = GameObject.Find("Main Menu");

	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
