using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    
    private void Start()
    {
        GameObject.Find("Exit").GetComponent<WinCondition>().addAmountToWin();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") {
            other.gameObject.GetComponent<PlayerController>().setCollectEnabled(true);
            other.gameObject.GetComponent<PlayerController>().setCollectedGameObject(this.gameObject);
        }
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().setCollectEnabled(false);
        }

    }
}
