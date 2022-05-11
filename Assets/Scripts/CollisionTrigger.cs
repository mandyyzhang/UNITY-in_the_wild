using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTrigger : MonoBehaviour
{
    public bool collidedTop = false; 
    public bool collidedLeft = false;
    public bool collidedRight = false;
    public bool collidedBottom = false;

    void Update() {

    }
    
    public void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") { 

            Debug.Log("Collided with game object name = " + gameObject.name); 

            if (gameObject.name == "EdgeTriggerTop") {
                collidedTop = true; 
            }
            if (gameObject.name == "EdgeTriggerBottom") {
                collidedBottom = true; 
            }
            if (gameObject.name == "EdgeTriggerRight") {
                collidedRight = true; 
            }
            if (gameObject.name == "EdgeTriggerLeft") {
                collidedLeft = true; 
            }
            
        }
         
    }

} 
