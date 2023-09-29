using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadTester : MonoBehaviour
{
    Player player;
    InventoryManager inventoryManager;

    void Start()
    {
        player = FindAnyObjectByType<Player>();
        inventoryManager = FindAnyObjectByType<InventoryManager>();
    }
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(player);
        //SaveSystem.SaveInventory(inventoryManager);

    }

    public void LoadPlayer()
    {
        //SaveSystem.LoadPlayer();

        PlayerData data = SaveSystem.LoadPlayer();
        //InventoryData data = SaveSystem.LoadPlayer();
        
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
