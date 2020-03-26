using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{
    float SPEEDX = 2f;
    float SPEEDY = 0.1f;
    int ATK = 10;
    Rigidbody2D myBody;
    bool elevate = false;
    int FLY_TIME = 30;
    int COUNTER_FLY = 0;
    EnemyController controller;
    // Start is called before the first frame update
    void Start()
    {
        myBody = this.GetComponent<Rigidbody2D>();
        controller = this.transform.GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!controller.CanUpdate()) return;
        if (controller.Reset())
        {
            myBody.velocity = new Vector3(0f, 0f, 0f);
            return;
        }
        Move();
    }

    public void Move()
    {
        //Horizontal movement
        Vector2 myVel = myBody.velocity;
        myVel.x = -SPEEDX;
        
        //Vertical movement
        COUNTER_FLY++;
        if(COUNTER_FLY > FLY_TIME)
        {
            elevate = !elevate;
            COUNTER_FLY = 0;
        }
        if (elevate)
        {
            myVel.y += SPEEDY*1.2f;
        }
        else
        {
            myVel.y -= SPEEDY;
        }
        myBody.velocity = myVel;
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            PlayerMovement pm = col.gameObject.GetComponent<PlayerMovement>();
            pm.GetDamage(-this.ATK);
        }
    }
   
}
