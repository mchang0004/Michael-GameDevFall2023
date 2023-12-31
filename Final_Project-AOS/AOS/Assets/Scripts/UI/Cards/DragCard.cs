using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
//https://www.youtube.com/watch?v=zMKUfI8VE2I 

public class DragCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	private RectTransform cardTransform;
	private CanvasGroup canvasGroup;
	private Transform lastDraggedArea;
	private Transform playerDeckArea;
	private Transform playerInventoryArea;
	private Canvas canvas;
	private UICardManager UIcardManager;
	private SaveLoad saveLoad;


	public Card card;

	public TextMeshProUGUI cardNameText;
	public TextMeshProUGUI Card_Description;

	private void Start()
	{
		cardTransform = GetComponent<RectTransform>();
		playerDeckArea = GameObject.Find("Player Deck Area").transform;
		playerInventoryArea = GameObject.Find("Inventory Cards Area").transform;
		canvasGroup = GetComponent<CanvasGroup>();
		canvas = GameObject.Find("MenuUI").GetComponent<Canvas>();
		UIcardManager = GameObject.Find("UICardManager").GetComponent<UICardManager>();
		saveLoad = FindAnyObjectByType<SaveLoad>();

		cardNameText.text = card.name;
		Card_Description.text = card.description;
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		canvasGroup.blocksRaycasts = false;
		lastDraggedArea = transform.parent;
		transform.SetParent(transform.root);
	}

	public void OnDrag(PointerEventData eventData)
	{
		cardTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
	}

	public void OnEndDrag(PointerEventData eventData)
	{


		canvasGroup.blocksRaycasts = true;

		if (RectTransformUtility.RectangleContainsScreenPoint(playerDeckArea.GetComponent<RectTransform>(), eventData.position))
		{
			transform.SetParent(playerDeckArea);
		}
		else if (RectTransformUtility.RectangleContainsScreenPoint(playerInventoryArea.GetComponent<RectTransform>(), eventData.position))
		{
			transform.SetParent(playerInventoryArea);
		}
		else
		{
			transform.SetParent(lastDraggedArea);
		}

		UIcardManager.saveDecks();

		StartCoroutine(SaveAfterDelay());
	}

	IEnumerator SaveAfterDelay()
	{
		yield return new WaitForSeconds(0.5f);
		saveLoad.SavePlayer();
	}
}