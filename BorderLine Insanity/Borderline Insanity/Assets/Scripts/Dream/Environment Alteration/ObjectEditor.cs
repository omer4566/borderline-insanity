using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ObjectEditor : MonoBehaviour
{
    public float damage, weight;
    public TextMeshProUGUI damage_txt, weight_txt;

    public GameObject editWindow_obj;

    DreamManager dreamManager;

    private void Start()
    {
        dreamManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<DreamManager>();
    }

    private void Update()
    {
        damage_txt.text = "Damage : " +damage.ToString();

        weight_txt.text = "Weight : " + weight.ToString();

        GetComponent<Rigidbody2D>().mass = weight;

        if (dreamManager.dreamEditMode== false)
        {
            editWindow_obj.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        if (dreamManager.dreamEditMode)
            editWindow_obj.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var combat = collision.gameObject.GetComponent<CombatSystem>();
            combat.currentHealth -= damage;

            var rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.AddForce(Vector2.up * damage * 10);
        }
        else if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Friend"))
        {
            var ai = collision.gameObject.GetComponent<EnemyAI>();
            ai.currentHealth -= damage;

            var rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.AddForce(Vector2.up * damage * 10);
        }
    }


    public void Object_damage(float newDamage)
    {
        damage = newDamage;
    }

    public void Object_weight(float newWeight)
    {
        weight = newWeight;
    }
}
