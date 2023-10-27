using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{

	public virtual void Collect()
	{
		Debug.Log("Collected");
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player")) 
		{
			Collect(); 
		}
	}


}


