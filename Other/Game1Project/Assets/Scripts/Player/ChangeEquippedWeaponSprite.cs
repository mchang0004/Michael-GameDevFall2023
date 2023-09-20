using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEquippedWeaponSprite : MonoBehaviour
{

	public InventoryManager inventoryManager;

	public SpriteRenderer spriteRenderer;
	
	public Sprite backup;

    void Update()
    {
		ChangeSprite();
	}

    void ChangeSprite()
    {
		if (inventoryManager.GetSelectedItem(false) != null && (inventoryManager.GetSelectedItem(false).GetItemType() == ItemType.Weapon || inventoryManager.GetSelectedItem(false).GetItemType() == ItemType.Ranged_Weapon))
		{
			Debug.Log("Sprite is Shown");
			spriteRenderer.enabled = true;
			spriteRenderer.sprite = inventoryManager.GetSelectedItem(false).image;
		} else
		{
			//Debug.Log("Sprite is Hidden");
			//spriteRenderer.sprite = backup;
			spriteRenderer.enabled = false;
		}
	


	}
}
