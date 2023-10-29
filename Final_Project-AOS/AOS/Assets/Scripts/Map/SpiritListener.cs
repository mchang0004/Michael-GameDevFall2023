using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritsListener : MonoBehaviour
{

	bool isZoneOnCooldown = false;

	public FirstPersonController playerController;
	public GameManager gameManager;

	public bool playerIsCrouched = false; 

	void Start()
	{
		playerController = GameObject.Find("Player").GetComponent<FirstPersonController>();

		gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
		
		
	}


	void FixedUpdate()
	{

	
		
	}

	void OnTriggerExit(Collider other)
	{
		
		if (other.gameObject.tag == "Player" && !isZoneOnCooldown && !playerController.getIsCrouched())
		{
			isZoneOnCooldown = true;

			float cooldown = Random.Range(5f, 20f);
			Debug.Log("## " + cooldown);
			StartCoroutine(spiritCooldown(cooldown));	
		}
		Debug.Log("## Exited");

			
	}

	IEnumerator spiritCooldown(float timer)
	{
		//Debug.Log("## Generated Noise");
		gameManager.generateNoise(1);
		yield return new WaitForSeconds(timer);
		isZoneOnCooldown = false;
	}

}
