using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTrigger : MonoBehaviour
{
    public bool collidedTop = false; 
    public bool collidedLeft = false;
    public bool collidedRight = false;
    public bool collidedBottom = false;

    //public Vector3 turnAroundRotation = new Vector3(0, 180, 0); 
    public float pushForwardValue = 16.0f; 

    // referencing components in other objects here 
    //private FirstPersonController fpc;  
    private Rigidbody playerBody; 
    [SerializeField] private GameObject player; 

    private SceneChanger sc; 
    [SerializeField] private GameObject SceneManager; 

    void Awake() {
        //fpc = player.GetComponent<FirstPersonController>(); 
        playerBody = player.GetComponent<Rigidbody>();
        sc = SceneManager.GetComponent<SceneChanger>();  
    }

    void Update() {

        #region Reject the player from falling off the world by push them back to where they came from 

        if (collidedTop && !sc.collectedAllGems) {
            //playerBody.transform.Rotate(turnAroundRotation);
            playerBody.transform.Translate(transform.forward * -pushForwardValue * Time.deltaTime);
            collidedTop = false; 
        }
        if (collidedBottom && !sc.collectedAllGems) {
            //playerBody.transform.Rotate(turnAroundRotation);
            playerBody.transform.Translate(transform.forward * -pushForwardValue * Time.deltaTime);
            collidedBottom = false; 
        }
        if (collidedRight && !sc.collectedAllGems) {
            //playerBody.transform.Rotate(turnAroundRotation);
            playerBody.transform.Translate(transform.forward * -pushForwardValue * Time.deltaTime);
            collidedRight = false; 
        }
        if (collidedLeft && !sc.collectedAllGems) {
            //playerBody.transform.Rotate(turnAroundRotation);
            playerBody.transform.Translate(transform.forward * -pushForwardValue * Time.deltaTime);
            collidedLeft = false; 
        }

        #endregion 
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
