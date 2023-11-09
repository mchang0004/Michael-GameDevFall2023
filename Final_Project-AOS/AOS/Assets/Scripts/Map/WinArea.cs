using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinArea : MonoBehaviour
{
	public PlayerController player;
	public GameManager gameManager;
	void Start()
    {
		player = GameObject.Find("Player").GetComponent<PlayerController>();
		gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();


	}

	// Update is called once per frame
	void Update()
    {
        
    }

	void OnDrawGizmos()
	{
		Gizmos.color = new Color(0, 1, 0, 0.5f);
		Gizmos.DrawCube(transform.position, new Vector3(3, .25f, 3));
	}

	void OnTriggerEnter(Collider other)
	{
		// Check if the collided object has the GameManager script

		if (gameManager != null)
		{
			if (gameManager.HasArtifact)
			{
				player.Win();
			}
			else
			{
				Debug.Log("You need an artifact to leave.");
			}
		}
		
	}
}
