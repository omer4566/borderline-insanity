using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoScript : MonoBehaviour
{
    public float speed, dmg;

    GameObject player;
    Vector3 startPos;
    Rigidbody2D rb;

    float timeToDie = 6;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        startPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        //transform.position = new Vector3(transform.position.x,transform.position.y * Random.Range(0,31));
    }

    private void Update()
    {
        timeToDie -= Time.deltaTime;

        if (timeToDie <= 0)
        {
            Destroy(gameObject);
        }

        /*if (startPos.x < targetPos.x) // if the bullet is on the right
            rb.velocity = Vector2.left * speed;

        if (startPos.x > targetPos.x) // if the bullet is on the left
            rb.velocity = Vector2.right * speed;*/

        rb.velocity = Vector2.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);       

        switch (collision.tag)
        {
            #region Player Collision
            case "Player":
                if (player.GetComponent<DreamMovement>().blocking == false)
                {
                    collision.GetComponent<CombatSystem>().currentHealth -= dmg;

                    rb = collision.GetComponent<Rigidbody2D>();
                    // If target was on the right
                    if (transform.position.x > player.transform.position.x)
                    {
                        Debug.Log("Hit right");
                        rb.AddForce(new Vector2(-dmg * 25, 40));
                    }
                    // If target was on the left
                    else if (transform.position.x < player.transform.position.x)
                    {
                        Debug.Log("Hit left");
                        rb.AddForce(new Vector2(dmg * 25, 40));
                    }
                    player.transform.position = new Vector3(0, 0);
                }
                break;
            #endregion
            #region Enemy Collision
            case "Enemy":

                collision.GetComponent<EnemyAI>().currentHealth -= dmg;

                rb = collision.GetComponent<Rigidbody2D>();
                // If target was on the right
                if (transform.position.x > collision.transform.position.x)
                {
                    Debug.Log("Hit right");
                    rb.AddForce(new Vector2(-dmg * 25, 40));
                }
                // If target was on the left
                else if (transform.position.x < collision.transform.position.x)
                {
                    Debug.Log("Hit left");
                    rb.AddForce(new Vector2(dmg * 25, 40));
                }
                
                break;
            #endregion
            #region Friend Collision
            case "Friend":
                collision.GetComponent<EnemyAI>().currentHealth -= dmg;

                rb = collision.GetComponent<Rigidbody2D>();
                // If target was on the right
                if (transform.position.x > collision.transform.position.x)
                {
                    Debug.Log("Hit right");
                    rb.AddForce(new Vector2(-dmg * 25, 40));
                }
                // If target was on the left
                else if (transform.position.x < collision.transform.position.x)
                {
                    Debug.Log("Hit left");
                    rb.AddForce(new Vector2(dmg * 25, 40));
                }

                break;
            #endregion
        }
        Destroy(gameObject); // Destroys current game object, just like my life
        
    }
}
