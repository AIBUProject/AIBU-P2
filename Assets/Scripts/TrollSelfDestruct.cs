using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrollSelfDestruct : MonoBehaviour
{
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("TrollHuntTimer").GetComponent<EnemyHunt>().getIsHunting() == false){
            Destroy(this.gameObject);
        }
    }
}
