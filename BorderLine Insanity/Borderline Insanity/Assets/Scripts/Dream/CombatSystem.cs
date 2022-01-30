using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    Rigidbody2D rb;

    [Header("Stats")]
    public float maxHealth;
    public float currentHealth;
    public float atkDamage;
    public float atkDistance;
    public float maximumTimer = 1f;
    private float currentTimer;

    public bool attacking, timer_acttive;

    private void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        currentTimer = maximumTimer;
    }

    void Update()
    {

        if (Input.GetKeyDown("f"))
        {
            MeleeAttack();
            if (timer_acttive)
            {
                currentTimer -= Time.deltaTime;
                if (currentTimer <= 0)
                {
                    attacking = false;
                    timer_acttive = false;
                }
            }
        }

        if (GetComponent<DreamMovement>().blocking)
        {
            rb.velocity = new Vector2(0,rb.velocity.y);
        }

        if (timer_acttive)
        {
            currentTimer -= Time.deltaTime;
            if (currentTimer <= 0)
            {
                attacking = false;
                timer_acttive = false;
            }
        }
    }

    void MeleeAttack()
    {
        if (transform.localScale.x > 0) // looking right
        {
            rb.velocity = new Vector2(atkDistance * 4, 2);//Vector2.right * (atkDistance * 4);
        }
        else // looking left
        {
            rb.velocity = new Vector2(-atkDistance * 4, 2);
        }
        attacking = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (attacking)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                atkDamage = collision.gameObject.GetComponent<EnemyAI>().currentHealth;
                collision.gameObject.GetComponent<EnemyAI>().currentHealth -= atkDamage;
                Debug.Log("attacking");
                attacking = false;
            }
            else
            {
                timer_acttive = true;
                attacking = false;
            }
        }
    }
}
