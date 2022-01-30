using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomContents : MonoBehaviour
{
    [Header("Positioning")]
    public Transform[] enemySpawnPoints;

    public Transform[] chestSpawnPoints;

    [Header("Prefabs")]
    public GameObject enemyPrefab;

    public GameObject chestPrefab;

    [Header("Stats")]
    public int chanceToSpawnChest; // set it to 0 for 100% chance

    [Header("Fog")]
    public FogOfWarScript fog;
    public Color fogColor;

    GameObject enemy;

    private void Start()
    {
        #region Enemy Spawning
        if (enemySpawnPoints.Length > 0)
        {
            int rand = Random.Range(0, enemySpawnPoints.Length);
            enemy = Instantiate(enemyPrefab, enemySpawnPoints[rand].position, Quaternion.identity);

            var enem = Random.Range(0,4);
            if (enem == 0)
            {
               enemy.GetComponentInChildren<EnemyAI>().type = EnemyType.RANGED;
            }

            enemy.GetComponentInChildren<EnemyAI>().enabled = false;
            //enemy.transform.position = enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length)].position;
            /*if (transform.localScale.x < 0)
                enemy.transform.localScale = new Vector3(-1, 1);*/

        }
        #endregion

        #region Chest Spawning
        if (chestSpawnPoints.Length > 0)
        {
            if (Random.Range(0, chanceToSpawnChest) == 0)
            {
                Debug.Log("Chests spawn points : " + chestSpawnPoints.Length + "| from : " + gameObject.name);
                switch(chestSpawnPoints.Length)
                {
                    case 1:
                        Instantiate(chestPrefab, chestSpawnPoints[0].position, Quaternion.identity);
                        break;

                    default:
                        int rand = Random.Range(0, chestSpawnPoints.Length);
                        Instantiate(chestPrefab, chestSpawnPoints[rand].position, Quaternion.identity);
                        break;
                }
            }           
        }
        #endregion     
    }

    private void Update()
    {
        #region FOWS (Fog of war script)
        if (fog.explored)
        {
            if (enemy != null)
            enemy.GetComponentInChildren<EnemyAI>().enabled = true;
        }
        else
        {
            fog.GetComponent<SpriteRenderer>().color = new Color(fogColor.r, fogColor.g, fogColor.b);
        }
        #endregion
    }
}
