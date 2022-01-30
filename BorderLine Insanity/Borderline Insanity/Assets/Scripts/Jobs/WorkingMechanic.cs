using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WorkingMechanic : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed, finalSalar;

    public GameObject statusTXT, salaryTXT;
    public WorkManager workManager;

    public bool goRight, goLeft;

    bool submit, timerActivated;

    float timer = 3;
    int bonusPoints;

    string currentScene;

    private void Start()
    {
        transform.position = new Vector3(Random.Range(-30,30), transform.position.y);
        currentScene = SceneManager.GetActiveScene().name;

        submit = false;

        rb = GetComponent<Rigidbody2D>();
        var rand = Random.value; //Random.Range(-2, 1);
        if (rand >= 0.5f)
        {
            goRight = true;
        }
        else if (rand < 0.5f)
        {
            goLeft = true;
        }
    }

    void Update()
    {
        if (Input.GetKey("g"))
        {
            PlayerPrefs.SetInt("timesWorked", 0);
            PlayerPrefs.SetInt("bonusPt", 0);
        }


        if (timerActivated)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                /*if (PlayerPrefs.GetInt("timesWorked") >= 4)
                {                    
                    /*SceneManager.LoadScene(0); // Home scene
                    PlayerPrefs.SetInt("timesWorked", 0);
                    PlayerPrefs.SetInt("bonusPt", 0);
                }*/
                SceneManager.LoadScene(currentScene);
            }
        }             

        if (transform.position.x >= 16)
        {
            goLeft = true;
            goRight = false;
        }
        if (transform.position.x <= -16)
        {
            goLeft = false;
            goRight = true;
        }


        if (Input.GetKeyDown("e") && PlayerPrefs.GetInt("timesWorked") < 5)
        {
            rb.velocity = Vector2.zero;
            submit = true;
            GetComponent<BoxCollider2D>().enabled = true;
        }

        if (PlayerPrefs.GetInt("timesWorked") >= 4)
        {
            if (Input.GetKey("space"))
            {
                SceneManager.LoadScene(0); // Home scene
                PlayerPrefs.SetInt("timesWorked", 0);
                PlayerPrefs.SetInt("bonusPt", 0);
            }
        }

        if (submit == false)
        {
            if (goRight)
            {
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, speed, .5f * Time.deltaTime), 0);
            }
            else if (goLeft)
            {
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, -speed, .5f * Time.deltaTime), 0);
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (submit && PlayerPrefs.GetInt("timesWorked") < 4)
        {
            statusTXT.SetActive(true);
            timerActivated = true;
            PlayerPrefs.SetInt("timesWorked",  1 + PlayerPrefs.GetInt("timesWorked"));
            Debug.Log("times worked : " + PlayerPrefs.GetInt("timesWorked"));
            switch (collision.name)
            {
                case "Very":
                    bonusPoints += 4;
                    statusTXT.GetComponent<TextMeshProUGUI>().text = "Great job!!";
                    break;

                case "Good":
                    bonusPoints += 2;
                    statusTXT.GetComponent<TextMeshProUGUI>().text = "Good job!";
                    break;

                case "Fine":
                    statusTXT.GetComponent<TextMeshProUGUI>().text = "Fine";
                    break;

                case "Bad":
                    bonusPoints -= 1;
                    statusTXT.GetComponent<TextMeshProUGUI>().text = "Okay... that's bad";
                    break;
            }
            Debug.Log("bonus pt : " + PlayerPrefs.GetInt("bonusPt"));
            PlayerPrefs.SetInt("bonusPt",bonusPoints + PlayerPrefs.GetInt("bonusPt"));
        }     
        else if (submit && PlayerPrefs.GetInt("timesWorked") >= 4)
        {
            finalSalar = workManager.salary + ((workManager.salary / 25) * PlayerPrefs.GetInt("bonusPt"));
            salaryTXT.SetActive(true);
            salaryTXT.GetComponent<TextMeshProUGUI>().text = "Your today's payment : " + finalSalar;
            PlayerPrefs.SetFloat("Salary",finalSalar);
            Debug.Log(finalSalar);
        }
    }
}
