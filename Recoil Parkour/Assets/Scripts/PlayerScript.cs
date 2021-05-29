using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody playerRB;
    public CharacterController controller;
    

    [Header("Movement")]
    public float speed;
    public float jumpHeight;
    
    public float gravity;
    public Vector3 velocity;
    public float drag;

    [Header("GroundCheck")]
    public LayerMask groundLayer;
    public float rayLength;

    void Start()
    {
        
    }

    void Update()
    {
        velocity *= Mathf.Clamp01(1f - drag * Time.deltaTime);
        HandleMovement();
    }

    void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        velocity.y += gravity * Time.deltaTime;

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime);

        if(GroundCheck())
        {
            if(velocity.y < 0) velocity.y = 0f;
            
        }
        if(Input.GetKeyDown(KeyCode.Space) && GroundCheck())
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            Debug.Log("Jump button pressed");
        }
    }

    public bool GroundCheck()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * rayLength, Color.red);

        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, rayLength, groundLayer))
        {
            
            //Debug.Log("Touched Ground");
            return true;
        }
        else
        {
           //Debug.Log("Didn't hit");
            return false;
        }
    }
}
