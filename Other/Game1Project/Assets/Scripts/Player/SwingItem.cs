using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwingItem : MonoBehaviour
{

	//add input for specific item
	//change direction
	//alternate left and right swings

	private bool allowRepeatSwing = false;
	//private bool waitingForNextSwing = false;

	public int defaultSortingOrder = 1; 
	public int facingUpSortingOrder = -1; 
	//private SpriteRenderer spriteRenderer;

	private float swingSpeed; // 180f = 1 sec, 360f = 0.5 sec
	private float currentSwingAngle;
	[HideInInspector]
	public bool isSwinging = false;
	private float swingStartTime;
	private Quaternion initialRotation;
	private Vector2 previousDirection;

	public ChangeEquippedWeaponSprite weaponSprite;


	public GameObject itemSpriteObject; 


	private Vector2 lastNonZeroDirection;
	
	float targetAngle = 0;
	//float turnSpeed = 15;

	public float swingOffset = -90f; // Offset angle for the swing
	public float rangedRotationOffset = -90f;
	//public PlayerCombat playerCombat; // Reference to the PlayerCombat script
	public Player player; // Reference to the PlayerMovement script
	private float initialAngle;

	void Start()
	{

		initialAngle = transform.rotation.eulerAngles.z;
		// itemSpriteObject.GetComponent<SpriteRenderer>().sprite = equippedItemSprite; // Update the sprite of the child game object
	}

	void Update()
	{
		Vector2 facingDirection = player.playerDirection;
		float angle = Vector2.SignedAngle(Vector2.right, facingDirection);
		swingSpeed = 180f / player.attackSpeed;


		if (isSwinging)
		{
			//Debug.Log(angle);
			if (angle > -45f && angle <= 44f) // Facing right
			{
				//Debug.Log("Right");
				weaponSprite.spriteRenderer.sortingOrder = defaultSortingOrder;
			}
			else if (angle > 44f && angle <= 135f) // Facing up
			{
				//Debug.Log("Up");
				weaponSprite.spriteRenderer.sortingOrder = facingUpSortingOrder;
			}
			else if (angle > 135f || angle <= -135f) // Facing left
			{
				//Debug.Log("Left");
				weaponSprite.spriteRenderer.sortingOrder = defaultSortingOrder;
			}
			else // Facing down
			{
				//Debug.Log("Down");
				weaponSprite.spriteRenderer.sortingOrder = defaultSortingOrder;
			}
		} else if (player.equipped_item != null && player.equipped_item.GetItemType() == ItemType.Ranged_Weapon)
		{
			weaponSprite.spriteRenderer.sortingOrder = defaultSortingOrder;
		}

		// Calculate the target angle based on the current facing direction and swing offset
		targetAngle = angle + swingOffset;
		allowRepeatSwing = player.allowRepeatSwing;
		//allowRepeatSwing = false;
		//if(player.equipped_item.GetItemType() != ItemType.Weapon)


		if (player.canAttack && player.attack.action.ReadValue<float>() > 0.0f && !isSwinging && allowRepeatSwing && player.equipped_item != null && player.equipped_item.GetItemType() == ItemType.Weapon)
		{
			StartSwing();
		}
		else if (player.canAttack && player.attack.action.triggered && !allowRepeatSwing && !isSwinging && player.equipped_item != null && player.equipped_item.GetItemType() == ItemType.Weapon)
		{
			singleStartSwing();
		} else if (player.equipped_item != null && !isSwinging && player.equipped_item.GetItemType() == ItemType.Ranged_Weapon)
		{
			//Debug.Log("Ranged Weapon Equipped!");
			rotateRangedWeapon();
		}

        if (isSwinging && player.canAttack)
        {
            swing();
        }
        else if (isSwinging && !allowRepeatSwing && player.canAttack)
        {
            singleSwing();
        }

		//Hides weapon when not swinging
		if (!isSwinging && player.equipped_item != null && player.equipped_item.GetItemType() != ItemType.Ranged_Weapon)
		{
            weaponSprite.spriteRenderer.enabled = false;
        }



        //Resets the swing if the player's facing direction has changed and the swing is not in progress
        if (facingDirection != previousDirection && !isSwinging)
		{
			resetSwing();
		}
		previousDirection = facingDirection;
		

		
	}

	//Melee Swinging
	void StartSwing()
	{
		isSwinging = true;
		swingStartTime = Time.time;
		initialRotation = transform.rotation;
    }

	void swing()
	{
        weaponSprite.spriteRenderer.enabled = true;
        float elapsedTime = Time.time - swingStartTime;
		currentSwingAngle = swingSpeed * elapsedTime;

		if (currentSwingAngle >= 180f)
		{
			currentSwingAngle = 180f;
			isSwinging = false;
			resetSwing();

		}
		else
		{
			transform.rotation = initialRotation * Quaternion.Euler(0, 0, currentSwingAngle);
		}

	}


	void resetSwing()
	{
        transform.rotation = Quaternion.Euler(0, 0, targetAngle - 180);
	}


	void singleStartSwing()
	{
		isSwinging = true;
		swingStartTime = Time.time;
		initialRotation = transform.rotation;
	}

	void singleSwing()
	{
		float elapsedTime = Time.time - swingStartTime;
		currentSwingAngle = swingSpeed * elapsedTime;

		if (currentSwingAngle >= 180f)
		{
			currentSwingAngle = 180f;
			isSwinging = false;
			resetSwing();

		}
		else
		{
			transform.rotation = initialRotation * Quaternion.Euler(0, 0, currentSwingAngle);
		}

	}

	
	//Ranged Aim Rotation Thing
	void rotateRangedWeapon()
	{
		weaponSprite.spriteRenderer.enabled = true;
		
		Vector3 mousePosition = Input.mousePosition;
		Vector3 direction = (mousePosition - Camera.main.WorldToScreenPoint(transform.position)).normalized;
		float angle = Mathf.Atan2(direction.y, direction.x);
		transform.rotation = Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg + rangedRotationOffset);
	}
}
