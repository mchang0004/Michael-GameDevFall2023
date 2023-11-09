using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
	public GameObject artifactLocationObject;
	public List<GameObject> possibleArtifactLocations = new List<GameObject>();
    public List<GameObject> lootAreasFloor1 = new List<GameObject>();
    public List<GameObject> lootAreasFloor2 = new List<GameObject>();

    public Color artifactLocationColor = new Color(1, 1, 0, 0.5f);

	public int currentFloor = 1;



	void Awake()
	{
		selectArtifactLocation();
	}

   
	public void spawnLootByFloor(int floor)
	{
		if(floor == 1)
		{
            spawnLoot(lootAreasFloor1);
        } else if (floor == 2)
		{
            spawnLoot(lootAreasFloor2);
        }
        else
		{
			Debug.Log("Floor not set to 1 or 2");
		}
    }

    public void spawnAshByFloor(int floor)
    {
        if (floor == 1)
        {
            spawnAsh(lootAreasFloor1);
        }
        else if (floor == 2)
        {
            spawnAsh(lootAreasFloor2);
        }
        else
        {
            Debug.Log("Floor not set to 1 or 2");
        }
    }

    void selectArtifactLocation()
    {

		if (possibleArtifactLocations.Count > 0)
		{
			int randomIndex = Random.Range(0, possibleArtifactLocations.Count);

			GameObject selectedLocation = possibleArtifactLocations[randomIndex];

			if (artifactLocationObject != null)
			{
				artifactLocationObject.transform.position = selectedLocation.transform.position;
				artifactLocationObject.transform.rotation = selectedLocation.transform.rotation;
			}
		}
		
	}


	void spawnLoot(List<GameObject> currentFloorArea)
	{
		int randomLootAreaIndex = Random.Range(0, currentFloorArea.Count);
		GameObject selectedLootArea = currentFloorArea[randomLootAreaIndex];
		LootArea lootArea = selectedLootArea.GetComponent<LootArea>();
        lootArea.dropLoot("loot");
        
           


    }

	void spawnAsh(List<GameObject> currentFloorArea)
	{
        int randomLootAreaIndex = Random.Range(0, currentFloorArea.Count);
        GameObject selectedLootArea = currentFloorArea[randomLootAreaIndex];
        LootArea lootArea = selectedLootArea.GetComponent<LootArea>();
        lootArea.dropLoot("ash");
    }

}
