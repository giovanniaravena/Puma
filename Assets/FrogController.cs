using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonoBehaviour
{
    Rigidbody2D myBody;
    float JUMP = 27500;
    int COUNTER_JUMP = 0;
    double JUMP_TIME = 100;
    int ATK = 10;
    EnemyController controller;
    // Start is called before the first frame update
    void Start()
    {
        System.Random rand = new System.Random();
        JUMP_TIME += 50 * rand.NextDouble();
        myBody = this.transform.GetComponent<Rigidbody2D>();
        controller = this.transform.GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!controller.CanUpdate()) return;
        controller.Reset();
        Jump();   
    }
    void Jump()
    {
        COUNTER_JUMP++;
        if(COUNTER_JUMP >= JUMP_TIME)
        {

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
