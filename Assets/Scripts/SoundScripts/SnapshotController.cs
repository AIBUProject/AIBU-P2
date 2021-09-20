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
    public string currentState = "defaultState";
    [SerializeField] private AudioSource chasedMusic;
    [SerializeField] private AudioSource defaultMusic;

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
                chasedMusic.Stop();
                defaultMixerState.TransitionTo(0.7f);
                defaultMusic.PlayDelayed(0.6f);
                break;
            case "chasedState":               
                chasedMusic.PlayDelayed(0.1f);
                defaultMusic.Stop();
                gettingChasedState.TransitionTo(0.1f);
                    break;
            }
            currentState = state;

    }

}
