using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour
{
    public AIPath aIPath;

    void Update()
    {
        if(aIPath.desiredVelocity.x >= 0.01f)//Moving right
        {
            //do animator stuff
        }
        else if(aIPath.desiredVelocity.x <= -0.01f)//Moving left
        {
            //do animator stuff
        }
        else if(aIPath.desiredVelocity.y >= 0.01f)//Moving up
        {
            //do animator stuff
        }
        else if(aIPath.desiredVelocity.y <= 0.01f)//Moving down
        {
            //do animator stuff
        }
    }
}
