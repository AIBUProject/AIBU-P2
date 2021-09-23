using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private ParticleSystem pickupParticle;
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
            Instantiate(pickupParticle, other.transform.position, Quaternion.identity);
            pickupParticle.Play();
            other.gameObject.GetComponent<PlayerController>().setCollectEnabled(false);
        }

    }
}
