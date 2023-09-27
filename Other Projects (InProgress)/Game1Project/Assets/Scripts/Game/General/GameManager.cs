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
    public List<GameObject> Inventory = new List<GameObject>();


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
        Debug.Log("Game Manger is Awake");

        player = GameObject.Find("Player");
		inventoryManager = GameObject.Find("InventoryManager");
		dialogueManager = GameObject.Find("DialogueManager");
		UICanvas = GameObject.Find("UI Canvas");
        swingItem = FindAnyObjectByType<SwingItem>();
		//Add an Is dead bool, something that isn't reset. Each time you load a scene, it shouldn't respawn enemies.
		Enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));

		//NEED TO SET ITEMS ACTIVE BEFORE SEARCHING
		//Inventory.AddRange(GameObject.FindGameObjectsWithTag("InvSlot"));

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
            SceneManager.LoadScene("Testing Scene");

        }
		if (Input.GetKeyDown(KeyCode.K))
		{

			player.SetActive(true);
			inventoryManager.SetActive(true);
			dialogueManager.SetActive(true);
			UICanvas.SetActive(true);
			swingItem.enabled = true;

		}



		if (Enemies.Count > 0)
        {
            foreach (GameObject enemy in Enemies)
            {
                if (enemy == null)
                {
                    Enemies.Remove(enemy);
                }
            }

        }



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
        foreach(GameObject enemy in Enemies)
        {
           
            enemy.GetComponent<EnemyController>().enabled = false;
            enemy.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    public void unpauseGame()
    {

        //Unpauses Enemies
        foreach (GameObject enemy in Enemies)
        {
            enemy.GetComponent<EnemyController>().enabled = true;
            enemy.GetComponent<Rigidbody2D>().velocity = enemy.GetComponent<EnemyController>().getSpeed(); //gets Vector2 
        }
    }

    public void getInventorySlots()
    {
        Debug.Log("Test");
        //make sure to set all inventory slots as active!!! 
        foreach(GameObject slots in Inventory)
        {
            //write to save file or a saved InventoryManager
            Debug.Log("##: " + slots);
        }


    }

    public void setInventorySlots()
    {

        //make sure to set all inventory slots as active!!! 

        //get the saved file 
        foreach (GameObject slot in Inventory)
        {
            //inventoryManger has an array
        }
    }

}
