using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3f;
    public Vector3 offset = new Vector3(0f, 0f, 0f);

    private Vector2 velocity = Vector2.zero;
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    private void FixedUpdate()
    {
        Vector3 targetPosition = target.position + offset;

        transform.position = Vector2.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }


}

