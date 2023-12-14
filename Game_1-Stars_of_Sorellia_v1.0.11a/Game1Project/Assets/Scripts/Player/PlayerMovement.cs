using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.U2D.Path.GUIFramework;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public bool canMove = true;
    public float normalSpeed = 1.5f;
    public float sprintSpeed = 3f;
    public float slowWalkSpeed = 1f;
    private float currentSpeed;
    private float lastDirection;
    private Rigidbody2D rb;
    public Animator animator;

    public InputActionAsset playerControls;

    private Vector2 pointerInput, movementInput;

    public bool isMoving = false;

    public Vector2 Direction { get; private set; }

	

	[SerializeField]
    private InputActionReference movement, attack, pointerPosition, sprint, slowWalk, stopMoving;


	void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
	{
		movementInput = movement.action.ReadValue<Vector2>();
		Vector2 direction = movementInput;

		bool isSprinting = sprint.action.ReadValue<float>() > 0;
		bool isSlowWalking = slowWalk.action.ReadValue<float>() > 0;
		bool isStopped = stopMoving.action.ReadValue<float>() > 0;


		float movementAnimator = 0;

		// Direction for passing to PlayerCombat Script
		Direction = direction;

		// Debug.Log("direction: " + getLastDirection());
		if (canMove)
        {
            

            //Handles idle to movement switching
            
            if (Mathf.Abs(direction.x) > 0 || Mathf.Abs(direction.y) > 0)  //is moving
            {
                isMoving = true;

				animator.SetFloat("Horizontal", direction.x);
                animator.SetFloat("Vertical", direction.y);
                movementAnimator = 1;
                animator.SetFloat("Speed", movementAnimator);
                SaveLastDirection(direction);
               
			}
            else  //not moving
            {

                isMoving = false;
                animator.SetFloat("Horizontal", getLastDirection().x);
                animator.SetFloat("Vertical", getLastDirection().y);
                movementAnimator = 0;
                animator.SetFloat("Speed", movementAnimator);
				
			}
            if (isMoving)
            {
				if (isSprinting)
				{
					currentSpeed = sprintSpeed;
				}
				else if (isSlowWalking)
				{
					currentSpeed = slowWalkSpeed;
				}
				else if (isStopped)
				{
					currentSpeed = 0f;

				}
				else
				{
					currentSpeed = normalSpeed;
				}
			}
           


            rb.velocity = direction * currentSpeed;

		} else
        {
            currentSpeed = 0f;
			rb.velocity = direction * currentSpeed;


			isMoving = false;
			animator.SetFloat("Horizontal", getLastDirection().x);
			animator.SetFloat("Vertical", getLastDirection().y);
			movementAnimator = 0;
			animator.SetFloat("Speed", movementAnimator);
		}
	}



    void SaveLastDirection(Vector2 direction)
    {
        if (direction.x != 0 || direction.y != 0) //This only updates lastDirection if the direction is not zero
        {
            lastDirection = direction.x > 0 ? 1 :         // RightIdle
                            direction.x < 0 ? 0.33f :     // LeftIdle
                            direction.y > 0 ? 0.55f :     // UpIdle
                            direction.y < 0 ? 0.77f : 0;  // DownIdle
        }
    }

    //Used in the Player update script to get direction/offset for swinging
	public Vector3 getLastDirection()
	{
		Vector3 directionVector = Vector3.zero;

		if (lastDirection == 1) // RightIdle
		{
			directionVector = Vector3.right;
		}
		else if (lastDirection == 0.33f) // LeftIdle
		{
			directionVector = Vector3.left;
		}
		else if (lastDirection == 0.55f) // UpIdle
		{
			directionVector = Vector3.up;
		}
		else if (lastDirection == 0.77f) // DownIdle
		{
			directionVector = Vector3.down;
		}

		return directionVector;
	}

	private Vector3 GetPointerInput()
    {
        Vector3 mousePos = pointerPosition.action.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane; //Only if using perspective camera not ortho
        return Camera.main.ScreenToWorldPoint(mousePos); //this is to convert mouse to screenspace (aka the monitor size/resultion).
    }

}
