using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaverController : MonoBehaviour
{
    EnemyController controller;
    Rigidbody2D myBody;
    float JUMP = 35000;
    int COUNTER_JUMP = 0;
    int JUMP_TIME = 100;
    float SPEEDX = 4f;
    int ATK = 10;
    // Start is called before the first frame update
    void Start()
    {
        myBody = this.transform.GetComponent<Rigidbody2D>();
        controller = this.transform.GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!controller.CanUpdate()) return;
        controller.Reset();
        Move();
        Jump();
    }
    void Move()
    {
        Vector2 myVel = myBody.velocity;
        myVel.x = -SPEEDX;
        myBody.velocity = myVel;
    }
    void Jump()
    {
        COUNTER_JUMP++;
        if (COUNTER_JUMP >= JUMP_TIME)
        {
            Debug.Log("JUMP BEAVER");
            myBody.AddForce(Vector2.up * JUMP);
            COUNTER_JUMP = 0;
        }
    }
    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Player")
        {
            PlayerMovement pm = col.gameObject.GetComponent<PlayerMovement>();
            pm.GetDamage(-ATK);
        }
    }
}
