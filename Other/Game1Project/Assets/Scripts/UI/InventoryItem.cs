
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
	[Header("UI")]
	public Image image;
	public TMP_Text countText;
	public SwingItem swingItem;
	public InventoryManager inventoryManager;
	public InventorySlot slot;
	public bool showInfoPanel;


	[HideInInspector] public Item item;
	[HideInInspector] public int count = 1;
	[HideInInspector] public Transform parentAfterDrag;

	public Player player;

	private GameObject draggingItem;
	private Transform originalParent;

	public bool canDrag = false;
	//private bool isDragging = false;
	//private static bool isAnyItemDragging = false;

	// Start is called before the first frame update
	public void InitializeItem(Item newItem)
	{
		item = newItem;
		image.sprite = newItem.image;
		RefreshCount();
		Debug.Log(item);
	}

	public void RefreshCount()
	{
		countText.text = count.ToString();
		bool textActive = count > 1;
		countText.gameObject.SetActive(textActive);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if(item != null)
		{
			inventoryManager.currentInfo.currentItem = item;
			showInfoPanel = true;
			Debug.Log(showInfoPanel);

		}

	}

	public void OnPointerExit(PointerEventData eventData)
	{
		
		inventoryManager.currentInfo.currentItem = null;
		showInfoPanel = false;
		Debug.Log(showInfoPanel);

		
	}


	public void OnBeginDrag(PointerEventData eventData)
	{
		if (canDrag)
		{
			inventoryManager.globalAllowCollection = false;
			image.raycastTarget = false;
			parentAfterDrag = transform.parent;
			transform.SetParent(transform.root);
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (canDrag)
		{
			
			transform.position = Input.mousePosition;
			
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (canDrag)
		{
			inventoryManager.globalAllowCollection = true;
			image.raycastTarget = true;
			transform.SetParent(parentAfterDrag);
		}
	}
}

