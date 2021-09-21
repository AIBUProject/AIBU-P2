using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public GameObject spawnLocationObject;
    private Transform spawnLocation;

    private bool allowSpawn = false;
    // Start is called before the first frame update
    void Start()
    {
        spawnLocation = spawnLocationObject.transform;
    }

    // Update is called once per frame
  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(GameObject.Find("TrollHuntTimer").GetComponent<EnemyHunt>().getIsHunting());
        if (GameObject.Find("TrollHuntTimer").GetComponent<EnemyHunt>().getIsHunting() == false && allowSpawn == true)
        {


          

                int random = Random.Range(0, 10);
                Debug.Log(random);
            if (random <= 7)
            {

            }
            else if (random >= 8) { 
                 Debug.Log(random);
            GameObject.Find("TrollHuntTimer").GetComponent<EnemyHunt>().setIsHunting(true);
            Instantiate(enemy, spawnLocation.position, spawnLocation.rotation);
               

                  }
            allowSpawn = false;

        }
        
        }

    public void setAllowSpawn(bool b) {

        allowSpawn = b;
        Debug.Log(allowSpawn);
    }
}
