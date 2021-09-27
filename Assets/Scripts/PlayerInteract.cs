using System;
using UnityEditor;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float interactDistance;
    public float throwForce;
    public Transform world;
    public Transform destination;
    public LayerMask playerMask;

    private Camera mainCamera;
    private GameObject holdingObject;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(mainCamera.transform.position, mainCamera.transform.TransformDirection(Vector3.forward) * 10);
        if (holdingObject == null && Input.GetMouseButtonDown(0))
        {
            Debug.Log("click");
            if (Physics.Raycast(mainCamera.transform.position,
                mainCamera.transform.TransformDirection(Vector3.forward), out var hit, interactDistance,
                ~playerMask))
            {
                Debug.Log(hit.distance);
                if (hit.distance < interactDistance)
                {
                    if (hit.transform.gameObject.GetComponent<Rigidbody>() != null)
                    {
                        Debug.Log(hit.transform.gameObject);
                        GrabObject(hit.transform.gameObject);
                    }
                }
            }
        }

        if (holdingObject == null && Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(mainCamera.transform.position,
                mainCamera.transform.TransformDirection(Vector3.forward), out var hit, interactDistance,
                ~playerMask))
            {
                if (hit.distance < interactDistance)
                {
                    ThrowObject(hit.transform.gameObject);
                }
            }
        }

        if (holdingObject != null && Input.GetMouseButtonDown(1))
        {
            StopHoldAndThrow();
        }

        if (holdingObject != null && Input.GetKeyDown("e"))
        {
            StopHold();
        }
    }

    private void GrabObject(GameObject gObject)
    {
        holdingObject = gObject;
        if (holdingObject != null && holdingObject.GetComponent<Rigidbody>() != null)
        {
            //holdingObject.GetComponent<BoxCollider>().enabled = false;
            holdingObject.GetComponent<Rigidbody>().useGravity = false;
            holdingObject.GetComponent<Rigidbody>().freezeRotation = true;
            holdingObject.GetComponent<Rigidbody>().isKinematic = true;
            holdingObject.transform.position = destination.position;
            holdingObject.transform.rotation = destination.rotation;
            holdingObject.transform.parent = destination;
        }
    }

    private void ThrowObject(GameObject gObject)
    {
        Debug.Log("throwCall");
        if (gObject != null && gObject.GetComponent<Rigidbody>() != null)
        {
            Debug.Log("throwForce");
            gObject.GetComponent<Rigidbody>().AddForce(mainCamera.transform.forward * throwForce);
        }
    }

    private void StopHold()
    {
        if (holdingObject != null)
        {
            ResetObject();
            holdingObject = null;
        }
    }

    private void StopHoldAndThrow()
    {
        if (holdingObject != null)
        {
            ResetObject();
            ThrowObject(holdingObject);
            holdingObject = null;
        }
    }

    private void ResetObject()
    {
        holdingObject.transform.parent = world;
        //holdingObject.GetComponent<BoxCollider>().enabled = true;
        holdingObject.GetComponent<Rigidbody>().useGravity = true;
        holdingObject.GetComponent<Rigidbody>().freezeRotation = false;
        holdingObject.GetComponent<Rigidbody>().isKinematic = false;
    }
}