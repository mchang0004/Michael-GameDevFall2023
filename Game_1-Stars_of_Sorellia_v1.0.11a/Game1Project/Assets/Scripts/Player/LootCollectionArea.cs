using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootCollectionArea : MonoBehaviour
{
    public float walkingRadius = 1f;
    public float standingRadius = 0.5f;
    public CircleCollider2D colliderArea;
	public PlayerMovement playerMovement;
    // Update is called once per frame
    void Update()
    {
		StartCoroutine(collectionRadius());
	}

    private IEnumerator collectionRadius()
    {
		if (playerMovement.isMoving)
		{

			yield return new WaitForSeconds(0.5f);

            colliderArea.radius = walkingRadius;

		}
		else
		{
			yield return new WaitForSeconds(0.1f);

            colliderArea.radius = standingRadius;

		}

        colliderArea.radius = 0f; //temp reset to check if collectable item is on player
	}

	
}
