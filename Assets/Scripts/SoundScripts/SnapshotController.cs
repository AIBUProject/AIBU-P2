using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SnapshotController : MonoBehaviour
{
    // AudioMixer settings for the different states of the game. Default state, and when getting chased
    [SerializeField] private AudioMixerSnapshot defaultMixerState;
    [SerializeField] private AudioMixerSnapshot gettingChasedState;
    [SerializeField] private AudioMixerSnapshot muteAudioState;

    [SerializeField] private bool muted = false;
    public string currentState;
    // Start is called before the first frame update
    void Start()
    {
        defaultMixerState.TransitionTo(0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MuteAudio()
    {
        Debug.Log("Current state is: " + currentState);
        if (!muted)
        {
            muteAudioState.TransitionTo(0.1f);       
        }
        else
        {
            ChangeMixerState("defaultState");
        }
        muted = !muted;
    }
    public void ChangeMixerState(string state)
    {
        switch (state)
        {
            case "defaultState":
                defaultMixerState.TransitionTo(0.2f);
                break;
            case "chasedState":
                gettingChasedState.TransitionTo(0.1f);
                break;
        }

    }

}
