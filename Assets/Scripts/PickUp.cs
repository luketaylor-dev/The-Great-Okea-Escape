using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PickUp : MonoBehaviour
{
    private Transform world;

    public float power;
    private Camera mainCamera;
    // Start is called before the first frame update
    private void Start()
    {
        mainCamera  = Camera.main;
        world = transform.parent;
    }

    public void GrabObject(Transform destination)
    {
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().freezeRotation = true;  
        GetComponent<Rigidbody>().isKinematic = true;  
        transform.position = destination.position;
        transform.rotation = destination.rotation;
        transform.parent = destination;
    }


    public void StopHold()
    {
        transform.parent = world;
        GetComponent<BoxCollider>().enabled = true;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().freezeRotation = false;  
        GetComponent<Rigidbody>().isKinematic = false;  

    }
}
