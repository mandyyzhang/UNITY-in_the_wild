using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public Camera firstPersonCamera;
    public Camera topDownCamera;
    public KeyCode switchCamKey = KeyCode.M; 

    // Start is called before the first frame update
    void Start()
    {
        firstPersonCamera.enabled = true;
        topDownCamera.enabled = false; 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(switchCamKey))
        {
            if (firstPersonCamera.enabled)
            {
                firstPersonCamera.enabled = false;
                topDownCamera.enabled = true;
            }
            else if (topDownCamera.enabled)
            {
                firstPersonCamera.enabled = true;
                topDownCamera.enabled = false;
            }
        }
    }
}
