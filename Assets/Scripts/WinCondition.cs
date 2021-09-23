using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class WinCondition : MonoBehaviour
{
    Scene scene;
    GameObject[] collectibles;
    private GameObject player;
    public Animator animator;
    private bool playedAnim = false;
    private GameObject collectibleCounter;
    private Text amountCollected;
    void Awake()
    {
        scene = SceneManager.GetActiveScene();
        collectibles = GameObject.FindGameObjectsWithTag("Collectible");
        player = GameObject.FindGameObjectWithTag("Player");
        amountToWin = collectibles.Length;
        collectibleCounter = GameObject.FindGameObjectWithTag("CollectibleCounter");
        amountCollected = collectibleCounter.GetComponent<Text>();
    }

    void Update()
    {
        //string total = amountToWin.ToString();

        amountCollected.text = player.gameObject.GetComponent<PlayerController>().getCollectedGameObject().ToString() + "/" + amountToWin.ToString();
        if(!playedAnim)
        {
            if(player.gameObject.GetComponent<PlayerController>().getCollectedGameObject() == amountToWin)
            {
                animator.SetTrigger("Open");
                playedAnim = true;
            }
        }
    }

    private int amountToWin;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(collectibles.Length + "to win");
        if (other.tag == "Player" && other.gameObject.GetComponent<PlayerController>().getCollectedGameObject() == amountToWin)
        {
            Debug.Log("You Win");
            GameObject.Find("Canvas").GetComponentInChildren<FadeBlack>().setWinScreen(true);
            //SceneManager.LoadScene(scene.name);
        }

    }
    public int GetAmountToWin()
    {
        return amountToWin;
    }
}
 