using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyCollision : MonoBehaviour
{
 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") {
            GameObject.Find("Canvas").GetComponentInChildren<FadeBlack>().setDoEnd(true);
       
        }
        
    }

}