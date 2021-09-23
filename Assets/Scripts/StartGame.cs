using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{

    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject settings;
    [SerializeField] private ParticleSystem door;
    // Start is called before the first frame update
    void Start()
    {
        credits.SetActive(false);
        settings.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowCredits()
    {
        if (!credits.activeSelf)
        {
            credits.SetActive(true);
            door.Stop();
        }
        else
        {
            credits.SetActive(false);
            door.Play();
        }
    }

    public void ShowSettings()
    {
        if (!settings.activeSelf)
        {
            settings.SetActive(true);
            door.Stop();
        }
        else
        {
            settings.SetActive(false);
            door.Play();
        }
    }

    public void NewGame()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("QuitGame");
        Application.Quit();
    }
}
