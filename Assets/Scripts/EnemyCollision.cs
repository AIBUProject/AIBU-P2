using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyCollision : MonoBehaviour
{
    [SerializeField] private ParticleSystem deathParticle;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") {
            Instantiate(deathParticle, other.transform.position, Quaternion.identity);
            GameObject.Find("Canvas").GetComponentInChildren<FadeBlack>().setLoseScreen(true);
            //GameObject.Find("Canvas").GetComponentInChildren<FadeBlack>().setDoEnd(true);
        }
        
    }

}