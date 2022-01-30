using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomType {START, CORRIDOR, VERTICAL_CORRIDOR, PLAY, END}

public class RoomManager : MonoBehaviour
{
    [Header("Room type")]
    public RoomType type;
    public bool bothWays;
    public bool changingRotation;

    [Header("Dungeon Length (Only with Starting room)")]
    public float maxLength;
    public float minLength;

    float boway; // If both spawnPoints will lead to Mr.x
    public float length; // Dungeon's length

    public GameObject[] spawnPoints;

    [Header("---Debug---")]
    public GameObject doorToX;

    private void Start()
    {      
        switch (type)
        {
            #region Starting room
            case RoomType.START:
                length = Random.Range(minLength, maxLength);

                //boway = Random.Range(0, 2);
                boway = 1;

                switch (boway)
                {
                    #region Goes both ways
                    case 0: // If it goes both ways
                        Debug.Log("Both Ways");
                        foreach (GameObject dor in spawnPoints)
                        {
                            dor.GetComponent<SpawnRooms>().leadToX = true;
                        }
                        bothWays = true;
                        break;
                    #endregion

                    #region Goes only one way
                    case 1: // If it DOESN'T go both ways

                        bothWays = false;

                        var numToX = Random.Range(0, spawnPoints.Length);
                        var leadsToX = spawnPoints[numToX].GetComponent<SpawnRooms>();

                        leadsToX.leadToX = true;

                        doorToX = leadsToX.gameObject;
                        break;
                        #endregion
                }

                break;
            #endregion

            #region Vectical Corridor

            case RoomType.VERTICAL_CORRIDOR:

                if (transform.localScale.x < 0) // If the room is on the right side
                {
                    transform.eulerAngles = new Vector3(0, 0, -90);
                    //transform.localScale = new Vector3();
                }

                else // If the room is on the left side
                {
                    transform.eulerAngles = new Vector3(0, 0, 90);
                }

                ////////////// CHANGING ROTATION ///////////////

                if (changingRotation)
                {
                    var val = Random.Range(0, 5);
                    if (val == 0)
                    {
                        if (transform.localScale.x < 0) // if the room is on the right side
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
                }
                break;
            #endregion

            #region Play
            case RoomType.PLAY:
                if (changingRotation)
                {
                    var val = Random.Range(0, 2);
                    if (val == 0)
                    {
                        if (transform.localScale.x < 0) // if the room is on the right side
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
                }

                break;
                #endregion
        }

        foreach (GameObject dor in spawnPoints)
        {
            var spawn = dor.GetComponent<SpawnRooms>();

            spawn.length = length;
        }
    }
}
