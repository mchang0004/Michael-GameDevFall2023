using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootArea : MonoBehaviour
{

	public FirstPersonController playerController;
	public GameManager gameManager;

	public GameObject coin;
	public GameObject shell;
	public GameObject key;
	public GameObject ash;


	// Start is called before the first frame update
	void Start()
	{
		playerController = GameObject.Find("Player").GetComponent<FirstPersonController>();

		gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();


	}

	void OnDrawGizmos()
	{
		Gizmos.color = new Color(1, 1, 0, 0.5f);
		Gizmos.DrawCube(transform.position, new Vector3(3, .25f, 3));
	}




}
