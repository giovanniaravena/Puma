using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public int HP_MAX;
    int HP_CURR;
    Color myColor;
    // Start is called before the first frame update
    void Start()
    {
        HP_CURR = HP_MAX;
        myColor = this.gameObject.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getDamage(int amount)
    {
        Debug.Log("Enemy: " + this.name + " gets damage: " + amount + ".");
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
}
