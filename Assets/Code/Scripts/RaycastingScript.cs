using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastingScript : MonoBehaviour
{
    Transform ThisTransform;
    public GameObject theBall;

    // Start is called before the first frame update
    void Start()
    {

        gameObject.transform.rotation.SetLookRotation(new Vector3(0,0,0));
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = theBall.transform.position;
    }
}
