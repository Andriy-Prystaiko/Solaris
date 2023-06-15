using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbiter : MonoBehaviour
{
    // Variable to control speed of rotation compared to mouse movement
    public float mouseSensetivity = 3.0f;

    // X and Y rotation variables
    private float rotationY;
    private float rotationX;

    // Gets the coordinates of the body we are trying to orbit with our camera
    public Transform target;

    // What distance from the target we want to orbit at
    public float distanceFromTarget = 4.0f;

    // Saves the current rotation we are at before changes
    private Vector3 currentRotation;

    // Variable to save the velocity with which we rotate so that the rotation is not instanteneous
    private Vector3 smoothVelocity = Vector3.zero;

    // Factor by which we smooth the rotation
    public float smoothTime = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        // Moves the camera up and down based on mouse movements
        float mouseX = Input.GetAxis("Mouse X") * mouseSensetivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensetivity;

        if (Input.GetMouseButton(1))
        {
            // adds the mouse movement rotations to the current X and Y angle of the camera
            rotationY += mouseX;
            rotationX += mouseY;
        }

        // Limits the vertical angle of the camera
        rotationX = Mathf.Clamp(rotationX, -40, 40);

        // determines the rotation that we need to make based on inputs
        Vector3 nextRotation = new Vector3(rotationX, rotationY, 0);

        // Smoothes the movement by adding a slight input delay and a gradual build to the motion
        currentRotation = Vector3.SmoothDamp(currentRotation, nextRotation, ref smoothVelocity, smoothTime);

        // creates a new camera angle based on the newly calculated mouse movements
        transform.localEulerAngles = currentRotation;

        // Locks the center of the axis of rotation around whatever body we are rotating around
        transform.position = target.position - transform.forward * distanceFromTarget;
    }
}
