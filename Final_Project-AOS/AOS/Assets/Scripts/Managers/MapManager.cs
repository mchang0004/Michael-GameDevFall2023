using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
	public GameObject artifactLocationObject;
	public List<GameObject> possibleArtifactLocations = new List<GameObject>();

	public Color artifactLocationColor = new Color(1, 1, 0, 0.5f);



	void Awake()
	{
		selectArtifactLocation();
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

	

}
