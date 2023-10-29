using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactLocations : MonoBehaviour
{
	public MapManager mapManager;

	

	void OnDrawGizmos()
	{
		Gizmos.color = mapManager.artifactLocationColor;
		Gizmos.DrawCube(transform.position, Vector3.one * 0.5f); 
	}
	
}
