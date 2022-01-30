using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    [Header("Music Settings")]
    public AudioClip[] ost;
    [Range(0.0f,1)]
    public float volume;
    public int ost_index;
    public bool playMusic;

    [Header("Music Trigger")]
    public string[] ost_keyWords;
    public int[] keyTrack;
    private int ost_trigger_index = 0;

    [Header("Plot Progression")]
    public string[] plot_keyWords;
    private int plot_trigger_index = 0;

    [Header("Other stuff")]
    public DialogueSystem system;
    public Animator animator;
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        #region Music control
        audioSource.volume = volume;

        if (playMusic && audioSource.isPlaying == false)
        {
            PlayTrack();
        }
        if (playMusic == false)
        {
            StopTrack();
        }
        #endregion

        #region Music Triggers
        if (ost_keyWords.Length != 0 || ost_keyWords.Length <= ost_trigger_index)
        switch (ost_trigger_index)
        {
            case 0:
                if (system.currentLine == ost_keyWords[ost_trigger_index])
                {
                    Debug.Log("Trigger activated");
                    StopTrack();
                    ost_index = keyTrack[ost_trigger_index];
                    PlayTrack();
                    ost_trigger_index++;
                }
                break;
            case 1:
                if (system.currentLine == ost_keyWords[ost_trigger_index])
                {
                    Debug.Log("Trigger activated");
                    StopTrack();
                    ost_index = keyTrack[ost_trigger_index];
                    PlayTrack();
                    ost_trigger_index++;
                }
                break;
            case 2:
                if (system.currentLine == ost_keyWords[ost_trigger_index])
                {
                    Debug.Log("Trigger activated");
                    StopTrack();
                    ost_index = keyTrack[ost_trigger_index];
                    PlayTrack();
                    ost_trigger_index++;
                }
                break;
            case 3:
                if (system.currentLine == ost_keyWords[ost_trigger_index])
                {
                    Debug.Log("Trigger activated");
                    StopTrack();
                    ost_index = keyTrack[ost_trigger_index];
                    PlayTrack();
                    ost_trigger_index++;
                }
                break;

            default:
                Debug.Log("Triggers ended");
                break;
        
        }       
        #endregion

        #region Plot 
        if (plot_keyWords.Length != 0 || plot_trigger_index > plot_keyWords.Length)
        switch (plot_trigger_index)
        {
            case 0:
                if (system.currentLine == plot_keyWords[plot_trigger_index])
                {
                    plot_trigger_index++;
                    animator.SetBool("Part2", true);
                }
                break;
            case 1:
                if (system.currentLine == plot_keyWords[plot_trigger_index])
                {
                    plot_trigger_index++;
                    animator.SetBool("Part3", true);
                }
                break;
            case 2:
                if (system.currentLine == plot_keyWords[plot_trigger_index])
                {
                    plot_trigger_index++;
                    animator.SetBool("Part4", true);
                }
                break;
            case 3:
                if (system.currentLine == plot_keyWords[plot_trigger_index])
                {
                    plot_trigger_index++;
                }
                break;
            case 4:
                if (system.currentLine == plot_keyWords[plot_trigger_index])
                {
                    plot_trigger_index++;
                }
                break;
        }
        #endregion
    }

    void PlayTrack()
    {
        playMusic = true;
        Debug.Log("Starts new track : " + ost[ost_index].name);
        audioSource.PlayOneShot(ost[ost_index], volume);       
    }

    void StopTrack()
    {
        playMusic = false;
        Debug.Log("Stopped clip : " + ost[ost_index].name);
        audioSource.Stop();
    }
}
