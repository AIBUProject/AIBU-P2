using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHunt : MonoBehaviour
{
    private bool isHunting = false;
    private float huntTimer = 0;
    private int huntTime;
    private bool makeNewTime = true;
    // Start is called before the first frame update
  
    // Update is called once per frame
    void FixedUpdate()
    {
        if (isHunting) {
            
            if (makeNewTime) {
                huntTime = Random.Range(10, 21);
                makeNewTime = false;
            }
            
           
            if (huntCountdown(huntTime))
            {
                isHunting = false;
                makeNewTime = true;
                // Destroy should happen after player leaves the room
             
            }
        }
      
    }
    private bool huntCountdown(float seconds)
    {
        //Debug.Log(seconds);
        huntTimer += Time.deltaTime;

        if (huntTimer >= seconds)
        {

            huntTimer = 0;
            return true;

        }

        return false;
    }

    public void setIsHunting(bool b) {

        isHunting = b;
    }
    public bool getIsHunting() {

        return isHunting;
    }
}
