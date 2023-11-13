using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerStats : MonoBehaviour
{
	public int totalShells;



	void Awake()
	{
		DontDestroyOnLoad(this);
	}



	

	public void addShells(int amount)
	{
		totalShells += amount;
	}

	public void removeShells(int amount)
	{
		totalShells -= amount;
	}


}
