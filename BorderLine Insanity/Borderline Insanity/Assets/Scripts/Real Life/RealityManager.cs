using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Security.Cryptography;

public enum Day { Sunday , Monday , Tuesday , Wednesday , Thursday , Friday , Saturday}
public enum TimeOfDay { Morning, Noon , AfterNoon, Evening, Night};

public class RealityManager : MonoBehaviour
{
    public TextMeshProUGUI moneyTXT, dayTXT, timeTXT;

    public Day day;
    public TimeOfDay timeOfDay;
    public int baseHours, minutes;
    public float money;

    private void Start()
    {
        //baseHours = baseHours += PlayerPrefs.GetInt("takenTime");
        //PlayerPrefs.SetInt("hourTime", PlayerPrefs.GetInt("hourTime") + PlayerPrefs.GetInt("takenTime"));
        //newHour = baseHours + PlayerPrefs.GetInt("takenTime");
        //PlayerPrefs.SetFloat("Money", money + PlayerPrefs.GetFloat("Salary"));
        Physics2D.gravity = new Vector2(0,-20);

        /* PlayerPrefs.SetFloat("Money", PlayerPrefs.GetFloat("Money") + PlayerPrefs.GetFloat("Salary"));
         PlayerPrefs.SetFloat("Salary",0);*/
        #region Time Calculation
        if (PlayerPrefs.GetInt("actualTime") == 0)
        {
            PlayerPrefs.SetInt("actualTime", baseHours + PlayerPrefs.GetInt("takenTime") + PlayerPrefs.GetInt("actualTime"));
        }
        else
        {
            PlayerPrefs.SetInt("actualTime", PlayerPrefs.GetInt("takenTime") + PlayerPrefs.GetInt("actualTime"));
        }

        PlayerPrefs.SetInt("takenTime",0);
        #endregion

        #region Money Calculation
        var oldMoney = PlayerPrefs.GetInt("Money");
        var salary = PlayerPrefs.GetInt("Salary");

        PlayerPrefs.SetInt("Money", oldMoney + salary);
        money = PlayerPrefs.GetInt("Money");

        Debug.Log("Salary :" + PlayerPrefs.GetInt("Salary") + " Money :" + PlayerPrefs.GetInt("Money"));

        moneyTXT.text = "Money : " + money.ToString("C");
        PlayerPrefs.SetInt("Salary",0);
        #endregion
    }

    private void Update()
    {
        money = PlayerPrefs.GetFloat("Money");
        var hours = PlayerPrefs.GetInt("actualTime");

        dayTXT.text = day.ToString() + ", " + timeOfDay.ToString();
        timeTXT.text = hours.ToString("00") + ":" + minutes.ToString("00");

        #region Time
        if (Input.GetKeyDown("f"))
        {
            baseHours += 1;
        }

        #region Daytime logistics
        if (hours >= 6)// || hours > 22)
        {
            timeOfDay = TimeOfDay.Morning;
        }
        if (hours >= 12)
        {
            timeOfDay = TimeOfDay.Noon;
        }
        if (hours >= 14)
        {
            timeOfDay = TimeOfDay.AfterNoon;
        }
        if (hours >= 17)
        {
            timeOfDay = TimeOfDay.Evening;
        }
        if (hours >= 22 || hours < 6)
        {
            timeOfDay = TimeOfDay.Night;
        }
        #endregion
        if (hours >= 24)
        {
            //hours = 0;
            var newHour = hours -= 24;
            PlayerPrefs.SetInt("actualTime", Mathf.Abs(newHour));
            DayChange();
        }
        #endregion
        if (Input.GetKeyDown("g"))
        {
            //PlayerPrefs.SetFloat("Money", 0);
            PlayerPrefs.DeleteAll();
        }
    }

    void DayChange()
    {
        switch (day)
        {
            case Day.Sunday:
                day = Day.Monday;
                break;
            case Day.Monday:
                day = Day.Tuesday;
                break;
            case Day.Tuesday:
                day = Day.Wednesday;
                break;
            case Day.Wednesday:
                day = Day.Friday;
                break;
            case Day.Friday:
                day = Day.Saturday;
                break;
            case Day.Saturday:
                day = Day.Sunday;
                break;
        }
    }
}