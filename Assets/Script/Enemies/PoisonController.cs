using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonController : MonoBehaviour
{

    public int ATK = 20;
    public int DISTANCE = 150;
    int DISTANCE_COUNTER = 0;
    char direction;
    float SPEED = 3f;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        DISTANCE_COUNTER++;
        if(DISTANCE_COUNTER > DISTANCE)
        {
            Debug.Log("Destroy Poison");
            Destroy(transform.gameObject);
        }
        Move();
    }

    public void Move()
    {
        if (direction == 'L')
        {
            transform.position -= transform.right * SPEED * Time.deltaTime;
        }
        else if (direction == 'R')
        {
            
            transform.position += transform.right * SPEED * Time.deltaTime;
        }
    }

    public void setDirection(char direction)
    {
        
        this.direction = direction;
        if(direction == 'R')
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            
            PlayerMovement pm = col.gameObject.GetComponent<PlayerMovement>();
            
            pm.GetDamage(-ATK);
            Destroy(transform.gameObject);
        }
    }
}
