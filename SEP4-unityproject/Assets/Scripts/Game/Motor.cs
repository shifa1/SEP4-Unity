using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motor : MonoBehaviour {

    private const float TIME_BEFORE_START = 3.0f;

    public float moveSpeed = 5.0f;
    public float drag = 0.5f;
    public float jumpforce = 7;
    public float terminalRotationSpeed = 25.0f;
    public VortualJoystick moveJoystick;

    
    private Rigidbody controller;
    private Transform camTransform;

    private float startTime = 0;

    private void Start()
    {
        controller = GetComponent<Rigidbody>();
        controller.maxAngularVelocity = terminalRotationSpeed;
        controller.drag = drag;
        startTime = Time.time;

        camTransform = Camera.main.transform;
    }

    private void Update()
    {
        if (Time.time - startTime < TIME_BEFORE_START)
            return;

        Vector3 dir = Vector3.zero;
        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");

        //if you run in diagonally you will run with the same speed as forward
        if (dir.magnitude > 1)
            dir.Normalize();

        if (moveJoystick.inputDirection != Vector3.zero)
        {
            dir = moveJoystick.inputDirection;
        }

        //rotate direction vector with camera
        Vector3 rotatedDir = camTransform.TransformDirection(dir);
        rotatedDir = new Vector3(rotatedDir.x, 0, rotatedDir.z);
        rotatedDir = rotatedDir.normalized * dir.magnitude;

        controller.AddForce(rotatedDir * moveSpeed);
       


    }

    public void Jump()
    {
        if (Time.time - startTime < TIME_BEFORE_START)
            return;
        controller.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);

    }
}
