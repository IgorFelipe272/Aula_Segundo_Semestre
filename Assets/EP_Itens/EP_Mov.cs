using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class EP_Mov : MonoBehaviour
{
    public float speed = 100f;        
    public float gravity = -9.81f;  
    public float jumpHeight = 2f;   

    private Vector3 velocity;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
   
        // Pega entrada de movimento (WASD ou setas)
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        rb.linearVelocity = (move * speed * Time.deltaTime);

    }

}