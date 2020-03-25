using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax1 : MonoBehaviour
{
    //private float total;
    //private string name;

    private float length;
    
    private float startpos;
    public GameObject cam;
    public float parallaxEffect;

    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        /*
        total = GetComponent<SpriteRenderer>().;
        name = GetComponent<SpriteRenderer>().name;
        Debug.Log("name: " + name);
        Debug.Log("lenght: " + length);
        Debug.Log("total: " + total);*/


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1-parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);
        
        transform.position = new Vector3(startpos+dist, transform.position.y, transform.position.z);
        
        if(temp>startpos+length) startpos += length;
        else if(temp<startpos-length) startpos -= length;
    }
}
