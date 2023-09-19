
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
	//public int maxStackedItems = 4;
	public InventorySlot[] inventorySlots;
	public GameObject inventoryItemPrefab;

	public bool globalAllowCollection = true;

	public SwingItem swingItem;
	public bool inventoryEnabled = true;
	public bool inventoryShown = false;
	
	public GameObject inventoryMenu;
	public GameObject inventoryBar;
	public GameObject lootPrefab;

	//info ui
	public GameObject itemInfoPanel;
	public RectTransform itemInfoRect;
	public ItemInfo currentInfo;		

	public Item defaultItem;
	public Player player;

	int selectedSlot = -1;

	[SerializeField]
	private InputActionReference toggleInventory, scrollInput, dropItemInput;

	


	private void Start()
	{
		inventoryShown = false;
		ChangeSelectedSlot(0);
		for(int i = 0; i < 1; i++)
		{
			AddItem(defaultItem);
		}

		itemInfoRect = itemInfoPanel.GetComponent<RectTransform>();

	}

	private void Update()
	{
		


		bool isInventoryFull = CheckInventoryIsFull();

		if(dropItemInput.action.triggered) { DropItem(); }

		if (swingItem.isSwinging)
		{
			DisableAllDragging();
			inventoryEnabled = false;
		} 
		

		if (inventoryShown == false)
		{
			Debug.Log("Inventory Is Hidden");
			DisableAllDragging();
		}

		if (!swingItem.isSwinging)
		{
			inventoryEnabled = true;
		}

		//toggleInventory
		if (inventoryEnabled && toggleInventory.action.triggered) { 
			toggleInventoryMenu();
			
		} 



	

		//scrolling hotbar
		float scroll = Input.GetAxis("Mouse ScrollWheel");

		scroll = -scroll; //inverts scroll direction


		if (scroll != 0 && !swingItem.isSwinging)
		{
			
			int newValue = selectedSlot + (int)(scroll / Mathf.Abs(scroll));
			if (newValue < 0)
			{
				newValue = inventorySlots.Length - 1;
			}
			else if (newValue >= inventorySlots.Length)
			{
				newValue = 0;
			}
			ChangeSelectedSlot(newValue % 10);
		
		}


		//use number keys for hotbar
		if (Input.inputString != null && !swingItem.isSwinging)
		{
			bool isNumber = int.TryParse(Input.inputString, out int number);
			if (isNumber && number > 0 && number < 10)
			{
				ChangeSelectedSlot(number - 1);
			}
			else if (isNumber && number == 0)
			{
				ChangeSelectedSlot(9);
			}
			
		}



		// Get the mouse position in screen coordinates
		Vector3 mousePosition = Input.mousePosition;

		mousePosition.x += 5;
		mousePosition.y += 5;

		// Set the position of the itemInfoPanel to exactly match the mouse cursor
		itemInfoRect.position = mousePosition;
	}

	void ChangeSelectedSlot(int newSlot)
	{
		if (selectedSlot >= 0)
		{
			inventorySlots[selectedSlot].Deselect();
		}
		inventorySlots[newSlot].Select();
		selectedSlot = newSlot;
	}




	public bool DropItem() 
	{
		Item itemToDrop = GetSelectedItem(true);
		if (itemToDrop == null)
		{
			Debug.Log("Nothing to Drop");
			return false;
		}

		Vector3 attackPointPosition = player.GetDropPointPosition();
		Vector3 playerFacingDirection = player.GetPlayerDirection();
		float offsetDistance = 2f; // Adjust the offset distance as needed

		Vector3 spawnPosition = attackPointPosition + playerFacingDirection * offsetDistance;

		GameObject lootObj = Instantiate(lootPrefab, spawnPosition, Quaternion.identity);
		Loot loot = lootObj.GetComponent<Loot>();
		loot.Initialize(itemToDrop);

		return true;
	}

	public void UseSelectedItem()
	{
		Item receivedItem = GetSelectedItem(true);
		if (receivedItem != null)
		{
			if (receivedItem.GetItemType() == ItemType.Consumable)
			{
				player.giveHealth(receivedItem.HPBonus);
				Debug.Log("This item was consumed giving: " + receivedItem.HPBonus);
				//give other bonus
			}
			//Debug.Log("Used Item: " + receivedItem);
		}
		else
		{
			Debug.Log("Item: Not Found");
		}
	}

	//check whether or not the slot is default or equipment slot.
	public bool AddItem(Item itemToAdd)
	{
		if (globalAllowCollection)
		{
			for (int i = 0; i < inventorySlots.Length; i++)
			{
				InventorySlot slot = inventorySlots[i];



				InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
				if (itemInSlot != null && itemInSlot.item == itemToAdd && itemInSlot.count < itemToAdd.MaxStackSize && itemInSlot.item.stackable == true)
				{
					itemInSlot.count++;
					itemInSlot.RefreshCount();
					return true;
				}
				else
				{
					//Debug.Log("INVENTORY IS FULL"); //this needs to block loot, but allow loot for stackable items until they are also full.

				}
			}


			for (int i = 0; i < inventorySlots.Length; i++)
			{
				InventorySlot slot = inventorySlots[i];
				InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
				if (itemInSlot == null)
				{
					SpawnNewItem(itemToAdd, slot);
					return true;
				}
			}
		}
		

		return false;
	}



	void SpawnNewItem(Item item, InventorySlot slot)
	{
		GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
		InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
		inventoryItem.InitializeItem(item);
		inventoryItem.inventoryManager = this; // Assign the inventoryManager reference
	}

	public Item GetSelectedItem(bool use)
	{
		InventorySlot slot = inventorySlots[selectedSlot];

		InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
		if (itemInSlot != null)
		{
			Item item = itemInSlot.item;
			if (use == true)
			{
				itemInSlot.count--;
				if (itemInSlot.count == 0)
				{
					Destroy(itemInSlot.gameObject);
				}
				else
				{
					itemInSlot.RefreshCount();
				}
			}

			return item;
		}

		return null; // Return null when there is no item in the selected slot.
	}



	public bool toggleInventoryMenu()
	{
		if (!inventoryMenu.activeSelf)
		{
			inventoryMenu.SetActive(true);
			inventoryShown = true;
			player.canAttack = false;
			EnableAllDragging();
			
			return true;
		}
		else
		{
			inventoryMenu.SetActive(false);
			inventoryShown = false;
			player.canAttack = true;
			DisableAllDragging();
			itemInfoPanel.SetActive(false);

			return false;
		}
	}

	public void EnableAllDragging()
	{
		foreach (InventorySlot slot in inventorySlots)
		{
			//Debug.Log("DRAGGING YES");
			slot.SetDraggable(true);
		}
	}

	public void DisableAllDragging()
	{
		foreach (InventorySlot slot in inventorySlots)
		{
			//Debug.Log("DRAGGING NO");
			slot.SetDraggable(false);
		}
	}




	public bool CheckInventoryIsFull()
	{
		foreach (InventorySlot slot in inventorySlots)
		{
			if (!slot.isEquipmentSlot)
			{
				InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
				if (itemInSlot == null)
				{
					//Inventory is not full
					return false;
				}
			}
			
		}
		//Inventory is full
		return true;
	}


	public bool stackableItemHasAvailableStack(Item item)
	{
		if (!item.stackable)
		{
			return false;
		}

		for (int i = 0; i < inventorySlots.Length; i++)
		{
			InventorySlot slot = inventorySlots[i];
			InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

			if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < item.MaxStackSize)
			{
				//The item is stackable and there is a slot with available space
				return true;
			}
		}

		//No stackable space
		return false;
	}



}


