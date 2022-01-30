using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType { RANGED, CLOSE, FRIEND_CLOSE, FRIEND_RANGED}

public class EnemyAI : MonoBehaviour
{
    public EnemyType type;
    GameObject player;
    Rigidbody2D rb;
    bool cooldown_activated;

    [Header("Don't Touch!")]
    public GameObject healthBar;
    public GameObject ammoSample;
    public GameObject deathEffect;
    public GameObject chestKey;

    [Header("Enemy Stats")]
    public float enemySpeed;
    public float sightDist;
    public float atkDist;
    public float damage;
    public float maxHealth;
    public float max_cooldown;
    public float currentHealth;
    private float cooldown;

    private float size_x;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        cooldown = max_cooldown;
        if (type == EnemyType.RANGED || type == EnemyType.FRIEND_RANGED)
            atkDist = sightDist;
        else if (type == EnemyType.FRIEND_CLOSE || type == EnemyType.FRIEND_RANGED)
            gameObject.tag = "Friend";

        //ammoSample
        size_x = transform.localScale.x;
    }

    void Update()
    {
        var dist = Vector2.Distance(player.transform.position ,transform.position);
        var friendDist = Vector2.Distance(FindClosestGameObjectWithTag("Friend").transform.position, transform.position);
        var enemyDist = Vector2.Distance(FindClosestGameObjectWithTag("Enemy").transform.position, transform.position);
        
        //Debug.Log(dist);

        if (currentHealth <= 0)
        {
            int rand = Random.Range(1, 2);
            //Debug.Log(rand);
            if (rand == 1)
            {
                chestKey.SetActive(true);
                chestKey.transform.SetParent(null);
                chestKey.transform.position = transform.position;
                chestKey.GetComponent<BoxCollider2D>().enabled = true;
            }

            Death();
        }

        if (dist < sightDist)
        {
            healthBar.SetActive(true);
            healthBar.transform.localScale = new Vector3(currentHealth / maxHealth, healthBar.transform.localScale.y);
            switch (type)
            {
                #region Enemy - Close
                case EnemyType.CLOSE:

                    if (friendDist <= sightDist) // If friend is close
                    {
                        var enemyPos = FindClosestGameObjectWithTag("Friend").transform.position;

                        if (enemyPos.x > transform.position.x) // On right
                        {
                            rb.velocity = new Vector2(enemySpeed * Time.deltaTime, rb.velocity.y);
                            transform.localScale = new Vector3(-size_x, transform.localScale.y);
                        }
                        else if (enemyPos.x < transform.position.x) // On left
                        {
                            rb.velocity = new Vector2(-enemySpeed * Time.deltaTime, rb.velocity.y);
                            transform.localScale = new Vector3(size_x, transform.localScale.y);
                        }
                        if (friendDist <= atkDist)
                        {
                            rb.velocity = Vector2.zero;
                            if (cooldown_activated == false)
                                MeleeAttack();
                        }
                    }
                    else
                    {
                        if (player.transform.position.x > transform.position.x) // On right
                        {
                            rb.velocity = new Vector2(enemySpeed * Time.deltaTime, rb.velocity.y);
                            transform.localScale = new Vector3(-size_x, transform.localScale.y);
                        }
                        if (player.transform.position.x < transform.position.x) // On left
                        {
                            rb.velocity = new Vector2(-enemySpeed * Time.deltaTime, rb.velocity.y);
                            transform.localScale = new Vector3(size_x, transform.localScale.y);
                        }
                        if (dist <= atkDist && player.transform.position.y == transform.position.y)
                        {
                            rb.velocity = Vector2.zero;
                            if (cooldown_activated == false)
                                MeleeAttack();
                        }
                    }                    
                    break;
                #endregion

                #region Enemy - Ranged
                case EnemyType.RANGED:
                    if (player.transform.position.x > transform.position.x) // On right
                    {
                        transform.localScale = new Vector3(-size_x, transform.localScale.y);
                    }
                    if (player.transform.position.x < transform.position.x) // On left
                    {
                        transform.localScale = new Vector3(size_x, transform.localScale.y);
                    }

                    if (dist <= atkDist && cooldown_activated == false)
                    {
                        RangedAttack();
                    }
                    break;
                #endregion

                #region Friend - Close
                case EnemyType.FRIEND_CLOSE:

                    // Following player
                    if (player.transform.position.x > transform.position.x) // On right
                    {
                        rb.velocity = new Vector2(enemySpeed * Time.deltaTime, rb.velocity.y);
                        transform.localScale = new Vector3(-size_x, transform.localScale.y);
                    }
                    if (player.transform.position.x < transform.position.x) // On left
                    {
                        rb.velocity = new Vector2(-enemySpeed * Time.deltaTime, rb.velocity.y);
                        transform.localScale = new Vector3(size_x, transform.localScale.y);
                    }
                    // When gets too close to player
                    if (dist <= atkDist && Mathf.Approximately(transform.position.y, player.transform.position.y))
                    {
                        rb.velocity = Vector2.zero;
                    }

                    if (Vector2.Distance(FindClosestGameObjectWithTag("Enemy").transform.position, transform.position) <= sightDist)
                    {
                        var enemyPos = FindClosestGameObjectWithTag("Enemy").transform.position;

                        if (enemyPos.x > transform.position.x) // On right
                        {
                            rb.velocity = new Vector2(enemySpeed * Time.deltaTime, rb.velocity.y);
                            transform.localScale = new Vector3(-size_x, transform.localScale.y);
                        }
                        else if(enemyPos.x < transform.position.x) // On left
                        {
                            rb.velocity = new Vector2(-enemySpeed * Time.deltaTime, rb.velocity.y);
                            transform.localScale = new Vector3(size_x, transform.localScale.y);
                        }
                        if (enemyDist <= atkDist)
                        {
                            rb.velocity = Vector2.zero;
                            if (cooldown_activated == false)
                                MeleeAttack();
                        }
                    }
                    break;
                #endregion

                #region Friend - Ranged
                case EnemyType.FRIEND_RANGED:
                    if (enemyDist <= sightDist)
                    {
                        if (FindClosestGameObjectWithTag("Enemy").transform.position.x > transform.position.x) // On right
                        {
                            transform.localScale = new Vector3(-2, 4);
                        }
                        if (FindClosestGameObjectWithTag("Enemy").transform.position.x < transform.position.x) // On left
                        {
                            transform.localScale = new Vector3(2, 4);
                        }
                        if (enemyDist <= atkDist && cooldown_activated == false)
                        {
                          RangedAttack();
                        }
                    }
                    break;
                    #endregion
            }
        }
        else
        {
            healthBar.SetActive(false);
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        // Attack Cooldown
        if (cooldown_activated)
        {
            cooldown -= Time.deltaTime;
            if (cooldown <= 0)
            {
                cooldown_activated = false;
                cooldown = max_cooldown;
            }
        }


        #region Animation Stuff

        GetComponent<Animator>().SetFloat("Speed", Mathf.Abs(rb.velocity.x));

        #endregion

    }

    void MeleeAttack()
    {       
        // Play an attack animation
        if (type == EnemyType.CLOSE)
        {
            var friendDist = Vector2.Distance(FindClosestGameObjectWithTag("Friend").transform.position, transform.position);
            var dist = Vector2.Distance(player.transform.position, transform.position);

            if (friendDist < dist) // if the friend is closer
            {
                var enemyAI = FindClosestGameObjectWithTag("Friend").GetComponent<EnemyAI>();
                var enemy_rb = FindClosestGameObjectWithTag("Friend").GetComponent<Rigidbody2D>();

                if (FindClosestGameObjectWithTag("Friend").transform.position.x > transform.position.x) // On right
                {
                    enemy_rb.AddForce(new Vector2(damage * 100, damage * 10));
                }
                if (FindClosestGameObjectWithTag("Friend").transform.position.x < transform.position.x) // On left
                {
                    enemy_rb.AddForce(new Vector2(-damage * 100, damage * 10));
                }
                enemyAI.currentHealth -= damage;
            }
            else if (dist < friendDist) // If player is closer
            {
                var player_combat = player.GetComponent<CombatSystem>();
                var player_rb = player.GetComponent<Rigidbody2D>();

                if (player.GetComponent<DreamMovement>().blocking == false || player_combat.GetComponent<CombatSystem>().attacking == false)
                {
                    Debug.Log("Enemy attack player");
                    player.transform.position = new Vector3(0, 0);
                }
                else if (player.GetComponent<DreamMovement>().blocking && player.transform.position.x > transform.position.x) // On right
                {                                     
                    player.transform.position = new Vector3(player.transform.position.x + damage / 10, player.transform.position.y + damage / 20);
                    //player_rb.AddForce(new Vector2(damage * 100, damage * 10));
                    Debug.Log("on right");
                }
                else if (player.GetComponent<DreamMovement>().blocking && player.transform.position.x < transform.position.x) // On left
                {
                    //player_rb.AddForce(new Vector2(-damage * 100, damage * 10));
                    player.transform.position = new Vector3(player.transform.position.x - damage / 10, player.transform.position.y - damage / 20);
                    Debug.Log("on left"); player.transform.position = new Vector3(0, 0); 
                }
            }

        }
        else if (type == EnemyType.FRIEND_CLOSE)
        {
            var enemyAI = FindClosestGameObjectWithTag("Enemy").GetComponent<EnemyAI>();
            var enemy_rb = FindClosestGameObjectWithTag("Enemy").GetComponent<Rigidbody2D>();

            if (FindClosestGameObjectWithTag("Enemy").transform.position.x > transform.position.x) // On right
            {
                enemy_rb.AddForce(new Vector2(damage * 100, damage * 10));
            }
            if (FindClosestGameObjectWithTag("Enemy").transform.position.x < transform.position.x) // On left
            {
                enemy_rb.AddForce(new Vector2(-damage * 100, damage * 10));
            }

            enemyAI.currentHealth -= damage;
        }

        cooldown_activated = true;
    }

    void RangedAttack()
    {
        var ammo = Instantiate(ammoSample);
        ammo.transform.position = transform.position - new Vector3(transform.localScale.x / 4, 0);
        ammo.GetComponent<AmmoScript>().enabled = true;

        if (transform.localScale.x > 0)
        ammo.GetComponent<AmmoScript>().speed = -ammo.GetComponent<AmmoScript>().speed;
        cooldown_activated = true;
        Debug.Log("shot");
    }

    /*public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance <= distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }*/

    public GameObject FindClosestGameObjectWithTag(string tag)
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag(tag);
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance <= distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    /*public GameObject FindClosestFriend()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Friend");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance <= distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }*/

    void Death()
    {
        Debug.Log("Enemy Died");
        deathEffect.SetActive(true);
        deathEffect.transform.SetParent(null);
        deathEffect.transform.position = transform.position;
        Destroy(gameObject.transform.parent.gameObject);
    }
}
