using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EnemyDreamChanger : MonoBehaviour
{
    [Header("Don't touch")]

    public GameObject health_TXT, damage_TXT, sight_TXT,name_TXT, enemy_EditOBJ;

    bool isFriend;

    DreamManager dreamManager;
    EnemyAI enemy;
    // UI ////////////////

    public Slider slider_health;
    public TMP_Dropdown dropdown_type;
    float health_change, damage_change, sight_change;

    // UI ////////////////

    bool editMode;

    private void Start()
    {
        enemy = GetComponent<EnemyAI>();
        if (gameObject.CompareTag("Friend"))
        {
            isFriend = true;
        }
        else
        {
            isFriend = false;
        }
    }

    private void OnMouseDown()
    {
        if (Input.GetKey("q"))
        {
            dreamManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<DreamManager>();
        }

        if (dreamManager.dreamEditMode)
        {
            Debug.Log("Editing the enemy");
            enemy_EditOBJ.SetActive(true);
            editMode = true;

            health_change = enemy.currentHealth;
            damage_change = enemy.damage;
            sight_change = enemy.sightDist;

            name_TXT.GetComponent<TextMeshProUGUI>().text = tag;

            ///// UI //////          
            slider_health.maxValue = enemy.maxHealth;
            slider_health.onValueChanged.AddListener(Enemy_HealthChange);

            if (enemy.type == EnemyType.RANGED)
            {
                dropdown_type.value = 0;
            }
            else
            {
                dropdown_type.value = 1;
            }
        }
    }

    private void Update()
    {
        if (editMode)
        {
            enemy.currentHealth = health_change;
            enemy.damage = damage_change;
            enemy.sightDist = sight_change;

            damage_TXT.GetComponent<TextMeshProUGUI>().text = "Damage : " + Mathf.RoundToInt(enemy.damage);
            health_TXT.GetComponent<TextMeshProUGUI>().text = "Health : " + Mathf.RoundToInt(enemy.currentHealth);
            sight_TXT.GetComponent<TextMeshProUGUI>().text = "Sight : " + Mathf.RoundToInt(enemy.sightDist);

            if (dreamManager.dreamEditMode == false)
            {
                editMode = false;
            }
        }          
       
        if (editMode == false)
        {
            enemy_EditOBJ.SetActive(false);
        }
    }

    public void Enemy_HealthChange(float newHealth)
    {
        if (editMode)
            health_change = newHealth;
    }

    public void Enemy_DamageChange(float newDamage)
    {
        if (editMode)
            damage_change = newDamage;
    }

    public void Enemy_SightChange(float newSight)
    {
        if (editMode)
            sight_change = newSight;
    }

    public void Enemy_TypeChange(int val)
    {
        if (isFriend)
        {
            switch (val)
            {
                case 0:
                    enemy.type = EnemyType.FRIEND_RANGED;
                    enemy.atkDist = enemy.sightDist;
                    break;
                case 1:
                    enemy.type = EnemyType.FRIEND_CLOSE;
                    break;
            }       
        }
        else
        {
            switch (val)
            {
                case 0:
                enemy.type = EnemyType.RANGED;
                enemy.atkDist = enemy.sightDist;
                    break;
                case 1:
                    enemy.type = EnemyType.CLOSE;
                    break;
            }
        }
    }

    public void Enemy_ChangeSide()
    {
        if (isFriend)
        {
            isFriend = false;
            enemy.type = EnemyType.CLOSE;
            gameObject.tag = "Enemy";
            name_TXT.GetComponent<TextMeshProUGUI>().text = gameObject.tag;
        }
        else if (isFriend == false)
        {
            isFriend = true;
            enemy.type = EnemyType.FRIEND_CLOSE;
            gameObject.tag = "Friend";
            name_TXT.GetComponent<TextMeshProUGUI>().text = gameObject.tag;
        }
                 
        
    }
}
