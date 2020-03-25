using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float Life = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag.Equals("Enemy"))
        {
            Life -= 5;
            HealthBar.Health = Life;
            //animar puma recibiendo daño
        }

    }   

    public void getDamage(int dmg)
    {
        Life -= dmg;
        HealthBar.Health = Life;
    }
/*
    public void OnCollisionEnter2D(Collider2D col)
    {
        if(col.tag.Equals("Enemy"))
        {
            Life -= 20;
            HealthBar.Health = Life;
            //animar puma recibiendo daño
        }

    }   
*/
}
