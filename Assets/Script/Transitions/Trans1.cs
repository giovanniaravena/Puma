using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trans1 : MonoBehaviour{

    public string newScene;
    bool enter = false;
    void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.name == "Player"){
            enter = true;
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        enter = true;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        enter = false;
    }
    void Update(){
        if ((Input.GetButtonDown("Fire1")) && enter)
        {
            SceneManager.LoadScene(newScene, LoadSceneMode.Single);
        }
        
    }
}