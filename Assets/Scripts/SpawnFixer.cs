using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFixer : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision");
        if(other.tag == "Player")
        {
            Debug.Log("player");
            other.transform.position = new Vector3(-6.5f,0,0);
        }
    }
}
