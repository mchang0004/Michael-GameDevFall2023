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
        Enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));

        //NEED TO SET ITEMS ACTIVE BEFORE SEARCHING
        //Inventory.AddRange(GameObject.FindGameObjectsWithTag("InvSlot"));

        

    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("New Scene?");
            SceneManager.LoadScene("Testing Scene");
        }
        else if (Input.GetKeyDown("k"))
        {
            SceneManager.LoadScene("SampleScene");

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
