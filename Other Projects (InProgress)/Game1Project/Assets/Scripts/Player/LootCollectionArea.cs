using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootCollectionArea : MonoBehaviour
{
    public float walkingRadius = 1f;
    public float standingRadius = 0.5f;
    public CircleCollider2D collider;
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

			collider.radius = walkingRadius;

		}
		else
		{
			yield return new WaitForSeconds(0.1f);

			collider.radius = standingRadius;

		}

		collider.radius = 0f; //temp reset to check if collectable item is on player
	}

	
}
