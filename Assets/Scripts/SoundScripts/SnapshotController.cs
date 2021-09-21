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
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private bool muted = false;
    [SerializeField] private AudioSource chasedMusic;
    [SerializeField] private AudioSource defaultMusic;
    public string currentState = "defaultState";
    private float startingMusicVol;
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
                StartCoroutine(FadeAudioSource.StartFade(chasedMusic, 4f, 0));
                StartCoroutine(FadeAudioSource.StartFade(defaultMusic, 2f, 1));
                defaultMixerState.TransitionTo(0.7f);
                defaultMusic.Play();
                break;
            case "chasedState":
                StartCoroutine(FadeAudioSource.StartFade(chasedMusic, 0.4f, 1));
                StartCoroutine(FadeAudioSource.StartFade(defaultMusic, 0.4f, 0));
                chasedMusic.Play();
                gettingChasedState.TransitionTo(0.1f);
                    break;
            }
            currentState = state;

    }
    public void DistanceAdjustment(float dist)
    {
            audioMixer.SetFloat("Music", ((Mathf.Log10(1 / dist) * 20) + 6));

    }   
    public void SetDefaultMusicVolume()
    {
        audioMixer.SetFloat("Music", -11f);
    }

}

