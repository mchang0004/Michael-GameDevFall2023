using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButtons : MonoBehaviour
{
	public GameManager gameManager;


	void Start()
    {
		gameManager = GameManager.Instance;

	}


	public void generateNoiseTest()
	{
		gameManager.increaseStat("w", -1);
	}

	public void generateCrumbleTest()
	{
		gameManager.increaseStat("s", -1);
	}
}
