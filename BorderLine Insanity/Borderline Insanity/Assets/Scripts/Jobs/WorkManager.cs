using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WorkType { MCRONALD, OFFICE, CEO, COLLAGE}

public class WorkManager : MonoBehaviour
{
    public WorkType type;
    [Range(0, 100000)]
    public int salary;
    [Range(1,14)]
    public int takenTime;

    void Start()
    {
        PlayerPrefs.SetInt("takenTime", takenTime);
        PlayerPrefs.SetInt("Salary", salary);
    }
   
}
