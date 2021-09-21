using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WinCondition : MonoBehaviour
{
    Scene scene;
    GameObject[] collectibles;
    private GameObject player;
    public Animator animator;
    private bool playedAnim = false;
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        collectibles = GameObject.FindGameObjectsWithTag("Collectible");
        player = GameObject.FindGameObjectWithTag("Player");
        amountToWin = collectibles.Length;
    }

    void Update()
    {
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
            SceneManager.LoadScene(scene.name);
        }

    }
}
 