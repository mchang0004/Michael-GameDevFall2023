using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{

	public PlayerController player;
	public Item item;


    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    public virtual void Collect()
	{
		Debug.Log("Collected");
		player.addItem(item);
        Destroy(gameObject);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player")) 
		{
			Collect(); 
		}
	}


}


