using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public GameObject destructableObject;

    private bool isThrown = false;
    private int count  = 0;
    public void MakeDestructable()
    {
        isThrown  = true;
    }

    public void OnCollisionEnter(Collision other)
    {
        if (isThrown && count == 0)
        {
            count++;
            Instantiate(destructableObject, transform.position, transform.rotation);
            Destroy(gameObject);
            Debug.Log("Somehow We are here");
        }
    }
}
