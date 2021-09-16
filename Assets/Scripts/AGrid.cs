using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AGrid : MonoBehaviour
{

    public GameObject pathfinder;
    bool doOnce = false;

    void Update()
    {
      if(GameObject.Find("RoomController").GetComponentInChildren<RoomController>().updatedRooms == true && doOnce == false){
            Instantiate(pathfinder, new Vector2(0, 0), Quaternion.identity);
            doOnce = true;

        }
    }
}
