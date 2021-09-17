using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WinCondition : MonoBehaviour
{
    Scene scene;
    GameObject[] collectibles;
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        collectibles = GameObject.FindGameObjectsWithTag("Collectible");
        amountToWin = collectibles.Length;
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
 