using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingItem : MonoBehaviour
{

	//add input for specific item
	//change direction
	//alternate left and right swings

	private bool allowRepeatSwing = false;
	private bool waitingForNextSwing = false;

	public int defaultSortingOrder = 1; // The default sorting order value when not facing up
	public int facingUpSortingOrder = -1; // The sorting order value when facing up
	//private SpriteRenderer spriteRenderer;

	private float swingSpeed; // 180f = 1 sec, 360f = 0.5 sec etc:
	private float currentSwingAngle;
	[HideInInspector]
	public bool isSwinging = false;
	private float swingStartTime;
	private Quaternion initialRotation;
	private Vector2 previousDirection;

	public ChangeEquippedWeaponSprite weaponSprite;


	public GameObject itemSpriteObject; // Reference to the child game object with the sprite renderer


	private Vector2 lastNonZeroDirection;
	
	float targetAngle = 0;
	//float turnSpeed = 15;

	public float swingOffset = -90f; // Offset angle for the swing
									 //public PlayerCombat playerCombat; // Reference to the PlayerCombat script
	public Player player; // Reference to the PlayerMovement script
	private float initialAngle;

	// Start is called before the first frame update
	void Start()
	{

		initialAngle = transform.rotation.eulerAngles.z;
		// itemSpriteObject.GetComponent<SpriteRenderer>().sprite = equippedItemSprite; // Update the sprite of the child game object
	}

	// Update is called once per frame
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
		}

		// Calculate the target angle based on the current facing direction and swing offset
		targetAngle = angle + swingOffset;
		allowRepeatSwing = player.allowRepeatSwing;
		//allowRepeatSwing = false;
		//if(player.equipped_item.GetItemType() != ItemType.Weapon)
		

		if (player.canAttack && player.attack.action.ReadValue<float>() > 0.0f && !isSwinging && allowRepeatSwing && !waitingForNextSwing && player.equipped_item != null && player.equipped_item.GetItemType() == ItemType.Weapon)
		{
			StartSwing();
		}
		else if (player.canAttack && player.attack.action.triggered && !allowRepeatSwing && !isSwinging && !waitingForNextSwing && player.equipped_item != null && player.equipped_item.GetItemType() == ItemType.Weapon)
		{
			singleStartSwing();
		}


		if (isSwinging && player.canAttack)
		{
			weaponSprite.spriteRenderer.enabled = true; // Enable the spriteRenderer when isSwinging is true
			swing();
		}
		else if (isSwinging && !allowRepeatSwing && player.canAttack)
		{
			weaponSprite.spriteRenderer.enabled = true;
			singleSwing();
		}
		else
		{
			weaponSprite.spriteRenderer.enabled = false; // Disable the spriteRenderer when isSwinging is false
		}

		

		

		// Reset the swing if the player's facing direction has changed and the swing is not in progress
		if (facingDirection != previousDirection && !isSwinging)
			{

			resetSwing();
			}

			// Update previousDirection every frame
			previousDirection = facingDirection;
		
	}


	void StartSwing()
	{

		isSwinging = true;
		swingStartTime = Time.time;
		initialRotation = transform.rotation;

		waitingForNextSwing = true;
	}

	void swing()
	{

		float elapsedTime = Time.time - swingStartTime;
		currentSwingAngle = swingSpeed * elapsedTime;

		if (currentSwingAngle >= 180f)
		{
			currentSwingAngle = 180f;
			isSwinging = false;
			resetSwing();

			waitingForNextSwing = false;
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

}
