
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
	private Coroutine hideCoroutine;
	private GameObject draggingItem;
	private Transform originalParent;

	public bool canDrag = false;


	public bool isDragging = false;
	//private static bool isAnyItemDragging = false;

	void Awake()
	{
		player = GameObject.FindAnyObjectByType<Player>();
	}

	// Start is called before the first frame update
	public void InitializeItem(Item newItem)
	{
		item = newItem;
		image.sprite = newItem.image;
		RefreshCount();
		//Debug.Log(item);
	}

	public void RefreshCount()
	{
		countText.text = count.ToString();
		bool textActive = count > 1;
		countText.gameObject.SetActive(textActive);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (item != null && inventoryManager.inventoryMenu.activeSelf)
		{
			inventoryManager.currentInfo.currentItem = item;
			inventoryManager.itemInfoPanel.SetActive(true);
			Debug.Log("showing");
			
		}

	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (item != null)
		{
			inventoryManager.currentInfo.currentItem = null;
			inventoryManager.itemInfoPanel.SetActive(false);
			Debug.Log("exit");
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
		}
	}
}

