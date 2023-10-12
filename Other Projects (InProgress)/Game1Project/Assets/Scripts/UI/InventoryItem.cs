
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

[System.Serializable]
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
	[HideInInspector] public int count;
	[HideInInspector] public Transform parentAfterDrag;

	public Player player;

	public bool canDrag = false;


	public bool isDragging = false;
	//private static bool isAnyItemDragging = false;

	void Awake()
	{
		player = GameObject.FindAnyObjectByType<Player>();
        inventoryManager = FindAnyObjectByType<InventoryManager>();
    }

	// Start is called before the first frame update
	public void InitializeItem(Item newItem)
	{
		item = newItem;
		image.sprite = newItem.image;
        slot = this.GetComponentInParent<InventorySlot>();

        //Debug.Log(" Initialized Item Count:" + count);
		RefreshCount();
		//Debug.Log(item);

		//Fixed bug where new item is added when inventory is open, and the newly added item cannot be dragged
		if (inventoryManager.inventoryShown)
		{
			canDrag = true;

        } else
		{
			canDrag = false;

        }
	}

	public void RefreshCount()
	{
		countText.text = count.ToString();
		bool textActive = count > 1;
		countText.gameObject.SetActive(textActive);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		slot = this.GetComponentInParent<InventorySlot>();

        inventoryManager.currentlyHoveredItem = this;
        //Debug.Log(this.getSlot() + " Current Item Hovering: " + inventoryManager.currentlyHoveredItem);
        


        if (item != null && inventoryManager.inventoryMenu.activeSelf)
		{
			inventoryManager.currentInfo.currentItem = item;
			inventoryManager.itemInfoPanel.SetActive(true);

			
		}

	}

	public void OnPointerExit(PointerEventData eventData)
	{
        slot = this.GetComponentInParent<InventorySlot>();

        inventoryManager.currentlyHoveredItem = null;
        //Debug.Log("No Item Hovering");


        if (item != null)
		{
			inventoryManager.currentInfo.currentItem = null;
			inventoryManager.itemInfoPanel.SetActive(false);

		}

	}


	public void OnBeginDrag(PointerEventData eventData)
	{
		if (!canDrag)
			return;
		isDragging = true;
		inventoryManager.globalAllowCollection = false;
		image.raycastTarget = false;
		parentAfterDrag = transform.parent;
		transform.SetParent(transform.root);
		player.inventoryAudio.Play();
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (!canDrag)
			return;

		transform.position = Input.mousePosition;
			
		
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		isDragging = false;
		if (canDrag)
		{
			inventoryManager.globalAllowCollection = true;
			image.raycastTarget = true;
			transform.SetParent(parentAfterDrag);
			player.inventoryAudio.Play();

		}
	}

	public Item getItem()
	{
		return item;

    }

	public InventorySlot getSlot()
	{
        return slot;
	}
}

