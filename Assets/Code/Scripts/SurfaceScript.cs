using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceScript : MonoBehaviour
{
    public float rotationSpeed = 1f;
    public float scale = 10;

    private void Update()
    {
        
        float rotationX = Input.GetAxis("Vertical") *scale; //inout from up and down arrows
        float rotationZ = Input.GetAxis("Horizontal") *scale; //input from left and right arrows

        Quaternion newRotation = Quaternion.Euler(rotationX, 0f, rotationZ); //Stores rotation

 
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime); //applies rotation based on input
    }
}
