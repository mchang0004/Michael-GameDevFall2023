using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UICardManager : MonoBehaviour
{
	public List<Card> allCards;

	public List<Card> UI_playerDeck;
    public List<Card> UI_cardInventory;
    public List<int> IDPlayerDeck;
	public List<int> IDCardInventory;

	public Transform deckTransform;
    public Transform inventoryTransform;

    public GameObject uiCardPrefab;
    public SaveLoad saveLoad;



    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        saveLoad = FindAnyObjectByType<SaveLoad>();
        

	}
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("MainMenu");
        saveLoad.LoadPlayer();
		loadInventoryAndDeckFromIDs();




	}

    // Update is called once per frame
    void Update()
    {

        setCardInventoryAndDeckListbyID();







	}

    public void LoadCards()
    {
        deckTransform = GameObject.Find("Player Deck Area").transform;
        inventoryTransform = GameObject.Find("Inventory Cards Area").transform;

        foreach (Transform card in deckTransform)
        {
            Destroy(card.gameObject);
        }

        foreach (Transform card in inventoryTransform)
        {
            Destroy(card.gameObject);
        }

        foreach (Card card in UI_playerDeck)
        {
            GameObject UICard = Instantiate(uiCardPrefab);
            UICard.transform.SetParent(deckTransform);
            UICard.transform.localScale = new Vector3(1, 1, 1);
            DragCard draggableCard = UICard.GetComponent<DragCard>();
            draggableCard.card = card;
        }

        foreach (Card card in UI_cardInventory)
        {
            GameObject UICard = Instantiate(uiCardPrefab);
            UICard.transform.SetParent(inventoryTransform);
            UICard.transform.localScale = new Vector3(1, 1, 1);
            DragCard draggableCard = UICard.GetComponent<DragCard>();
            draggableCard.card = card;
        }

    }
    public void saveDecks()
    {
        UI_playerDeck.Clear();
        UI_cardInventory.Clear();

        if (deckTransform != null)
        {
            saveCurrentDeck();
        }

        if (inventoryTransform != null)
        {
            saveCurrentInventory();
        }

		

	}
	public void saveCurrentDeck()
    {
        //will need to later pass this to player save data
        foreach (Transform uiCard in deckTransform)
        {
            Card card = uiCard.GetComponent<DragCard>().card;

            UI_playerDeck.Add(card);

        }
    }
    public void saveCurrentInventory()
    {
        foreach (Transform uiCard in inventoryTransform)
        {
            Card card = uiCard.GetComponent<DragCard>().card;

            UI_cardInventory.Add(card);
        }


    }

    public List<Card> getCurrentDeck()
    {
        return UI_playerDeck;
    }

    public void removeCardFromUI(Card card)
    {
        foreach (Transform uiCard in deckTransform)
        {
            if (card.name == uiCard.GetComponent<DragCard>().card.name)
            {
                Destroy(uiCard);
                break;
            }
            else
            {
                Debug.LogError("Card to be removed wasn't found");
            }
        }


    }

    public void addCardToInventory(Card card)
    {
        UI_cardInventory.Add(card);

    }

    public void setCardInventoryAndDeckListbyID()
    {
        IDCardInventory.Clear();
		IDPlayerDeck.Clear();   

		foreach (Card card in UI_cardInventory)
        {
            int id = card.cardID;
			IDCardInventory.Add(id);

		}

		foreach (Card card in UI_playerDeck)
		{
			int id = card.cardID;
			IDPlayerDeck.Add(id);

		}
	}

    public List<int> getCardInventoryByID()
    {
        return IDCardInventory;
    }

	public List<int> getDeckByID()
	{
        return IDPlayerDeck;
	}

    public void loadInventoryAndDeckFromIDs()
    {
        UI_cardInventory.Clear();
		UI_playerDeck.Clear();



		foreach (int id in IDCardInventory)
        {

			foreach (Card card in allCards)
            {
				if (id == card.cardID)
                {
                    UI_cardInventory.Add(card);
                }
			}

		}

        foreach(int id in IDPlayerDeck)
        {
			foreach (Card card in allCards)
			{
                if (id == card.cardID)
                {
                    UI_playerDeck.Add(card);
				}
			}
			
        }
    }
}
