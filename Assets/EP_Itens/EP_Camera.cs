using UnityEngine;

public class EP_Camera : MonoBehaviour
{
    public float moveSpeed = 5f;         
    public float lookSensitivity = 2f;    

    float yaw = 0f;  
    float pitch = 0f; 

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -89f, 89f); 

        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);

        float horizontal = Input.GetAxis("Horizontal"); 
        float vertical = Input.GetAxis("Vertical");     
        float upDown = 0f;

        if (Input.GetKey(KeyCode.Space)) upDown = 1f;
        if (Input.GetKey(KeyCode.LeftControl)) upDown = -1f;

        Vector3 move = transform.forward * vertical + transform.right * horizontal + transform.up * upDown;

        transform.position += move * moveSpeed * Time.deltaTime;
    }
}
