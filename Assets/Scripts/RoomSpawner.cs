using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;

    private RoomTemplates templates;

    public int rand;
    private bool spawned = false;
    // Start is called before the first frame update
    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        //Invoke("Spawn", 0.1f);
    }
  
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            Debug.Log("Key");
        }
        if (!spawned && Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("KeyDown");
            if (openingDirection == 1)
            {
                // BOTTOM DOOR
                rand = Random.Range(0, templates.bottomRooms.Length);
                Instantiate(templates.bottomRooms[rand], transform.position,
                    templates.bottomRooms[rand].transform.rotation);
            }

            else if (openingDirection == 2)
            {
                // TOP DOOR
                rand = Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[rand], transform.position,
                    templates.topRooms[rand].transform.rotation);
            }

            else if (openingDirection == 3)
            {
                //LEFT DOOR
                rand = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position,
                    templates.leftRooms[rand].transform.rotation);
            }

            else if (openingDirection == 4)
            {
                // RIGHT DOOR
                rand = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position,
                    templates.rightRooms[rand].transform.rotation);
            }

            spawned = true;
            //Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            try
            {
                if (!other.GetComponent<RoomSpawner>().spawned && !spawned)
                {
                    //spawn wall blocking
                    Debug.Log($"Object: {gameObject.transform.parent.gameObject} had a collision with {other.transform.parent.gameObject}");
                    Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                    Destroy(gameObject);

                }

                spawned = true;
            }
            catch
            {
                //ignore
            }
        }
    }
}
