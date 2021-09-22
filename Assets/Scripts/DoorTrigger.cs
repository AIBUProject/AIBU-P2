using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{


    GameObject[] spawners;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (GameObject.Find("TrollHuntTimer").GetComponent<EnemyHunt>().getHuntTimeDone() == true)
            {

                GameObject.Find("TrollHuntTimer").GetComponent<EnemyHunt>().setIsHunting(false);
                GameObject.Find("TrollHuntTimer").GetComponent<EnemyHunt>().setOnCooldown(true);
                GameObject.Find("TrollHuntTimer").GetComponent<EnemyHunt>().setHuntTimeDone(false);

            }

            spawners = GameObject.FindGameObjectsWithTag("Spawner");
            for (int i = 0; i < spawners.Length; i++)
            {
                Debug.Log("spawner number" + i);
                spawners[i].GetComponent<EnemySpawner>().setAllowSpawn(true);
            }

        }
    }

}
