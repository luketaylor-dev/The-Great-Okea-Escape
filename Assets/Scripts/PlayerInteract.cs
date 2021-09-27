using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerInteract : MonoBehaviour
    {
        public float interactDistance;
        public Transform destination;
        public LayerMask playerMask;
        
        private Camera mainCamera;
        
        private PickUp holdingObject;
        

        private void Start()
        {
            mainCamera = Camera.main;
        }

        // Update is called once per frame
        void Update(){
            
            Debug.DrawRay(mainCamera.transform.position, mainCamera.transform.TransformDirection(Vector3.forward) * 10);
            if (holdingObject == null && Input.GetMouseButtonDown(0))
            {
                Debug.Log("MouseDown");
                if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.TransformDirection(Vector3.forward),out var hit, interactDistance,  ~playerMask))
                {
                    Debug.Log(hit.transform.gameObject);
                    if (hit.distance < interactDistance)
                    {
                        holdingObject = hit.transform.gameObject.GetComponent<PickUp>();
                        
                        if (holdingObject != null)
                        {
                            holdingObject.GrabObject(destination);
                        }
                    }
                }
            }

            if (holdingObject != null && Input.GetMouseButtonDown(1))
            {
                holdingObject.StopHold();
                holdingObject.GetComponent<Rigidbody>().AddForce(mainCamera.transform.forward * holdingObject.power);
                holdingObject = null;
            }
        }
    }

}