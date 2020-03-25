using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashController : MonoBehaviour
{
    public float delay = 0f;
    public int DAMAGE = 10;
    PlayerMovement player;
    char direction;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        
        Invoke("Finish", 0.25f);
    }

    void Finish()
    {
        player.slash = false;
        Destroy(gameObject);
        
    }
    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            if(direction != player.direction)
            {
                player.slash = false;
                Debug.Log("player slash: " + player.slash + ".");
                Destroy(gameObject);
                return;
            }
            if(player.direction == 'R')
            {
                transform.position = new Vector2(player.transform.position.x + 1.5f, player.transform.position.y - 0.10f);
            }
            else
            {
                transform.position = new Vector2(player.transform.position.x - 1.5f, player.transform.position.y - 0.10f);
            }
            

        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            EnemyController enemy = col.gameObject.GetComponent<EnemyController>();
            enemy.getDamage(DAMAGE);
            //Destroy(this.gameObject);
        }
    }
    public void setPlayer(PlayerMovement player)
    {
        this.player = player;
        direction = player.direction;
        if(player.direction == 'L')
        {
            this.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
