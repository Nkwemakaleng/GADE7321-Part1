using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 2f;
    [SerializeField]  CharacterController controller;
   [SerializeField] private Camera playerCamera;
   private float verticalRotation;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if(!IsGamePaused())
        {
            // Player Movement
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;
            controller.Move(moveDirection * (moveSpeed * Time.deltaTime));

            // Player Rotation
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            verticalRotation -= mouseY;
            verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

            playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
            transform.Rotate(Vector3.up * mouseX);
        }
        
    }
    
    bool IsGamePaused()
    {
        // Check if the pause menu is active
        return Time.timeScale == 0f;
    }
}