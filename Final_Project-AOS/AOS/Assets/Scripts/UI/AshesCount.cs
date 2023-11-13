using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AshesCount : MonoBehaviour
{

	public PlayerStats playerStats;
	public TextMeshProUGUI ashesCountUI;

	void Start()
    {
		playerStats = FindAnyObjectByType<PlayerStats>();
		ashesCountUI = GameObject.Find("Ash Count").GetComponent<TextMeshProUGUI>();

	}

	void Update()
    {
        ashesCountUI.text = playerStats.totalAshes.ToString();
    }
}
