using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using TMPro;

public class DreamManager : MonoBehaviour
{
    [Range(0, 100)]
    public float lucidityLevel;
    [Range(-100, 100)]
    public float default_saturation;

    [Header("Player Attributes")]
    // Direct numbers  
    public float change_health;
    public float change_speed;

    // Player change UI
    public GameObject health_txt,speed_txt;

    ColorGrading grading;
    LensDistortion lensDistortion;

    public bool dreamEditMode;

    GameObject player;

    [Header("DO NOT TOUCH")]
    public GameObject dreamEdit_UI, gravityValue_txt, timeLeft_TXT;

    // UI ELEMENTS
    float gravity_change;
    public float timeLeft;

    // Start is called before the first frame update
    void Start()
    {
        #region Post Processing
        PostProcessVolume volume = GameObject.FindGameObjectWithTag("PostProcess").GetComponent<PostProcessVolume>();

        volume.profile.TryGetSettings(out grading);
        volume.profile.TryGetSettings(out lensDistortion);

        grading.saturation.value = default_saturation;

        gravity_change = Physics2D.gravity.y;
        #endregion
        player = GameObject.FindGameObjectWithTag("Player");

        if (PlayerPrefs.GetInt("takenTime") == 0)
        {
            timeLeft = 5 * 60;
        }
        else
        {
            timeLeft = (PlayerPrefs.GetInt("takenTime") * 3) * 60;
        }

        change_health = player.GetComponent<CombatSystem>().maxHealth;
        change_speed = player.GetComponent<DreamMovement>().speed; 

        health_txt.GetComponentInChildren<Slider>().maxValue = player.GetComponent<CombatSystem>().maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        var timeString = string.Format("{0:0}:{1:00}", Mathf.Floor(timeLeft / 60), timeLeft % 60);
        timeLeft_TXT.GetComponent<TextMeshProUGUI>().text = timeString;//timeLeft.ToString("00");
        health_txt.GetComponent<TextMeshProUGUI>().text = "Health : " + change_health.ToString();
        speed_txt.GetComponent<TextMeshProUGUI>().text = "Speed : " + change_speed.ToString();

        if (dreamEditMode)
        {
            player.GetComponent<CombatSystem>().currentHealth = change_health;
            player.GetComponent<DreamMovement>().speed = change_speed;
        }        

        if (timeLeft <= 0)
        {
            //PlayerPrefs.SetInt("takenTime" , 6);
            SceneManager.LoadScene(0);
        }

        if (lucidityLevel > 50)
        {
            /*if (dreamEditMode == false && Input.GetKeyDown("q"))
            {
                dreamEditMode = true;
                Debug.Log("Dream editmode enabled");
            }
            else if (dreamEditMode && Input.GetKeyDown("q"))
            {
                dreamEditMode = false;
                Debug.Log("Dream editmode disabled");
            }*/
            if (Input.GetKeyDown("q"))
            {
                dreamEditMode = true;
                Debug.Log("Dream editmode enabled");
                dreamEdit_UI.SetActive(true);
            }
            else if (Input.GetKeyUp("q"))
            {
                dreamEdit_UI.SetActive(false);
                dreamEditMode = false;
                Debug.Log("Dream editmode disabled");
            }
        }
    }

    private void LateUpdate()
    {
        if (dreamEditMode)
        {
            lensDistortion.intensity.value = Mathf.Lerp(lensDistortion.intensity.value, -42.5f, 1f * Time.deltaTime);
            Time.timeScale = Mathf.Lerp(Time.timeScale, 0.0f, 1f * Time.deltaTime);
            grading.saturation.value = Mathf.Lerp(grading.saturation.value, -40.2f, 1f * Time.deltaTime);
        }
        if (dreamEditMode == false)
        {
            lensDistortion.intensity.value = Mathf.Lerp(lensDistortion.intensity.value, 0, 2f * Time.deltaTime);
            Time.timeScale = Mathf.Lerp(Time.timeScale, 1, 2f * Time.deltaTime);
            grading.saturation.value = Mathf.Lerp(grading.saturation.value, default_saturation, 2f * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        if (dreamEditMode)
        {
            Physics2D.gravity = new Vector2(Physics2D.gravity.x, gravity_change);
            var graviTXT = Mathf.RoundToInt(-gravity_change);

            gravityValue_txt.GetComponent<TextMeshProUGUI>().text = graviTXT.ToString();
        }
    }

    public void GravityChange(float newGravity)
    {
        gravity_change = newGravity;
    }

    public void HealthChange(float newHealth)
    {
        change_health = newHealth;
    }

    public void SpeedChange(float newSpeed)
    {
        change_speed = newSpeed;
    }
}
