using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeBlack : MonoBehaviour
{
    Scene scene;
    public Image blackFade;
    public Image winScreen;
    public Image loseScreen;
    private bool doFadeIn = true;
    private bool doFadeOut = true;
    private float reloadTimer = 0;
    private bool doEnd = false;
    private bool winCheck;
    private bool loseCheck;

    private Animator anim;

    void Start()
    {
        blackFade.canvasRenderer.SetAlpha(1.0f);
        scene = SceneManager.GetActiveScene();
        winScreen.gameObject.SetActive(false);
        loseScreen.gameObject.SetActive(false);

    }

    private void Update()
    {
        if (GameObject.Find("RoomController").GetComponentInChildren<RoomController>().populatedRooms == true && doFadeOut == true) {
            fadeOut();
            doFadeOut = false;
        }
        if (GameObject.Find("RoomController").GetComponentInChildren<RoomController>().populatedRooms == true && doEnd == true)
        {
            if (doFadeIn == true) {
                fadeIn();
                doFadeIn = false;
            }

            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().setAllowInput(false);
            if (reloadCountdown(2.0f) == true) {
                reloadScene();
            }

        }

        if (GameObject.Find("RoomController").GetComponentInChildren<RoomController>().populatedRooms == true && winCheck == true)
        {
            FadeWin();

            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().setAllowInput(false);
            if (reloadCountdown(2.0f) == true)
            {
                reloadScene();
            }

        }

        if (GameObject.Find("RoomController").GetComponentInChildren<RoomController>().populatedRooms == true && loseCheck == true)
        {
            FadeLose();

            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().setAllowInput(false);
            if (reloadCountdown(2.0f) == true)
            {
                reloadScene();
            }

        }
    }

    void fadeOut()
    {
        blackFade.CrossFadeAlpha(0, 1.5f, false);
    }
    void fadeIn()
    {
        blackFade.canvasRenderer.SetAlpha(0.0f);
        blackFade.CrossFadeAlpha(1, 1f, false);
    }

    void FadeWin()
    {
        winScreen.gameObject.SetActive(true);
        fadeIn();
    }

    void FadeLose()
    {
        loseScreen.gameObject.SetActive(true);
        fadeIn();
    }

    void reloadScene() {
        SceneManager.LoadScene(scene.name);
    }

    public void setDoEnd(bool b) {
        doEnd = b;
    }
    public bool getDoEnd() {

        return doEnd;
    }

    public void setWinScreen(bool b)
    {
        winCheck = b;
    }

    public void setLoseScreen(bool b)
    {
        loseCheck = b;
    }
    public bool getLoseScreen()
    {
        return loseCheck;
    }
    private bool reloadCountdown(float seconds)
    {
        //Debug.Log(seconds);
        reloadTimer += Time.deltaTime;

        if (reloadTimer >= seconds)
        {

            reloadTimer = 0;
            return true;

        }

        return false;
    }

}
