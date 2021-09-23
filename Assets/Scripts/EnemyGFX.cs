using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour
{
    public AIPath aIPath; //how to find aiPath?
    [SerializeField] private Animator anim;

    void Update()
    {
        if(aIPath.desiredVelocity.x >= 0.01f)//Moving right
        {
            anim.SetTrigger("goSide");
            transform.localScale = new Vector2(1, 1);
        }
        else if(aIPath.desiredVelocity.x <= -0.01f)//Moving left
        {
            anim.SetTrigger("goSide");
            transform.localScale = new Vector2(-1, 1);
        }
        else if(aIPath.desiredVelocity.y >= 0.01f)//Moving up
        {
            anim.SetTrigger("goSide");
            transform.localScale = new Vector2(1, 1);
        }
        else if(aIPath.desiredVelocity.y <= 0.01f)//Moving down
        {
            anim.SetTrigger("goDown");
            transform.localScale = new Vector2(1, 1);
        }
    }
}
