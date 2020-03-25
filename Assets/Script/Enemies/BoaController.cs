using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoaController : MonoBehaviour
{
    //STATS
    int ATK = 10;

    public int WAIT = 100;
    int THROW_COUNTER = 0;
    
    Transform myTrans;
    // Start is called before the first frame update
    void Start()
    {
        myTrans = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        ThrowPoison();
    }
    void ThrowPoison()
    {
        THROW_COUNTER++;
        if(THROW_COUNTER > WAIT)
        {
            
            THROW_COUNTER = 0;
            Vector3 position = Vector3.zero;
            position.Set(myTrans.position.x - 0.5f, myTrans.position.y, 1);
            GameObject obj = Instantiate(Resources.Load("Poison") as GameObject, position, Quaternion.identity);
            obj.GetComponent<PoisonController>().setDirection('L');
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
