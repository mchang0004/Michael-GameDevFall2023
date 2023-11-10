using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UICardManager : MonoBehaviour
{
    public List<Card> UIplayerDeck;
    public List<Card> UIcardInventory;

    public Transform deckTransform;
    public Transform inventoryTransform;

    public GameObject uiCardPrefab;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("MainMenu");

        


    }

    // Update is called once per frame
    void Update()
    {
       

        if (Input.GetKeyDown(KeyCode.Space))
        {
            deckTransform = GameObject.Find("Player Deck Area").transform;
            inventoryTransform = GameObject.Find("Inventory Cards Area").transform;
            LoadCards();
            //loadDecks();
        }

    
    }

    public void LoadCards()
    {
        foreach(Card card in UIplayerDeck)
        {
            GameObject UICard = Instantiate(uiCardPrefab);
            UICard.transform.SetParent(deckTransform);
            DragCard draggableCard = UICard.GetComponent<DragCard>();
            draggableCard.card = card;
        }

        foreach (Card card in UIcardInventory)
        {
            GameObject UICard = Instantiate(uiCardPrefab);
            UICard.transform.SetParent(inventoryTransform);
            DragCard draggableCard = UICard.GetComponent<DragCard>();
            draggableCard.card = card;
        }
    }
    public void loadDecks()
    {
        UIplayerDeck.Clear();
        UIcardInventory.Clear();

        if (deckTransform != null)
        {
            LoadCurrentDeck();
        }

        if (inventoryTransform != null)
        {
            LoadCurrentInventory();
        }
    }
    public void LoadCurrentDeck()
    {
        //will need to later pass this to player save data
        foreach (Transform uiCard in deckTransform)
        {
            Card card = uiCard.GetComponent<DragCard>().card;
            
            UIplayerDeck.Add(card);

        }
        Debug.Log("Deck: " + UIplayerDeck);
    }
    public void LoadCurrentInventory()
    {
        foreach (Transform uiCard in inventoryTransform)
        {
            Card card = uiCard.GetComponent<DragCard>().card;

            UIcardInventory.Add(card);
        }

        Debug.Log("Inventory: " + UIcardInventory);

    }

    public List<Card> getCurrentDeck()
    {
        return UIplayerDeck;
    }

    public void removeCardFromUI(Card card)
    {
        foreach(Transform uiCard in deckTransform)
        {
            if(card.name == uiCard.GetComponent<DragCard>().card.name)
            {
                Destroy(uiCard);
                break;
            } else
            {
                Debug.LogError("Card to be removed wasn't found");
            }
        }


    }
}
