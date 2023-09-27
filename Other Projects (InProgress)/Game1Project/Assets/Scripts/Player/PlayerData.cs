using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * https://www.youtube.com/watch?v=XOjd_qU2Ido
 */

[System.Serializable]
public class PlayerData
{
    public int playerLevel;
    public float playerCurrentHP;
    public int playerGold;
    public float[] position;

    public PlayerData(Player player)
    {
        playerLevel = player.level;
        playerCurrentHP = player.currentHP;
        playerGold = player.gold;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;



    }

}
