using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
	[SerializeField] private SpriteRenderer sr;
	[SerializeField] private BoxCollider2D collider;
	[SerializeField] private float moveSpeed = 10f;
	[SerializeField] private float inactiveDuration = 1.5f; // Time before the loot can be collected again
	[SerializeField] private float dropSpeed = 1f; // Set the desired drop speed

	public Item item;
	public bool isCollectable;

	public InventoryManager inventoryManager;

	private bool isBeingCollected = false;
	private bool hasBeenDropped = false; // Add this line to declare hasBeenDropped

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


		checkIfFull();

		// Temp DELETE LATER
		if (isCollectable && inventoryManager.globalAllowCollection)
		{
			sr.color = Color.green;
		}
		else
		{
			sr.color = Color.white;
		}
		// 
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
		Destroy(collider);

		// Move the loot towards the player
		while (transform.position != target.position)
		{
			transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
			yield return null;
		}

		// Add the loot item to the inventory if it's collectible
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
			// If the loot can't be collected, set isCollectable to false
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