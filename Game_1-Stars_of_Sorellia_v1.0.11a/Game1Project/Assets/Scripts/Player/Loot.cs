using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
	[SerializeField] private SpriteRenderer sr;
	[SerializeField] private BoxCollider2D colliderLoot;
	[SerializeField] private float moveSpeed = 10f;
	//[SerializeField] private float inactiveDuration = 1.5f;
	[SerializeField] private float dropSpeed = 1f; 

	public Item item;
	public bool isCollectable;

	public Player player;

	public InventoryManager inventoryManager;

	private bool isBeingCollected = false;
	private bool hasBeenDropped = false; 

	public void Initialize(Item item)
	{
		this.item = item;
		sr.sprite = item.image;
		isCollectable = false;
		inventoryManager = GameObject.Find("InventoryManager")?.GetComponent<InventoryManager>();
	}

	void Update()
	{
		if (isBeingCollected)
			return;
		if(player == null)
		{
			player = GameObject.FindAnyObjectByType<Player>();
		}

		checkIfFull();

	/*	// Temp DELETE LATER
		if (isCollectable && inventoryManager.globalAllowCollection)
		{
			sr.color = Color.green;
		}
		else
		{
			sr.color = Color.white;
		}
		// */
	}
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Collect") && isCollectable && !isBeingCollected && inventoryManager.globalAllowCollection)
		{
			StartCoroutine(MoveAndCollect(other.transform));
		}
	}

	private bool checkIfFull()
	{
		if (inventoryManager.CheckInventoryIsFull() && !item.stackable)
		{
			isCollectable = false;
			return false;
		}
		else if (item.stackable && inventoryManager.CheckInventoryIsFull() && !inventoryManager.stackableItemHasAvailableStack(item))
		{
			isCollectable = false;
			return false;
		}
		else if (!inventoryManager.CheckInventoryIsFull() || inventoryManager.CheckInventoryIsFull() && inventoryManager.stackableItemHasAvailableStack(item))
		{
			isCollectable = true;
			return true;
		}
		return false;
	}

	private IEnumerator MoveAndCollect(Transform target)
	{
		
		Destroy(colliderLoot);

		while (transform.position != target.position)
		{
			transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
			yield return null;
		}

		if (isCollectable && checkIfFull())
		{
			InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();
			if (inventoryManager != null)
			{
				inventoryManager.AddItem(item);
				Destroy(gameObject);
			}
		}
		else
		{
			isCollectable = false;
		}
		
		
	}

	public void MoveAndDrop(Vector3 targetPosition)
	{
		if (!hasBeenDropped)
		{
			hasBeenDropped = true;
			StartCoroutine(MoveToDropPoint(targetPosition));
		}
	}


	private IEnumerator MoveToDropPoint(Vector3 targetPosition)
	{
			

		Vector3 initialPosition = inventoryManager.player.transform.position;

		Collider2D collisionCheck = Physics2D.OverlapPoint(targetPosition);

		if (collisionCheck != null)
		{
			float elapsedTime = 0f;
			while (elapsedTime < dropSpeed)
			{
				float t = elapsedTime / dropSpeed;
				transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
				elapsedTime += Time.deltaTime;
				yield return null;
			}

			transform.position = targetPosition;
		}
		else
		{
			transform.position = initialPosition;
		}

		gameObject.SetActive(true);
	}






}