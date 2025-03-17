using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 3f;
    public Transform cameraTransform;
    public Transform thirdPersonPosition; // Assign a position for third-person view in Inspector
    public Transform firstPersonPosition; // Assign a position for first-person view in Inspector

    private Animator anim;
    private float rotationX = 0f;
    private Rigidbody rb;
    private bool isFirstPerson = false; // Toggle state

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;
        anim = GetComponent<Animator>();

        SwitchPerspective(false); // Start in third-person mode
    }

    void Update()
    {
        HandleMovement();
        HandleCameraRotation();

        anim.SetInteger("State", 0);


        

        if (Input.GetKeyDown(KeyCode.P)) // Press 'P' to switch views
        {
            isFirstPerson = !isFirstPerson;
            SwitchPerspective(isFirstPerson);
        }
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        anim.SetInteger("Walk", 1);

        Vector3 moveDirection = transform.forward * moveZ + transform.right * moveX;
        moveDirection.Normalize();

        Vector3 newPosition = rb.position + moveDirection * moveSpeed * Time.deltaTime;
        rb.MovePosition(newPosition);
    }

    void HandleCameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -60f, 60f);
        cameraTransform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }

    void SwitchPerspective(bool firstPerson)
    {
        if (firstPerson)
        {
            cameraTransform.position = firstPersonPosition.position;
            cameraTransform.rotation = firstPersonPosition.rotation;
            cameraTransform.SetParent(firstPersonPosition); // Attach to first-person position
        }
        else
        {
            cameraTransform.position = thirdPersonPosition.position;
            cameraTransform.rotation = thirdPersonPosition.rotation;
            cameraTransform.SetParent(thirdPersonPosition); // Attach to third-person position
        }
    }
}
