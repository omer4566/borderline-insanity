using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public enum Character {CHAD }

public class RelationshipsManager : MonoBehaviour
{
    [Header("DO NOT TOUCH")]
    public int rank, relation_pt, cap;

    private bool use = true;

    [Header("CHANGE")]
    public static Character character;

    [Header("Objects")]
    public TextMeshProUGUI textMesh;

    public static bool isProgressionCutscene;

    private void Start()
    {
        relation_pt = PlayerPrefs.GetInt("Chad_pt");
        rank = PlayerPrefs.GetInt("Chad_rank");
        Debug.Log("rank " + rank + " relation : " + relation_pt);
    }

    public void AddPoints()
    {
        if (use)
        {
            CapLogistic();
            switch (character)
            {
                case Character.CHAD:
                    PlayerPrefs.SetInt("Chad_pt", relation_pt);
                    if (relation_pt >= cap)
                    {
                        NewRank();
                        PlayerPrefs.SetInt("Chad_cap", cap);
                    }

                    StartCoroutine(ExitScene(5, 0));

                    break;
            }
        } 
    }
    // Checks if the player gets new relationship rank
    public void NewRank()
    {
        if (use)
        {
            rank++;
            PlayerPrefs.SetInt("Chad_pt", 0);
            PlayerPrefs.SetInt("Chad_rank", rank);
            textMesh.text = "New Rank! " + rank;
            use = false;
        }       
    }

    public void CapLogistic()
    {
        switch (rank)
        {
            case 0:
                cap = 6;
                break;
            case 1:
                cap = 10;
                break;
            case 2:
                cap = 13;
                break;
            case 3:
                cap = 16;
                break;
        }
    }

    public IEnumerator ExitScene(float seconds, int scene)
    {
        yield return new WaitForSeconds(seconds);

        SceneManager.LoadScene(scene);
    }
}
