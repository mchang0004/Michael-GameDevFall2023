using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    [SerializeField]
    private GameObject pickupObject;
    [SerializeField]
    private ParticleSystem pickupParticle;

    void Start()
    {
        pickupParticle.Stop();

    }

    // Update is called once per frame
    void Update()
    {
        if (pickupObject.activeSelf == false)
        {
            Debug.Log(pickupObject.activeSelf);
            pickupParticle.Play();
            Destroy(gameObject, pickupParticle.main.duration);
        }
    }
}
