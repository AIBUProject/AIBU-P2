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
    private GameObject[] collectibles;
    public GameObject player;
    public GameObject volumeControls;
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
    public bool isAlive;
    private bool portalNotOpen = true;
    private float delay;
    private float delay2;
    private bool volumeMenu = true;
    private bool winConditionFound = false;
    private string tempState;
    private string currentState = "defaultState";
    public int amountToWin=-1;
    public int collectedPickups=0;
    private int tempCollectedPickups;
    private void Awake()
    {

    }
    void Start()
    {
        audioSource = GetComponents<AudioSource>();
        PlaySound("defaultMusic");
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (!winConditionFound)
        {
            GameObject exitObj = GameObject.FindGameObjectWithTag("Exit");
            if (exitObj!=null)
            {
                amountToWin = exitObj.GetComponent<WinCondition>().GetAmountToWin();
                winConditionFound = true;
            }
        }
        collectedPickups = player.gameObject.GetComponent<PlayerController>().getCollectedGameObject();
        if (Input.GetKeyDown(KeyCode.M))
        {
            //snapshotController.MuteAudio();
            snapshotController.SetDefaultMusicVolume();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            volumeControls.SetActive(volumeMenu);
            volumeMenu = !volumeMenu;
        }
        MovementCheck();
        UpdateState();
        PlayOneShots();
        TrollCheck();
    }

    //Does checks to determine what sounds to be playing every frame. From footsteps, to death sound, etc
    private void UpdateState()
    {
        if ((GameObject.Find("Canvas").GetComponentInChildren<FadeBlack>().getDoEnd() == true && isAlive) ||  (GameObject.Find("Canvas").GetComponentInChildren<FadeBlack>().getLoseScreen() == true && isAlive)) { 
        PlaySound("playerDied");
         PlaySound("trollChomp");
        snapshotController.SetDefaultMusicVolume();
         snapshotController.ChangeMixerState("defaultState");
         PlaySound("trollAngry");
            StartCoroutine(FadeAudioSource.StartFade(audioSource[3], 3f, 0));

            isAlive = false;
        }
        if (isMoving)
        {
            PlaySound("moving");
        }
        if (collectedPickups == amountToWin)
        {
            if (portalNotOpen)
            {
                PlaySound("openPortal");
                portalNotOpen = false;
            }
            PlaySound("portalAmbience");
        }
        if (collectedPickups<=amountToWin && collectedPickups != -1)
        {
            if (tempCollectedPickups != collectedPickups)
            {
                Debug.Log("PickupSound should play");
                PlaySound("pickup");
                tempCollectedPickups = collectedPickups;
            }
        }

    }
    private void TrollProximityVolume()
    {
            float dist = Vector3.Distance(GameObject.Find("Troll").transform.position, player.transform.position);
            snapshotController.DistanceAdjustment(dist);
    }
    private void PlayOneShots()
    {
        if (!audioSource[2].isPlaying)
        {
            delay = Random.Range(minDelay, maxDelay);
            delay2 = Random.Range(minDelay*2, maxDelay*2);
            PlaySound("hoot");
            Debug.Log("delay: " + delay + " delay2:" + delay2);
        }
        if (!audioSource[5].isPlaying)
        {
            PlaySound("chuScared");
        }
    }

    private void TrollCheck()
    {
        if (GameObject.Find("Troll") && isAlive)
        {
            tempState = "chasedState";
            TrollProximityVolume();
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
                    snapshotController.SetDefaultMusicVolume();
                    snapshotController.ChangeMixerState("defaultState");
                    PlaySound("trollAngry");
                    StartCoroutine(FadeAudioSource.StartFade(audioSource[3], 3f, 0));
                    break;
                case "chasedState":
                    PlaySound("twig");
                    PlaySound("trollAwaken");
                    PlaySound("chasedAmbient");
                    snapshotController.ChangeMixerState("chasedState");
                    break;
            }
            currentState = tempState;
        }
    }

    //The received snapshot is played in the selected audioSource. Source 0 is for footsteps and other foley primarily,
    // source 1 is for troll sound, source 2 and 3 for occasional background/character sounds
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
                audioSource[2].reverbZoneMix = Random.Range(1f, 1.1f);
                audioSource[2].volume = Random.Range(0.55f, 0.6f);
                audioSource[2].pitch = Random.Range(0.9f, 1f);
                audioSource[2].PlayOneShot(audioClip[1]);
                break;
                //Branch that breaks when troll spawns
            case "twig":
                    audioSource[0].reverbZoneMix = Random.Range(1.2f, 1.4f);
                    audioSource[0].volume = Random.Range(1.4f, 1.5f);
                    audioSource[0].pitch = Random.Range(1.3f, 1.4f);
                    audioSource[0].PlayOneShot(audioClip[4]);
                break;
                //Troll Sounds
            case "trollAngry":
                audioSource[1].reverbZoneMix = Random.Range(0.6f, 0.7f);
                audioSource[1].volume = Random.Range(0.5f, 0.6f);
                audioSource[1].pitch = Random.Range(0.8f, 0.9f);
                audioSource[1].PlayOneShot(audioClip[5]);
                break;
            case "trollAwaken":
                audioSource[1].reverbZoneMix = Random.Range(0.6f, 0.7f);
                audioSource[1].volume = Random.Range(0.5f, 0.6f);
                audioSource[1].pitch = Random.Range(0.8f, 0.9f);
                audioSource[1].PlayOneShot(audioClip[10]);
                break;
            case "trollChomp":
                audioSource[1].reverbZoneMix = Random.Range(0.6f, 0.7f);
                audioSource[1].volume = Random.Range(0.5f, 0.6f);
                audioSource[1].pitch = Random.Range(0.8f, 0.9f);
                audioSource[1].PlayOneShot(audioClip[9]);
                break;
            // Player Sounds when troll is asleep
            case "chuScared":
                    audioSource[5].volume = Random.Range(1f, 1.2f);
                    audioSource[5].clip = audioClip[7];
                    audioSource[5].PlayDelayed(delay2);
                break;
            //AUDIO SOURCE 2 & 3 (ambience one shots)
            case "chasedAmbient":
                audioSource[3].volume = 0.64f;
                audioSource[3].pitch = 0.8f;
                audioSource[3].PlayOneShot(audioClip[8]);
                break;
            case "hoot":
                audioSource[2].volume = Random.Range(0.6f, 0.7f);
                audioSource[2].clip = audioClip[3];
                audioSource[2].PlayDelayed(delay);
                break;
            case "openPortal":
                if (!audioSource[3].isPlaying) {
                    audioSource[3].reverbZoneMix = Random.Range(0.6f, 0.7f);
                    audioSource[3].volume = Random.Range(0.5f, 0.6f);
                    audioSource[3].pitch = Random.Range(0.8f, 0.9f);
                    audioSource[3].PlayOneShot(audioClip[11]);
                }
                break;
                //Pickup sound
            case "pickup":
                audioSource[4].reverbZoneMix = Random.Range(1.2f, 1.3f);
                audioSource[4].volume = Random.Range(0.05f, 0.08f);
                audioSource[4].pitch = Random.Range(0.9f,1.2f);
                audioSource[4].PlayOneShot(audioClip[13]);
                break;
            case "portalAmbience":
                //Need a distance method here, but no time to fix it rn. But it works at least.
               /*if (!audioSource[6].isPlaying) { 
                    audioSource[6].reverbZoneMix = Random.Range(0.4f, 0.45f);
                    audioSource[6].volume = Random.Range(0.1f, 0.15f);
                    audioSource[6].pitch = Random.Range(1f, 1.1f);
                    audioSource[6].PlayOneShot(audioClip[12]);
                }*/
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

