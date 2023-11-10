using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{

	public bool DebugComments = false;

	#region References
	public GameManager gameManager;
	public EffectManager effectManager;
	public UICardManager UIcardManager;
	#endregion

	#region Card Lists
	public int maxDeckSize = 40;
    public List<Card> playerDeck;
	public List<Card> loadedDeck;
	public List<Card> cardInventory;

	public float timeSinceLastDraw = 0f;
	public float timeBetweenDraws = 30f;

	public Card tremorCard; 
	
	private bool isPlayingCards = false;

	#endregion

	void Start()
    {

		gameManager = GameManager.Instance;
        effectManager = GameObject.Find("Effect Manager").GetComponent<EffectManager>();
        UIcardManager = GameObject.Find("UICardManager").GetComponent<UICardManager>();

		playerDeck = UIcardManager.getCurrentDeck();

		


        loadedDeck = new List<Card>();

        LoadDeck();


        StartCoroutine(PlayCardWithDelay());
		

    }

	void Update()
    {

		nextCardTimer();
	}

	#region Core Functions

	public void PlayCard(Card card)
	{
		if (gameManager != null)
		{
			gameManager.increaseStat("w", card.wardingBonus);
			gameManager.increaseStat("s", card.stabilityBonus);
			gameManager.increaseStat("a", card.ashesBonus);
			gameManager.increaseStat("l", card.lootBonus);
		}

		if (card.singleUse)
		{
			playerDeck.Remove(findCardByID(card.cardID, "playerDeck"));
			if(DebugComments) Debug.Log("Single Use Card was removed");
            if(DebugComments) Debug.Log(playerDeck);

        }


		//play effect
		effectManager.applyEffectsFromCard(card);


    }

	//sets the player's saved deck as the currently loaded deck for the game.
	void LoadDeck()
	{
		if (CheckPlayerDeckSize())
		{

			loadedDeck.Clear();
			loadedDeck.AddRange(playerDeck);
            findAndPlayInstants();

            ShuffleDeck(loadedDeck);
		}
	}

	void ShuffleDeck(List<Card> deck)
	{
		System.Random rng = new System.Random();
		deck.Sort((a, b) => rng.Next(3) - 1);
	}

	bool CheckPlayerDeckSize()
	{

		if (playerDeck != null)
		{

			if (playerDeck.Count <= maxDeckSize)
			{

				return true;
			}
		}

		return false;
	}

	public void AddCard(List<Card> deck, Card card)
	{
		deck.Add(card);
        if (DebugComments) Debug.Log("Added Card " + card.name + " to " + deck);
	}

	#endregion

	#region External Functions

	void AddTremor()
	{
		AddCard(loadedDeck, tremorCard);
		ShuffleDeck(loadedDeck);
	}


	void PlayNextCard()
	{


		if (isPlayingCards && loadedDeck.Count > 0)
		{
			Card nextCard = loadedDeck[0];
			if (nextCard != null)
			{
                if (DebugComments) Debug.Log("Played " + nextCard.name + " and removed from Loaded Deck");
				PlayCard(nextCard);
			}
			
			loadedDeck.RemoveAt(0);
		}


	}

	private Card findCardByID(int id, string Deck)
	{
		if(Deck == "playerDeck")
		{
            foreach (Card card in playerDeck)
            {
                if (card.cardID == id)
                {

                    return card;
                }
            }
        }

		if(Deck == "loadedDeck")
		{
            foreach (Card card in loadedDeck)
            {
                if (card.cardID == id)
                {

                    return card;
                }
            }
        }

        if (DebugComments) Debug.Log("Card Not Found In Player Deck");
		return null;
	}




	IEnumerator PlayCardWithDelay()
	{
		isPlayingCards = true;
		while (loadedDeck.Count > 0)
		{
			yield return new WaitForSeconds(timeBetweenDraws);
			PlayNextCard();
		}
		isPlayingCards = false;
	}


	void nextCardTimer()
	{
		timeSinceLastDraw += Time.deltaTime;
		if (timeSinceLastDraw >= timeBetweenDraws)
		{
			timeSinceLastDraw = 0f;
		}
	}

	void DisplayAllCards(List<Card> cardList)
	{
		foreach (Card card in cardList)
		{
            if (DebugComments) Debug.Log("Card Name: " + card.name);
		}
	}

	public float GetTimeSinceLastDraw()
	{
		return timeSinceLastDraw;
	}

	#endregion


	void findAndPlayInstants()
	{
		List<Card> instants = new List<Card>();
		foreach(Card card in loadedDeck){
			if (card.instant)
			{
                instants.Add(card);

            }
        }

		foreach(Card card in instants)
		{
            loadedDeck.Remove(findCardByID(card.cardID, "loadedDeck"));
            if (DebugComments) Debug.Log("Played Instant Card: " + card);


			PlayCard(card);
        }



    }
}
