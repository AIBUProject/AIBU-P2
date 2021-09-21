using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    new public Transform transform;
    private int collectedObjects = 0;
    private int health = 3;

    private bool isAlive = true;

    private bool goUp = false;
    private bool goDown = false;
    private bool goLeft = false;
    private bool goRight = false;

    private bool collectEnabled = false;
    private GameObject collectedGameObject;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 300;
        anim = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movementHandler();

    }
    void Update() {
        if(isAlive){
            collect();
            movementInput();
        }
    }
   
    private void movementInput() {
        if (Input.GetKey("up"))
        {
            goUp = true;
        }
        else {
            goUp = false;
        }
        if (Input.GetKey("left"))
        {
            goLeft = true;
        }
        else
        {
            goLeft = false;
        }
        if (Input.GetKey("right"))
        {
            goRight = true;
        }
        else
        {
            goRight = false;
        }
        if (Input.GetKey("down"))
        {
            goDown = true;
        }
        else
        {
            goDown = false;
        }
    }
    private void movementHandler() {

        if (goUp)
        {
            if (goLeft || goRight)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + 0.07f);
            }
            else {
                transform.position = new Vector2(transform.position.x, transform.position.y + 0.1f);
            }
            anim.SetTrigger("goUp");
            transform.localScale = new Vector2(1, 1);
        }
        if (goLeft)
        {
            if (goUp || goDown)
            {
                transform.position = new Vector2(transform.position.x - 0.07f, transform.position.y);
            }
            else
            {
                transform.position = new Vector2(transform.position.x - 0.1f, transform.position.y);
            }
            anim.SetTrigger("goSide");
            transform.localScale = new Vector2(-1, 1);
        }

        if (goRight)
        {
            if (goUp || goDown)
            {
                transform.position = new Vector2(transform.position.x + 0.07f, transform.position.y);
            }
            else
            {
                transform.position = new Vector2(transform.position.x + 0.1f, transform.position.y);
            }
            anim.SetTrigger("goSide");
            transform.localScale = new Vector2(1, 1);
        }
        if (goDown)
        {
            if (goLeft || goRight)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - 0.07f);
            }
            else
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - 0.1f);
            }
            anim.SetTrigger("goDown");
            transform.localScale = new Vector2(1, 1);
        }
        if (!goRight && !goLeft && !goUp && !goDown)
        {
            anim.speed = 0;

        }
        else
        {
            anim.speed = 1;
        }
    }

    private void collect() {
        if (Input.GetKeyDown(KeyCode.E)&& collectEnabled){
        collectedObjects++;
        Debug.Log(collectedObjects);
        Destroy(collectedGameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Player took " + damage + " damage, health is now " + health);
        if(health <= 0)
        {
            //die
            Debug.Log("Player lost");
        }
        else
        {
            Debug.Log("Respawn player");
            //teleport to start
        }
    }

    public void setCollectEnabled(bool b) {

        collectEnabled = b;
    }

    public int getCollectedGameObject() {

        return collectedObjects;
    }

    public void setCollectedGameObject(GameObject g) {

    collectedGameObject =  g;
}
}
