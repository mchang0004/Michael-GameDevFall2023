using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{
	public GameObject player;
	public GameObject artifactLocation;
	public Transform compassTransform;
	public Camera playerCamera;
	public float detectionDistance = 1f;
	
	private bool onLocation = false;
	public Color defaultColor = new Color(1f, 1f, 1f, 0.78f);
	public Color activeColor = new Color(1f, 0.843f, 0f, 0.78f);

	void Start()
	{
		player = GameObject.Find("Player");
		artifactLocation = GameObject.Find("Artifact_Location");
		playerCamera = Camera.main;
		compassTransform = transform;
		compassTransform.GetComponent<Image>().color = defaultColor;

	}

	void Update()
	{
		Vector3 cameraForward = playerCamera.transform.forward;
		
		Vector3 direction = artifactLocation.transform.position - player.transform.position;
		

		cameraForward.y = 0f;
		direction.y = 0f; 

		float angle = Vector3.SignedAngle(cameraForward, direction, Vector3.up);
		compassTransform.rotation = Quaternion.Euler(50f, 0f, -angle);

		float distance = direction.magnitude;

		if (distance <= detectionDistance)
		{
			if (!onLocation)
			{
				compassTransform.GetComponent<Image>().color = activeColor;
				onLocation = true;
			}
		}
		else
		{
			if (onLocation)
			{
				compassTransform.GetComponent<Image>().color = defaultColor;
				onLocation = false;
			}
		}

	}
}
