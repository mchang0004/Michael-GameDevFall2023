using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

	// Update is called once per frame
	void Update()
	{
		Camera yourCamera = Camera.main;

		if (yourCamera != null)
		{
			Ray ray = yourCamera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit))
			{
				GameObject hitObject = hit.transform.gameObject;
				Debug.Log("Mouse is over: " + hitObject.name);
			}
		}
	}
}
