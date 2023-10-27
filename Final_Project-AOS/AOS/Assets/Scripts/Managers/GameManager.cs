using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	
	private static GameManager instance;


	#region Stat Values

	//[HideInInspector]
	public int currentWarding;
	public int currentStability;
	public int currentAshes;
	public int currentLoot;

	public static int defaultMaxWarding = 10;
	public static int defaultMaxStability = 10;
	public static int defaultMaxAshes = 10;
	public static int defaultMaxLoot = 10;

	private static int startingAnger = 10;
	private int anger;
	#endregion



	public static GameManager Instance
    { 
        get 
        {
            if (instance == null)
            {
                Debug.LogError("GameManager is null");
            }
            return instance;
        
        }

    }

    private void Awake()
    {
        instance = this;
		anger = startingAnger;	

	}


    public bool HasArtifact { get; set; }


	public void increaseStat(string stat, int amount)
	{
		switch (stat)
		{
			case "w":
				if (amount > 0) currentWarding += amount;
				if (amount < 0) generateNoise(-amount);
				break;
			case "s":
				if (amount > 0) currentStability += amount;
				if (amount < 0) generateCrumble(-amount);
				break;
			case "a":
				currentAshes += amount;
				break;
			case "l":
				currentLoot += amount;
				break;	
		}

		clampStats();
	}

	#region Warding/Noise/Anger

	public void generateNoise(int amount)
	{
		
		if (currentWarding >= amount)
		{
			currentWarding -= amount;
		}
		else
		{
			int damageToAnger = amount - currentWarding;
			currentWarding = 0;
			anger -= damageToAnger;


			if(anger > 0)
			{
				Debug.Log("Anger Level: " + anger);
			}
			else if (anger == 0)
			{
				Debug.Log("Spirits are Angry!");
				
			}
			else if (anger < 0)
			{
				damageTemple();
			}
		}
		

		

	}


	public bool spiritAngered()
	{
		if (anger == 1)
		{
			return true;
		}
		return false;
	}


	#endregion



	#region Stability/Crumble/Temple Damage
	public void generateCrumble(int amount)
	{
		if (currentStability >= amount)
		{
			currentStability -= amount;
		}
		else
		{
			int damageToTemple = amount - currentStability;
			currentStability = 0;

			// Call a function to handle the damage multiple times
			for (int i = 0; i < damageToTemple; i++)
			{
				damageTemple();
			}
		}
	}

	private void damageTemple()
	{
		
		Debug.Log("Temple took damage.");
		
	}

	#endregion

	


	public void clampStats()
    {

		//clamp to max


		if (currentWarding > defaultMaxWarding)
        {
			currentWarding = defaultMaxWarding;
		}

		if (currentStability > defaultMaxStability)
		{
			currentStability = defaultMaxStability;
		}

		if (currentAshes > defaultMaxAshes)
		{
			currentAshes = defaultMaxAshes;
		}

		if (currentLoot > defaultMaxLoot)
		{
			currentLoot = defaultMaxLoot;
		}

		//clamp to 0

		if (currentWarding < 0)
		{
			currentWarding = 0;
		}

		if (currentStability < 0)
		{
			currentStability = 0;
		}

		if (currentAshes < 0)
		{
			currentAshes = 0;
		}

		if (currentLoot < 0)
		{
			currentLoot = 0;
		}

	}

}
