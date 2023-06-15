using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheBall : MonoBehaviour
{
    Rigidbody BallsRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        BallsRigidbody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (BallsRigidbody.IsSleeping())
        {
            BallsRigidbody.WakeUp();
        }
    }
    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("SurfaceTag")){

        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bottom"))
        {
            Destroy(this.gameObject);
        }
    }
}
