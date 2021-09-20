using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerStateTracker : MonoBehaviour
{
    //Snapshotcontroller for changing mixer state
    public SnapshotController snapshotController;
    //AudioControllers
    [SerializeField] private AudioSource[] audioSource;
    //The Clips used. Currently, clip 0 is footsteps, clip 1 is death sound, clip 2 is
    [SerializeField] private AudioClip[] audioClip;

    public GameObject player;

    //Audio Variables, mostly used for Foley to change quickly when testing
    [SerializeField] private float lowestVol = 0.33f;
    [SerializeField] private float highestVol = 0.48f;
    [SerializeField] private float lowestPitch = 1.05f;
    [SerializeField] private float highestPitch = 1.22f;

    [SerializeField] private float minDelay = 4f;
    [SerializeField] private float maxDelay = 12f;
    //Gamestate information variables
    private Vector3 earlierPosition;
    public bool isMoving;
    public bool playerIsAlive;
    private float delay;
    private string tempState;
    private string currentState = "defaultState";

    private void Awake()
    {
        
    }
    void Start()
    {
        audioSource = GetComponents<AudioSource>();
        PlaySound("defaultMusic");

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            snapshotController.MuteAudio();
        }
        MovementCheck();
        UpdateState();
        PlayOneShots();
        TrollCheck();

    }

    //Does checks to determine what sounds to be playing every frame. From footsteps, to death sound, etc
    private void UpdateState()
    {
        if (isMoving)
        {
            PlaySound("moving");
        }

    }
    private void PlayOneShots()
    {
        if (!audioSource[2].isPlaying)
        {
            delay = Random.Range(minDelay, maxDelay);
            PlaySound("hoot");
        }        
    }

    private void TrollCheck()
    {
        if (GameObject.Find("Troll"))
        {
            tempState = "chasedState";
            
        }
        else
        {
            tempState = "defaultState";
            
        }
        if (tempState != currentState)
        {
            switch (tempState)
            {
                case "defaultState":
                    snapshotController.ChangeMixerState("defaultState");
                    PlaySound("trollAngry");
                    break;
                case "chasedState":
                    PlaySound("twig");
                    snapshotController.ChangeMixerState("chasedState");
                    break;
            }
            currentState = tempState;
        }
    }

    //The received snapshot is played in the selected audioSource. Source 0 is for footsteps and other foley primarily, source 1 is for music, 
    private void PlaySound(string clip)
    {
        switch (clip)
        {
            //AUDIO SOURCE 0 (footsteps)
            case "moving":
                if (!audioSource[0].isPlaying)
                {
                    audioSource[0].reverbZoneMix = Random.Range(1f, 1.2f);
                    audioSource[0].volume = Random.Range(lowestVol, highestVol);
                    audioSource[0].pitch = Random.Range(lowestPitch, highestPitch);
                    audioSource[0].PlayOneShot(audioClip[0]);
                }
                break;
                //Death sound
            case "playerDied":
                audioSource[0].reverbZoneMix = Random.Range(1.2f, 1.4f);
                audioSource[0].volume = Random.Range(1.4f, 1.5f);
                audioSource[0].pitch = Random.Range(1.2f, 1.4f);
                audioSource[0].PlayOneShot(audioClip[1]);

                break;
                //Branch that breaks when troll spawns
            case "twig":
                    audioSource[0].reverbZoneMix = Random.Range(1.2f, 1.4f);
                    audioSource[0].volume = Random.Range(1.4f, 1.5f);
                    audioSource[0].pitch = Random.Range(1.3f, 1.4f);
                    audioSource[0].PlayOneShot(audioClip[4]);
                break;
            case "trollAngry":
                audioSource[1].reverbZoneMix = Random.Range(0.82f, 0.9f);
                audioSource[1].volume = Random.Range(0.6f, 0.7f);
                audioSource[1].pitch = Random.Range(0.8f, 0.9f);
                audioSource[1].PlayOneShot(audioClip[5]);
                break;
            //AUDIO SOURCE 2 (ambience one shots)
            case "hoot":
                audioSource[2].volume = Random.Range(0.6f, 0.7f);
                audioSource[2].clip = audioClip[3];
                audioSource[2].PlayDelayed(delay);
                break;

            default:
                Debug.Log("Clip: "+clip+" was received, but nothing played");
                break;
        }
    }

    //Checks if the player is moving 
    private void MovementCheck()
    {
        if (player.transform.position != earlierPosition)
        {
            isMoving = true;
            earlierPosition = player.transform.position;
        }
        else
        {
            isMoving = false;
        }
    }

}
