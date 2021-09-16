using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(GameObject.Find("TrollHuntTimer").GetComponent<EnemyHunt>().getIsHunting());
        if (GameObject.Find("TrollHuntTimer").GetComponent<EnemyHunt>().getIsHunting() == false)
        {


            // removed for testing purposes

            //    int random = Random.Range(0, 10);
            //    Debug.Log(random);
            //   if (random <= 8)
            //  {

            //   }
            //  else {
            //     Debug.Log(random);
            GameObject.Find("TrollHuntTimer").GetComponent<EnemyHunt>().setIsHunting(true);
            Instantiate(enemy, new Vector2(0, 0), Quaternion.identity);
               

                //  }


        }
        
        }

}
