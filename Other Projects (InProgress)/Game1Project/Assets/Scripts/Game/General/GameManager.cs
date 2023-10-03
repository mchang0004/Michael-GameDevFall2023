using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

/*  https://medium.com/nerd-for-tech/implementing-a-game-manager-using-the-singleton-pattern-unity-eb614b9b1a74 
    https://forum.unity.com/threads/add-visible-enemies-to-array.180029/ 
*/
public class GameManager : MonoBehaviour
{

    //private static GameManager instance;

    public List<GameObject> Enemies = new List<GameObject>();
	public List<int> DeadEnemyIDs = new List<int>();
	private Dictionary<int, GameObject> enemyDictionary = new Dictionary<int, GameObject>();



	public EnemyDatabase enemyDatabase;

	public GameObject player;
    public GameObject inventoryManager;
    public GameObject dialogueManager;
    public GameObject UICanvas;
    public SwingItem swingItem;


	/* public static GameManager Instance
	 {
		 get
		 {
			 if(instance == null)
			 {
				 Debug.Log("Game Manager is Null");
			 }

			 return instance;    
		 }


	 }*/


	private void Awake()
    {
        DontDestroyOnLoad(this);
        //instance = this;
        //Debug.Log("Game Manger is Awake");

        player = GameObject.Find("Player");
		inventoryManager = GameObject.Find("InventoryManager");
		dialogueManager = GameObject.Find("DialogueManager");
		UICanvas = GameObject.Find("UI Canvas");
        swingItem = FindAnyObjectByType<SwingItem>();
		//Add an Is dead bool, something that isn't reset. Each time you load a scene, it shouldn't respawn enemies.
		Enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));



		player.SetActive(false);
		inventoryManager.SetActive(false);
		dialogueManager.SetActive(false);
		UICanvas.SetActive(false);
		swingItem.enabled = false;

	}

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("New Scene?");
			
			loadNewScene();
			SceneManager.LoadScene("Testing Scene");

        }
		if (Input.GetKeyDown(KeyCode.K))
		{
			loadNewScene();
			SceneManager.LoadScene("SampleScene");

			/*player.SetActive(true);
			inventoryManager.SetActive(true);
			dialogueManager.SetActive(true);
			UICanvas.SetActive(true);
			swingItem.enabled = true;*/

		}


		//Handles Moving Enemy from alive list to dead list
		GameObject[] enemyGameObjects = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (GameObject enemy in enemyGameObjects)
		{

			if (!Enemies.Contains(enemy))
			{
				Enemies.Add(enemy);
                Debug.Log("# Added " + enemy);
			}

			List<GameObject> enemiesCopy = new List<GameObject>(Enemies);

			foreach (GameObject enemiesToDelete in enemiesCopy)
			{
				if (enemiesToDelete == null)
				{
					Enemies.Remove(enemy);
				}
			}

		}
	}

    //Helper function to move enemies from alive to dead (called in enemyController script)
    public void moveEnemyToDead(GameObject enemy)
    {
		Debug.Log("# Is Dead " + enemy);		

		EnemyController enemyController = enemy.GetComponent<EnemyController>();

		if (enemyController != null)
		{
			DeadEnemyIDs.Add(enemyController.EnemyID);
			Enemies.Remove(enemy);
			enemyDictionary[enemyController.EnemyID] = enemy; // Store the reference
		}
	}

	private GameObject FindDeadEnemyByID(int enemyID)
	{
		if (enemyDictionary.ContainsKey(enemyID))
		{
			return enemyDictionary[enemyID];
		}
		return null;
	}

	public void startGame()
    {


		player.SetActive(true);
		inventoryManager.SetActive(true);
		dialogueManager.SetActive(true);
		UICanvas.SetActive(true);
		swingItem.enabled = true;
		SceneManager.LoadScene("SampleScene");
	}

	public void pauseGame()
    {
		//Pauses Enemies
		if (Enemies.Count > 0)
		{
			
			foreach (GameObject enemy in Enemies)
			{

				enemy.GetComponent<EnemyController>().enabled = false;
				enemy.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			}
		}
    }

	public void unpauseGame()
	{
		//Unpauses Enemies

		if (Enemies != null && Enemies.Count > 0)
		{
			foreach (GameObject enemy in Enemies)
			{
				if (enemy != null && !enemy.Equals(null))
				{
					EnemyController enemyController = enemy.GetComponent<EnemyController>();

					if (enemyController != null)
					{
						enemyController.enabled = true;
						Rigidbody2D rb2d = enemy.GetComponent<Rigidbody2D>();

						if (rb2d != null)
						{
							rb2d.velocity = enemyController.getSpeed();
						}
					}
				}
			}
		}
	}


	//function for loading new scene must clear enemies list, but not dead enemies. The Enemies list will be regenerated based on the DeadEnemies.
	public void loadNewScene() //probably a string
	{

		Enemies.Clear();
		//add parameter for which scene later
		//SceneManager.LoadScene("STRING FOR SCENE?");

		foreach (int deadEnemyID in DeadEnemyIDs)
		{
			GameObject deadEnemy = FindDeadEnemyByID(deadEnemyID);

			if (deadEnemy != null)
			{
				deadEnemy.SetActive(false);
			}
		}



	}

	

}
