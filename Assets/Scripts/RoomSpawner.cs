using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        InvokeRepeating("Spawn", 0.4f, 10f);

    }
    
    // Update is called once per frame
    void Spawn()
    {
        if (!spawned)
        {
            if (templates.rooms.Count > templates.MaxSize)
            {
                if (openingDirection == 1)
                {
                    Instantiate(templates.OpenBottom, transform.position, templates.OpenBottom.transform.rotation);
                    templates.rooms.Add(templates.OpenBottom);
                }
                else if (openingDirection == 2)
                {
                    Instantiate(templates.OpenTop, transform.position, templates.OpenTop.transform.rotation);
                    templates.rooms.Add(templates.OpenTop);

                }
                else if (openingDirection == 3)
                {
                    Instantiate(templates.OpenLeft, transform.position, templates.OpenLeft.transform.rotation);
                    templates.rooms.Add(templates.OpenLeft);


                }
                else if (openingDirection == 4)
                {
                    Instantiate(templates.OpenRight, transform.position, templates.OpenRight.transform.rotation);
                    templates.rooms.Add(templates.OpenRight);


                }
            }
            else
            {
                Debug.Log("KeyDown");
                if (openingDirection == 1)
                {
                    // BOTTOM DOOR
                    rand = Random.Range(0, templates.bottomRooms.Length);
                    var room = Instantiate(templates.bottomRooms[rand], transform.position,
                        templates.bottomRooms[rand].transform.rotation);
                    templates.rooms.Add(room);
                }

                else if (openingDirection == 2)
                {
                    // TOP DOOR
                    rand = Random.Range(0, templates.topRooms.Length);
                    var room = Instantiate(templates.topRooms[rand], transform.position,
                        templates.topRooms[rand].transform.rotation);
                    templates.rooms.Add(room);

                }

                else if (openingDirection == 3)
                {
                    //LEFT DOOR
                    rand = Random.Range(0, templates.leftRooms.Length);
                    var room = Instantiate(templates.leftRooms[rand], transform.position,
                        templates.leftRooms[rand].transform.rotation);
                    
                    templates.rooms.Add(room);

                }

                else if (openingDirection == 4)
                {
                    // RIGHT DOOR
                    rand = Random.Range(0, templates.rightRooms.Length);
                    var room =Instantiate(templates.rightRooms[rand], transform.position,
                        templates.rightRooms[rand].transform.rotation);
                    templates.rooms.Add(room);

                }
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
                    Instantiate(templates.closedRoom, transform.position, templates.closedRoom.transform.rotation);
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
    // private void OnTriggerEnter(Collider other)
    // {
    //     if (!other.CompareTag("Room") ) 
    //     {
    //         try
    //         {
    //             if (!other.GetComponent<RoomSpawner>().spawned && !spawned)
    //             {
    //                 if (openingDirection < other.GetComponent<RoomSpawner>().openingDirection)
    //                 {
    //                     var doubleRooms = new List<GameObject>();
    //                     if (openingDirection == 1 || other.GetComponent<RoomSpawner>().openingDirection == 1)
    //                     {
    //                         doubleRooms.AddRange(templates.bottomRooms);
    //                     }
    //                     if(openingDirection == 2 || other.GetComponent<RoomSpawner>().openingDirection == 2)
    //                     {
    //                         doubleRooms.AddRange(templates.topRooms);
    //                     }
    //
    //                     if (openingDirection == 3 || other.GetComponent<RoomSpawner>().openingDirection == 3)
    //                     {
    //                         doubleRooms.AddRange(templates.leftRooms);
    //                     }
    //
    //                     if (openingDirection == 4 || other.GetComponent<RoomSpawner>().openingDirection == 4)
    //                     {
    //                         doubleRooms.AddRange(templates.rightRooms);
    //                     }
    //                     
    //                     var allDuplicateRooms = doubleRooms.GroupBy(x => x)
    //                         .Where(g => g.Count() > 1)
    //                         .Select(y => y.Key)
    //                         .ToList();
    //                     //spawn wall blocking
    //                     Debug.Log($"Object: {gameObject.transform.parent.gameObject} had a collision with {other.transform.parent.gameObject}");
    //                     rand = Random.Range(0, allDuplicateRooms.Count);
    //                     Instantiate(allDuplicateRooms[rand], transform.position, allDuplicateRooms[rand].transform.rotation);
    //                     //Destroy(other.gameObject);
    //                     Destroy(gameObject);
    //                     
    //
    //                 }
    //             }
    //             spawned = true;
    //         }
    //         catch
    //         {
    //             //ignore
    //         }
    //     }
    //     else
    //     {
    //         Destroy(gameObject);
    //     }
    // }
}
