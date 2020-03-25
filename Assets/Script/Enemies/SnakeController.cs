using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    int ATK;

    
    float SPEED = 1.5f;

    
    Rigidbody2D myBody;
    EnemyController controller;
    // Start is called before the first frame update
    void Start(){
        myBody = this.GetComponent<Rigidbody2D>();
        this.ATK = 10;
        controller = this.transform.GetComponent<EnemyController>();
    }


    public void move()
    {
        Vector2 myVel = myBody.velocity;
        myVel.x = -SPEED;
        myBody.velocity = myVel;
        //Debug.Log("snake pos: " + transform.position);
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.name == "Player")
        {
            PlayerMovement pm = col.gameObject.GetComponent <PlayerMovement>();
            pm.GetDamage(-this.ATK);
        }
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
        move();
    }

}
