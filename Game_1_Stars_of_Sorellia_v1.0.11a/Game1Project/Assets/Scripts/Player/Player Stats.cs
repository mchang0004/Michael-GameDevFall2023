using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{


	public float Strength = 0f;
	public float Dexterity = 0f;
	public float Constitution = 0f;
	public float Intelligence = 0f;
	public float Wisdom = 0f;
	public float Charisma = 0f;
	
	public int level;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



	public void setStat(string type, float amount)
	{
		switch (type)
		{
			case "Strength":
				Strength = amount; break;
			case "Dexterity":
				Dexterity = amount; break;
			case "Constitution":
				Constitution = amount; break;
			case "Intelligence":
				Intelligence = amount; break;
			case "Wisdom":
				Wisdom = amount; break;
			case "Charisma":
				Charisma = amount; break;				
		}
	}

	public float getStat(string type)
	{
		switch (type)
		{
			case "Strength":
				return Strength;
			case "Dexterity":
				return Dexterity;
			case "Constitution":
				return Constitution;
			case "Intelligence":
				return Intelligence;
			case "Wisdom":
				return Wisdom;
			case "Charisma":
				return Charisma;
			default:
				return 0f;
		}
	}

	public float getStatForLevel(int level, string type)
	{
		return 0f;
		//maybe make a new class that holds each stat by level
	}
}
