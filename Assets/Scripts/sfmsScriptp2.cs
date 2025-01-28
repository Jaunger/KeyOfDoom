using UnityEngine;
public class sfmsScriptp2 : MonoBehaviour
{
    CharacterController controller;
    public AudioSource walksound;
    public AudioSource pickup;
    public Transform cameraTransform; // 2 camera
    public float playerSpeed = 5;
    public bool hasKey;
    public float mouseSensivity = 3;     //1
    public Transform teleportTargetRed;
    public Transform teleportTargetBlue;
    public bool isPaused;
    public bool hasCat = false;
    Vector2 look;


    Vector3 velocity; // 7 person
    float mass = 1f;
    public float jumpSpeed = 5f;
    // 1  character
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }
    void Start()
    {
        //2 locked mouse
        Cursor.lockState = CursorLockMode.Locked;
        hasKey = false;  
    }
    void Update()
    {
        if (!isPaused)
        {
            UpdateLook();            //2
            UpdateMovement();        //3
            UpdateGravity();         //4
        }
    }

    void UpdateLook()
    {     //2
        look.x += Input.GetAxis("Mouse X") * mouseSensivity;   //2 camera
        look.y += Input.GetAxis("Mouse Y") * mouseSensivity; //2  camera
                                                             //Returns rotation z,x,y degrees around the z,x,y applied in that order.
        look.y = Mathf.Clamp(look.y, -90, 90);
        cameraTransform.localRotation = Quaternion.Euler(-look.y, 0, 0); //2.1
        transform.localRotation = Quaternion.Euler(0, look.x, 0); //2   palyer
    }
    void UpdateMovement()
    {             //3
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");
        var input = new Vector3();
        input += transform.forward * z;
        input += transform.right * x;
        input = Vector3.ClampMagnitude(input, 1f);

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocity.y += jumpSpeed;
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                playerSpeed = 7.5f;
            }
            walksound.enabled = true;
        }
       
        else
        {
            playerSpeed = 5;
            walksound.enabled = false;
        }
        controller.Move((input * playerSpeed/*9*/ + velocity) * Time.deltaTime);

    }


    private void UpdateGravity()
    {     //4    

        var gravity = Physics.gravity * mass * Time.deltaTime;
        //Check CharacterController touching the ground during the last move?
        velocity.y = controller.isGrounded ? -1 : velocity.y + gravity.y;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Key")
            hasKey = true;
        if (other.CompareTag("Cat"))
        {
            hasCat = true;
            other.gameObject.SetActive(false);
            pickup.Play();
        }
    }
}
