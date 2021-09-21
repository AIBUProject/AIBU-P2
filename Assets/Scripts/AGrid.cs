using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AGrid : MonoBehaviour
{
    private float waitTimer = 0;
    public GameObject pathfinder;
    bool doOnce = false;

    void Update()
    {
      if(GameObject.Find("RoomController").GetComponentInChildren<RoomController>().spawnedEndRoom == true && doOnce == false){
            if (gridWait()) {
                Instantiate(pathfinder, new Vector2(0, 0), Quaternion.identity);

                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().setAllowInput(true);
                doOnce = true;
            }
          
        }
    }

    private bool gridWait()
    {
        //Debug.Log(seconds);
        waitTimer += Time.deltaTime;

        if (waitTimer >= 1f)
        {

            waitTimer = 0;
            return true;

        }

        return false;
    }

}
