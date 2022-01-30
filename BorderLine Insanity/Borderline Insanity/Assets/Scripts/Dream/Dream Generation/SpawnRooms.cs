using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRooms : MonoBehaviour
{
    [Header("If the rooms leads to Mr.X")]
    public bool leadToX;

    [Header("Dungeon's size")]
    public float length;

    [Header("Rooms to spawn")]
    public GameObject[] rooms;

    [Header("End rooms")]
    public GameObject mrX_room;
    public GameObject end_room; // The other end

    [Header("Spawned room")]
    public GameObject room;

    float spawnTime = 0.0f;

    [Header("If the room will spawn more rooms")]
    public bool isSpawning = true;

    private bool stopSpawning;

    [Header("Spawn Points")]
    public bool goingLeft;

    private void Start()
    {
        stopSpawning = false;
        if (goingLeft)
        {
            if (transform.localPosition.x >= 0)
            {
                GetComponent<SpawnRooms>().isSpawning = false;
                GetComponent<SpawnRooms>().enabled = false;
            }
        }
        else
        {
            if (transform.localPosition.x == 0)
            {
                GetComponent<SpawnRooms>().isSpawning = false;
                GetComponent<SpawnRooms>().enabled = false;
            }
        }
    }

    void FixedUpdate()
    {
        if (stopSpawning == false)
        spawnTime -= Time.deltaTime;

        if (spawnTime <= 0 && stopSpawning == false)
        {
            Spawn();
            stopSpawning = true;
        }
    }

    public void Spawn()
    {
        if (isSpawning)
        {
            if (length > 0)
            {
                #region Spawning
                Debug.Log("Spawning Room");

                //var rooms = Resources.LoadAll<GameObject>("Dream/Rooms/LeftRooms");

                var roomID = Random.Range(0, rooms.Length);

                room = Instantiate(rooms[roomID], transform.parent.position, Quaternion.identity);
                if (goingLeft == false)
                {
                    room.transform.localScale = new Vector3(-1, room.transform.localScale.y);
                }
                #endregion

                room.GetComponent<RoomManager>().length = --length;

                room.transform.position = transform.position;

                #region UP/DOWN spawning
                /*if (room.GetComponent<RoomManager>().changingRotation) // If the room can change it's rotation
                {
                    var val = Random.Range(0, 2);

                    switch (room.GetComponent<RoomManager>().type) // Checks which type of room is that
                    {
                        /*case RoomType.VERTICAL_CORRIDOR:
                            if (val == 0)
                            {
                                if (room.transform.localScale.x < 0) // if the room is on the right side
                                {
                                    transform.eulerAngles = new Vector3(0, 0, -90);
                                    transform.localScale = new Vector3(1, 1);
                                }

                                else // if the room is on the left side
                                {
                                    transform.eulerAngles = new Vector3(0, 0, 90);
                                    transform.localScale = new Vector3(-1, 1);
                                }
                            }
                            break;*/

                        /*case RoomType.PLAY:
                            if (room.transform.localScale.x < 0) // if the room is on the right side
                            {
                                transform.eulerAngles = new Vector3(0, 0, 180);
                                transform.localScale = new Vector3(1, 1);
                            }

                            else // if the room is on the left side
                            {
                                transform.eulerAngles = new Vector3(0, 0, 180);
                                transform.localScale = new Vector3(-1, 1);
                            }
                            break;
                    }
                }*/
        
                #endregion
                if (goingLeft)
                {
                    foreach (GameObject gam in room.GetComponent<RoomManager>().spawnPoints)
                    {
                        gam.GetComponent<SpawnRooms>().goingLeft = true;
                    }
                }

                switch (leadToX)
                {
                    case true:
                        foreach(GameObject gam in room.GetComponent<RoomManager>().spawnPoints)
                        {
                            gam.GetComponent<SpawnRooms>().leadToX = true;
                        }
                        break;


                    case false:
                        room.GetComponentInChildren<SpawnRooms>().leadToX = false;
                        break;
                }

            }
            else if (length <= 0)
            {
                if (leadToX)
                {
                    room = Instantiate(mrX_room, transform.position, Quaternion.identity);

                    room.transform.position = transform.position;
                    if (goingLeft == false)
                    {
                        room.transform.localScale = new Vector3(-1, room.transform.localScale.y);
                    }
                }
                else
                {
                    room = Instantiate(end_room, transform.position, Quaternion.identity);

                    room.transform.position = transform.position;
                    if (goingLeft == false)
                    {
                        room.transform.localScale = new Vector3(-1, room.transform.localScale.y);
                    }
                }
            }
        }

        GetComponent<SpawnRooms>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<SpawnRooms>())
        {
            collision.GetComponent<SpawnRooms>().isSpawning = false;
        }
    }
}
