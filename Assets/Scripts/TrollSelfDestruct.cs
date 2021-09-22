using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrollSelfDestruct : MonoBehaviour
{
    bool doDestroy = false;
    float destroyTimer = 0f;
    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("TrollHuntTimer").GetComponent<EnemyHunt>().getIsHunting() == false){
            if (destroyWaitTime(0.3f)){
                Destroy(this.gameObject);
            }   
            
        }
    }

    public void setDoDestroy(bool b) {

        doDestroy = b;
    }

    private bool destroyWaitTime(float seconds)
    {

        destroyTimer += Time.deltaTime;

        if (destroyTimer >= seconds)
        {

            destroyTimer = 0;
            return true;

        }

        return false;
    }
}
