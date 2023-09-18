using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
	public GameObject winTextObject, instructions;
    public GameObject[] score;
    

    public Vector2 stopInput;

    private int count;
    private Rigidbody rb;
    private float movementX;
    private float movementY;

    [SerializeField]
    private InputActionReference stopped;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        
		rb = GetComponent<Rigidbody>();

		winTextObject.SetActive(false);

        
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;

	}

    void StopMovement(InputAction.CallbackContext context)
    {

    }

    void updateScoreGUI()
    {
        GameObject currentCounter = score[score.Length - count];
        currentCounter.SetActive(false);
        if (count >= 12)
        {
            winTextObject.SetActive(true);
        }
    }



	void FixedUpdate()
    {
        bool isStopped = stopped.action.ReadValue<float>() > 0;

        if (isStopped)
        {
            rb.velocity = new Vector3(0, 0, 0);
        }

        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
		rb.AddForce(movement * speed);


        if(rb.velocity.z != 0 || rb.velocity.x != 0 || isStopped)
        {
            instructions.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
			other.gameObject.SetActive(false);
            count++;
            updateScoreGUI();


        }

        

	}

    

}
