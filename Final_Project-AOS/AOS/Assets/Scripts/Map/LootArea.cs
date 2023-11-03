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

	// Start is called before the first frame update
	void Start()
	{
		playerController = GameObject.Find("Player").GetComponent<FirstPersonController>();

		gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();


        dropLoot();
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


    public void dropLoot()
	{
		//Debug.Log(RandomPointInBounds(area.bounds));
        spawnLootItem("coin");
        spawnLootItem("shell");
        spawnLootItem("ash");
        spawnLootItem("key");
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
