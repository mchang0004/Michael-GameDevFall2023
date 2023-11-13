using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	
	private static GameManager instance;
	public MapManager mapManager;



	#region Stat Values

	//[HideInInspector]
	public int currentWarding;
	public int currentStability;
	public int currentAshes;
	public int currentLoot;

	public int currentFloor;

	public static int defaultMaxWarding = 10;
	public static int defaultMaxStability = 10;
	public static int defaultMaxAshes = 10;
	public static int defaultMaxLoot = 10;

	public float timeBetweenCrumble = 30f;
	public float timeBetweenLootDeque = 1f;
    public float timeBetweenAshesDeque = 1f;

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
		mapManager = GameObject.Find("Map Manager").GetComponent<MapManager>();
        instance = this;
		anger = startingAnger;	
		StartCoroutine(crumbleTimed());
		StartCoroutine(lootDequedTimed());
        StartCoroutine(ashDequedTimed());


    }


	public bool HasArtifact { get; set; }

	void Update()
	{

	}

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
		Debug.Log("## Generated Noise");
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
				spawnSpirits();
				
				
			}
			else if (anger < 0)
			{
				damageTemple();
			}
		}
		

		

	}

	public void spawnSpirits()
	{
		Debug.Log("Spirits are Angry!");
	}



	#endregion



	#region Stability/Crumble/Temple Damage
	IEnumerator crumbleTimed()
	{
		while (true)
		{
			generateCrumble(1);
			Debug.Log("Crumble Was Generated");
			yield return new WaitForSeconds(timeBetweenCrumble);
		}
	}

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

			for (int i = 0; i < damageToTemple; i++)
			{
				damageTemple();
			}
		}
	}

    
    private void damageTemple()
	{
		//  1/18th chance for each hazard door/passage/trap
		//  list of list of each hazard trap (probably make a scriptable object for a few possible hazard locations to randomly close)
		Debug.Log("Temple took damage.");
		
	}





    #endregion


    #region loot/ashes

	public void dequeLoot()
	{
		
		currentLoot--;
		mapManager.spawnLootByFloor(currentFloor);


    }

    public void dequeAshes()
    {

        currentAshes--;
        mapManager.spawnAshByFloor(currentFloor);


    }

    IEnumerator lootDequedTimed()
    {
        while (true)
        {
			Debug.Log("Tried to Drop Loot, Current Loot: " + currentLoot);
			if(currentLoot > 0)
			{
                dequeLoot();
                Debug.Log("Loot Was Dropped");
            }
            yield return new WaitForSeconds(timeBetweenLootDeque);
        }
    }
    IEnumerator ashDequedTimed()
    {
        while (true)
        {
            Debug.Log("Tried to Drop Ashes, Current Ashes: " + currentAshes);
            if (currentAshes > 0)
            {
                dequeAshes();
                Debug.Log("Ashes Were Dropped");
            }
            yield return new WaitForSeconds(timeBetweenAshesDeque);
        }
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
