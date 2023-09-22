using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  https://medium.com/nerd-for-tech/implementing-a-game-manager-using-the-singleton-pattern-unity-eb614b9b1a74 
    https://forum.unity.com/threads/add-visible-enemies-to-array.180029/ 
*/
public class GameManager : MonoBehaviour
{

    private static GameManager instance;

    public List<GameObject> Enemies = new List<GameObject>();

    public static GameManager Instance
    {
        get
        {
            if(instance == null)
            {
                Debug.Log("Game Manager is Null");
            }

            return instance;    
        }


    }


    private void Awake()
    {
        instance = this;
        Debug.Log("Game Manger is Awake");
        Enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));

        

    }

    private void Update()
    {
        

       
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

}
