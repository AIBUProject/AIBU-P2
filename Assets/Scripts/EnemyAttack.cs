using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Collider2D attackCollider;
    public int damage = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
        }
    }
}
