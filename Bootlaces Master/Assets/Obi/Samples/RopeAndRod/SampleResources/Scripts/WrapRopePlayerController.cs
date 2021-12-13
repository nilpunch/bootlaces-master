using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WrapRopePlayerController : MonoBehaviour
{
    public float acceleration = 50;
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Vector3.zero;

        // Determine movement direction:
        if (UnityEngine.Input.GetKey(KeyCode.W))
        {
            direction += Vector3.up * acceleration;
        }
        if (UnityEngine.Input.GetKey(KeyCode.A))
        {
            direction += Vector3.left * acceleration;
        }
        if (UnityEngine.Input.GetKey(KeyCode.S))
        {
            direction += Vector3.down * acceleration;
        }
        if (UnityEngine.Input.GetKey(KeyCode.D))
        {
            direction += Vector3.right * acceleration;
        }

        rb.AddForce(direction.normalized * acceleration, ForceMode.Acceleration);
    }

}
