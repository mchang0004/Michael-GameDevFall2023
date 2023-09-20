using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*https://discussions.unity.com/t/layermask-vs-1-gameobject-layer/21919*/

public class Arrow : MonoBehaviour
{
	public LayerMask enemyLayer;

	private void OnCollisionEnter(Collision other)
	{
		if ((enemyLayer.value & (1 << other.gameObject.layer)) != 0)
		{
			Debug.Log("Collided with Enemy");
			Destroy(gameObject);
		}
		
		
	}
}
