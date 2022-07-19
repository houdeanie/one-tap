using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    private Vector3 moveVelocity = Vector3.zero;
    private Vector3 rotateVector = Vector3.zero;
    private Vector3 rotateCameraVector = Vector3.zero;

    private Rigidbody rb;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
    }

    // takes a velocity vector
    public void Move (Vector3 velocity)
    {
        moveVelocity = velocity;
    }

    // run every physics iteration
    void FixedUpdate ()
    {
        PerformMovement();
        PerformRotation();
    }

    // perform movement based on velocity variable
    void PerformMovement ()
    {
        if (moveVelocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
        }
    }

    public void Rotate (Vector3 rotation)
    {
        rotateVector = rotation;
    }

    public void RotateCamera(Vector3 cameraRotation)
    {
        rotateCameraVector = cameraRotation;
    }

    // perform rotation
    void PerformRotation ()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotateVector));
        if (cam != null)
        {
            cam.transform.Rotate(-rotateCameraVector);
        }
    }

}
