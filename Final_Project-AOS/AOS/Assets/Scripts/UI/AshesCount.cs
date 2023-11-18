using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AshesCount : MonoBehaviour
{

	public PlayerSaveStats playerSaveStats;
	public TextMeshProUGUI ashesCountUI;

	void Start()
    {
		playerSaveStats = FindAnyObjectByType<PlayerSaveStats>();
		ashesCountUI = GameObject.Find("Ash Count").GetComponent<TextMeshProUGUI>();

	}

	void Update()
    {
        ashesCountUI.text = playerSaveStats.totalAshes.ToString();
    }
}
