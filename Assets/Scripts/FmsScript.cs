using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FmsScript : MonoBehaviour
{
    private CharacterController controller;
    public AudioSource walksound;
    public GameObject spawn;
    private GameObject enemy;
    public Transform cameraTransform;
    public float playerSpeed = 10;
    public bool isBeingChased;
    public int lives;
    private bool isCrouched;
    public bool hasCat;
    public bool canDie;
    public bool hasBeenCaught;
    public bool isPaused = false;
    public bool hasKey;
    public float mouseSensitivity = 3;
    private Vector2 look;
    public AudioSource pickup;

    // Variables for grabbing and throwing the ball
    public GameObject heldBall;

    public Transform holdPoint;
    public float throwForce = 15f;
    public bool isHoldingBall = false;
    private PanelManager panelManager;

    public Transform teleportTargetRed;
    public Transform teleportTargetBlue;
    private bool isInKeyPad = false;
    public GameObject keyPad;
    private Vector3 velocity;
    private float mass = 2f;
    public float jumpSpeed = 10f;

    private void Awake()
    {
        canDie = true;
        controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        panelManager = FindObjectOfType<PanelManager>();
        lives = 3;
        isBeingChased = false;
        isCrouched = false;
        hasCat = false;
        hasKey = false;
    }

    private void Update()
    {
        if (!isPaused)
        {
            UpdateLook();
            UpdateMovement();
            UpdateGravity();
            caught();
        }
        if(heldBall == null)
        {
            isHoldingBall = false;
        }
        if (keyPad != null)
        {
            if (!isInKeyPad)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    OpenKeyPad();
                }
            }
            else if (isInKeyPad)
            {
                isPaused = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    isInKeyPad = false;
                    keyPad.SetActive(false);
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    isPaused = false;
                }
            }
        }
        {
        }
        if (isHoldingBall)
        {
            if (Input.GetMouseButtonDown(1)) // Right mouse button to throw
            {
                ThrowBall();
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.E)) // E key to grab
            {
                TryGrabBall();
            }
        }
    }

    private void UpdateLook()
    {
        look.x += Input.GetAxis("Mouse X") * mouseSensitivity;
        look.y += Input.GetAxis("Mouse Y") * mouseSensitivity;
        look.y = Mathf.Clamp(look.y, -90, 90);
        cameraTransform.localRotation = Quaternion.Euler(-look.y, 0, 0);
        transform.localRotation = Quaternion.Euler(0, look.x, 0);
    }

    private void UpdateMovement()
    {
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");
        var input = new Vector3();
        input += transform.forward * z;
        input += transform.right * x;
        input = Vector3.ClampMagnitude(input, 1f);

        if (Input.GetButtonDown("Jump") && controller.isGrounded && !isCrouched)
        {
            velocity.y += jumpSpeed;
            transform.SetParent(null); // Unparent when jumping
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            Crouch();
        }
        else
        {
            Stand();
        }

        // Move the player based on input
        Vector3 movement = input * playerSpeed + velocity;
        controller.Move(movement * Time.deltaTime);

        // Apply gravity
        var gravity = Physics.gravity * mass * Time.deltaTime;
        velocity.y = controller.isGrounded ? -1 : velocity.y + gravity.y;

        // Handle walking sound
        HandleWalkingSound(input);


    }

    private void HandleWalkingSound(Vector3 input)
    {
        if (input.magnitude > 0)
        {
            if (!walksound.isPlaying)
            {
                walksound.Play();
            }

            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                walksound.pitch = 1.5f;
                playerSpeed = 15f;
            }
            else if (isCrouched)
            {
                walksound.pitch = 0.75f;
                playerSpeed = 7.5f;
            }
            else
            {
                walksound.pitch = 1f;
                playerSpeed = 10f;

            }
        }
        else
        {
            if (walksound.isPlaying)
            {
                walksound.Stop();
            }
        }
    }


    private void UpdateGravity()
    {
        var gravity = Physics.gravity * mass * Time.deltaTime;
        velocity.y = controller.isGrounded ? -1 : velocity.y + gravity.y;
    }

    private void Crouch()
    {
        if (!isCrouched)
        {
            this.gameObject.layer = isBeingChased ? 8 : 0;

            // Adjust CharacterController for crouching
            controller.height = 2f; // Half the original height
            controller.center = new Vector3(0f, -1f, 0f); // Adjust the center to keep the bottom at the same Y level

            // Adjust CapsuleCollider for crouching (if needed)
            GetComponent<CapsuleCollider>().height = 2f;
            GetComponent<CapsuleCollider>().center = new Vector3(0f, -1f, 0f);

            // Adjust the camera for crouching
            cameraTransform.localPosition = new Vector3(0f, 0.5f, 0f); // Lower the camera

            isCrouched = true;
        }
    }

    private void Stand()
    {
        if (isCrouched)
        {
            this.gameObject.layer = 8;
            // Adjust CharacterController back to standing
            controller.height = 4f; // Full height
            controller.center = new Vector3(0f, 0f, 0f); // Reset the center

            // Adjust CapsuleCollider back to standing (if needed)
            GetComponent<CapsuleCollider>().height = 4f;
            GetComponent<CapsuleCollider>().center = new Vector3(0f, 0f, 0f);

            // Adjust the camera back to standing
            cameraTransform.localPosition = new Vector3(0f, 2f, 0f); // Raise the camera

            isCrouched = false;
        }
    }

    private void OpenKeyPad()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        Debug.DrawRay(cameraTransform.position, cameraTransform.forward * 3f, Color.red, 3f);
        if (Physics.Raycast(ray, out RaycastHit hit, 3f))
        {
            if (hit.collider.CompareTag("KeyPad"))
            {
                isInKeyPad = true;
                keyPad.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                isPaused = true;
            }
        }
    }

    private void TryGrabBall()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, 7f))
        {
            if (hit.collider.CompareTag("Ball"))
            {
                heldBall = hit.collider.gameObject;
                heldBall.GetComponent<Rigidbody>().isKinematic = true;
                heldBall.transform.SetParent(holdPoint);
                heldBall.transform.localPosition = Vector3.zero;
                heldBall.transform.localRotation = Quaternion.identity;
                isHoldingBall = true;
            }
        }
    }

    private void ThrowBall()
    {
        if (heldBall != null)
        {
            heldBall.transform.SetParent(null);
            Rigidbody rb = heldBall.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.AddForce(cameraTransform.forward * throwForce, ForceMode.VelocityChange);
            heldBall = null;
            isHoldingBall = false;
        }
    }

    private void caught()
    {
        if (hasBeenCaught)
        {
            Debug.Log("Caught");
            hasCat = false;
            transform.position = spawn.transform.position;
            StartCoroutine(WaitRoutine());
            hasBeenCaught = false;
            canDie = true;
            heldBall = null;
            isHoldingBall = false;
            if (panelManager != null)
            {
                panelManager.ResetPanels();
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cat"))
        {
            hasCat = true;
            other.gameObject.SetActive(false);
            pickup.Play();

        }
        if (other.CompareTag("Key"))
        {
            hasKey = true;

        }
        if (other.CompareTag("Enemy") && canDie)
        {
            canDie = false;
            hasBeenCaught = true;
            enemy = other.gameObject;
            enemy.GetComponent<CapsuleCollider>().isTrigger = false;
        }
    }


        public void respawn()
    {
        Debug.Log("Respawn");
        controller.enabled = false; // Disable the controller to avoid collisions during repositioning
        transform.position = spawn.transform.position;
        controller.enabled = true;  // Re-enable the controller after repositioning
        canDie = true;

        // Reset variables
        hasBeenCaught = false;
        isBeingChased = false;
        velocity = Vector3.zero;

        if (panelManager != null)
        {
            canDie = true;

            panelManager.ResetPanels();
        }

        // Additional respawn logic can go here
    }

    private IEnumerator WaitRoutine()
    {
        yield return new WaitForSeconds(3);
        canDie = true;
        if (enemy != null)
        {
            enemy.GetComponent<CapsuleCollider>().isTrigger = true;
        }
    }
}