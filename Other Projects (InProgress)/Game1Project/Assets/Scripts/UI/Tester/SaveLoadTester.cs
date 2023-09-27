using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadTester : MonoBehaviour
{
    Player player;

    void Start()
    {
        player = FindAnyObjectByType<Player>();
    }
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(player);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        player.level = data.playerLevel;
        player.currentHP = data.playerCurrentHP;
        player.gold = data.playerGold;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        player.transform.position = position;
        
    }
}
