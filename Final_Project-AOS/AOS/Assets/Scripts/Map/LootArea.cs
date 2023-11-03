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

	public Collider area;
    public int floor;


    public bool DebugBool = false;
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
    public static Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }


    public void dropLoot(string type)
	{
		//Debug.Log(RandomPointInBounds(area.bounds));
       
        if(type == "loot")
        {
            float randomNum = Random.Range(0f, 1f);
            if (randomNum <= 0.5f)
            {
                if(DebugBool) Debug.Log("Coin " + randomNum);
                spawnLootItem("coin");
            }

            if (randomNum > 0.5f && randomNum <= 0.75f)
            {
                if (DebugBool) Debug.Log("Shell" + randomNum);
                spawnLootItem("shell");
            }

            if (randomNum > 0.75f)
            {
                if (DebugBool) Debug.Log("Key" + randomNum);
                spawnLootItem("key");
            }
        }
        
        if(type == "ash")
        {
            spawnLootItem("ash");
        }
       


        
    }

	public void spawnLootItem(string item)
	{
		switch (item)
		{
            case "coin":
                Instantiate(coin, RandomPointInBounds(area.bounds), Quaternion.identity);
                break;
            case "shell":
				Instantiate(shell, RandomPointInBounds(area.bounds), Quaternion.identity);
                break;
            case "ash":
                Instantiate(ash, RandomPointInBounds(area.bounds), Quaternion.identity);
                break;
            case "key":
                Instantiate(key, RandomPointInBounds(area.bounds), Quaternion.identity);
                break;
        }
	}


}
