using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody playerRB;
    public CharacterController controller;
    public GameManager gameManager;

    [Header("Movement")]
    public float speed;
    public float jumpHeight;
    
    public float gravity;
    public Vector3 velocity;
    public float drag;

    [Header("GroundCheck")]
    public LayerMask groundLayer;
    public LayerMask respawnLayer;
    public float rayLength;

    void Start()
    {
        
    }

    void Update()
    {
        velocity *= Mathf.Clamp01(1f - drag * Time.deltaTime); // Adds Drag
        HandleMovement();
    }

    void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        velocity.y += gravity * Time.deltaTime; //Adds Gravity

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime); //Moves charecter with WASD
        controller.Move(velocity * Time.deltaTime); //Moves accordingly to velocity

        if(GroundCheck(groundLayer))
        {
            if(velocity.y < 0) velocity.y = 0f;
            
        }
        if(GroundCheck(respawnLayer))
        {
            gameManager.Respawn(this.gameObject);
        }
        if(Input.GetKeyDown(KeyCode.Space) && GroundCheck(groundLayer))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); //Adds jump
            Debug.Log("Jump button pressed");
        }
    }

    public bool GroundCheck(LayerMask _layer)
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * rayLength, Color.red);

        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, rayLength, _layer))
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
