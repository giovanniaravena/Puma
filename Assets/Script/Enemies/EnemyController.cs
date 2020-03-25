using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public int HP_MAX;
    int HP_CURR;
    Color myColor;
    // Start is called before the first frame update

    public GameObject player;
    float xInitial, yInitial;
    int DISTANCE = 12;
    void Start()
    {
        HP_CURR = HP_MAX;
        myColor = this.gameObject.GetComponent<SpriteRenderer>().color;
        xInitial = transform.position.x;
        yInitial = transform.position.y;
        Debug.Log("initial position: " + xInitial + "," + yInitial);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void getDamage(int amount)
    {
        //Debug.Log("Enemy: " + this.name + " gets damage: " + amount + ".");
        StartCoroutine(Blink());
        HP_CURR -= amount;
        if(HP_CURR <= 0)
        {
            Destroy(this.transform.gameObject);
        }
    }

    IEnumerator Blink()
    {
        for (int n = 0; n < 6; n++)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.black;

            yield return new WaitForSeconds(0.01f);
            this.gameObject.GetComponent<SpriteRenderer>().color = myColor;
            yield return new WaitForSeconds(0.01f);
        }
    }

    public bool CanUpdate()
    {
        if (player == null) return false;
        float d = xInitial - player.transform.position.x;
        //Debug.Log("Distance: "+ d+".");
        if(d <= DISTANCE)
        {
            //Debug.Log("Enable distance with: " + d + ".");
            return true;
        }
        else
        {
            //Debug.Log("cannot move");
            return false;
        }
    }
    public bool Reset()
    {
        if (-xInitial + player.transform.position.x > DISTANCE)
        {
           // Debug.Log("Reset with distance: "+(-xInitial + player.transform.position.x) +".");
            transform.position = new Vector2(xInitial, yInitial);
            this.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, 0f, 0f);
            //Debug.Log(transform.position);
            return true;
        }
        return false;
    }
}
