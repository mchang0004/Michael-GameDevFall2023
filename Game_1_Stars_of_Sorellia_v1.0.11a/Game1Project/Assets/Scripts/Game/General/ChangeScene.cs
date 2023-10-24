using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

	public string sceneName;
	

	public Vector3 spawnPosition;



	private void OnTriggerEnter2D(Collider2D other)
	{

		//Debug.Log("Trigger");

		if (other.CompareTag("Player"))
		{
		GameObject player = GameObject.FindGameObjectWithTag("Player");

		SceneManager.LoadScene(sceneName);
			
		player.transform.position = spawnPosition;

		}


	}
}


