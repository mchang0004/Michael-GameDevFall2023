using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  https://forum.unity.com/threads/serialize-custom-classes.102072/
 *  https://stackoverflow.com/questions/36852213/how-to-serialize-and-save-a-gameobject-in-unity
 * 
 */

[System.Serializable]
public class InventoryData
{   

    public InventorySlot[] slots;


   
    public InventoryData(InventoryManager inventoryManager)
    {
        slots = inventoryManager.inventorySlots;
        
    }


}
