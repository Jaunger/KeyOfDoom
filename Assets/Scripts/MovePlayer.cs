using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{

    CharacterController charactercontroller;
    public Transform cameraTransform;
    public float playerspeed = 5;

    //view parameter
    public float mouseSensitivy = 3;
    Vector2 look;

    //Gravity
    float mass = 1;
    Vector3 velocity;
    public float jumpSpeed = 5;
    private void Awake()
    {
        charactercontroller = GetComponent<CharacterController>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLook();

        UpdateMovement();

        UpdateGravity();

    }

    private void UpdateGravity()
    {
        var gravity = Physics.gravity * mass * Time.deltaTime;

        velocity.y = charactercontroller.isGrounded ? -1 : velocity.y + gravity.y;


    }

    private void UpdateMovement()
    {
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");
        var input = new Vector3();
        input += transform.forward * z;
        input += transform.right * x;
        input += Vector3.ClampMagnitude(input, 1);

        if (Input.GetButtonDown("Jump") && charactercontroller.isGrounded)
        {
            velocity.y += jumpSpeed;
        }
        charactercontroller.Move((input * playerspeed + velocity) * Time.deltaTime);

    }

    private void UpdateLook()
    {
        look.x += Input.GetAxis("Mouse X") * mouseSensitivy;
        look.y += Input.GetAxis("Mouse Y") * mouseSensitivy;
        look.y = Mathf.Clamp(look.y, -90, 90); //cant look behind back
        cameraTransform.localRotation = Quaternion.Euler(-look.y, 0, 0);
        transform.localRotation = Quaternion.Euler(0, look.x, 0);

    }
}
