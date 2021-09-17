using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    GameObject[] collectibles;
    void Start()
    {
        collectibles = GameObject.FindGameObjectsWithTag("Collectible");
        amountToWin = collectibles.Length;
    }
    private int amountToWin;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && other.gameObject.GetComponent<PlayerController>().getCollectedGameObject() == amountToWin)
        {
            Debug.Log("You Win");

        }

    }
}
 