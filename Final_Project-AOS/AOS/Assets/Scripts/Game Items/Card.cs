using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Card")]
public class Card : ScriptableObject
{



	[Header("Card Info")]
	public string cardName;
	public string cardCost;
	public int cardID;
	public string Rarity;
	public int maxCount;

	[TextArea(4, 5)]
	public string description;
	public Sprite cardImage;

	public bool instant;
	public bool singleUse = false;

	[Header("Bonuses")]
	public int wardingBonus;
	public int stabilityBonus;
	public int ashesBonus;
	public int lootBonus;

    [Header("Effect")]
    //give effect for duration
    public float effectDuration;
	public List<Effect> effects;

	

	

}
