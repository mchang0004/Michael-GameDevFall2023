
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
#endif

public class InventorySlot : MonoBehaviour, IDropHandler
{
	public Image image;
	public Color selectedColor, notSelectedColor;

	//[ReorderableList]
	public bool allowAllItemTypes;
	public ItemType[] allowedItemTypes;
	public bool isEquipmentSlot = false;

	private void Awake()
	{
		Deselect();
	}

	public void Select()
	{
		image.color = selectedColor;
	}

	public void Deselect()
	{
		image.color = notSelectedColor;
	}


	public void OnDrop(PointerEventData eventData)
	{
		if (transform.childCount == 0)
		{
			InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();

			if (IsItemTypeAllowed(inventoryItem.item.GetItemType()))
			{
				inventoryItem.parentAfterDrag = transform;
			}
		}
	}

	private bool IsItemTypeAllowed(ItemType itemType)
	{
		if (!allowAllItemTypes)
		{
			// Check if the itemType is present in the allowedItemTypes array
			foreach (ItemType allowedType in allowedItemTypes)
			{
				if (itemType == allowedType)
				{
					return true; // Item type is allowed
				}
			}
		} else
		{
			return true;
		}
		

		return false; // Item type is not allowed
	}

	public void SetDraggable(bool draggable)
	{
		InventoryItem itemInSlot = GetComponentInChildren<InventoryItem>();
		if (itemInSlot != null)
		{
			itemInSlot.canDrag = draggable;
		
		}
		
	}

}
