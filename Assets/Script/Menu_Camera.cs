using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Camera : MonoBehaviour{
    [SerializeField]
    private Transform targetToFollow;

    // Update is called once per frame
    void Update(){
        transform.position = new Vector3(
            Mathf.Clamp(targetToFollow.position.x, -0.2f, 11.0f),
            Mathf.Clamp(targetToFollow.position.y, -0.0f, 1.0f),
            transform.position.z);
    }
}
