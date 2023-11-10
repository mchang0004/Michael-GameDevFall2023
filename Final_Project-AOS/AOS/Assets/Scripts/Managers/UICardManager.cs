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

        foreach (Card card in UIplayerDeck)
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
    public void saveDecks()
    {
        UIplayerDeck.Clear();
        UIcardInventory.Clear();

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
            
            UIplayerDeck.Add(card);

        }
        Debug.Log("Deck: " + UIplayerDeck);
    }
    public void saveCurrentInventory()
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
