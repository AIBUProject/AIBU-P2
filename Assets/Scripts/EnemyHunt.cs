using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHunt : MonoBehaviour
{
    private bool isHunting = false;
    private float huntTimer = 0;
    private float cooldownTimer = 0;
    private int huntTime;
    private bool makeNewTime = true;
    private bool onCooldown = false;
    private bool huntTimeDone = false;
    // Start is called before the first frame update
  
    // Update is called once per frame
    void FixedUpdate()
    {
        if (isHunting) {
            
            if (makeNewTime) {
                huntTime = Random.Range(10, 18);
                makeNewTime = false;
            }
            
           
            if (huntCountdown(huntTime))
            {
                huntTimeDone = true;
                makeNewTime = true;
                // Destroy should happen after player leaves the room
                

            }
        }
        if (onCooldown == true){
            if (huntCooldown(10))
            {
                onCooldown = false;
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
    private bool huntCooldown(float seconds)
    {
        //Debug.Log(seconds);
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= seconds)
        {

            cooldownTimer = 0;
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
    public bool getOnCooldown() {
        return onCooldown;
    }
    public void setOnCooldown(bool b) {
        onCooldown = b;
    }

    public bool getHuntTimeDone() {
        return huntTimeDone;
    }

    public void setHuntTimeDone(bool b) {
        huntTimeDone = b;
    }



}
