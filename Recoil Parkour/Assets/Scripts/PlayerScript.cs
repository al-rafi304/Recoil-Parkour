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

    private float tempSpeed;

    [Header("GroundCheck")]
    public LayerMask groundLayer;
    public LayerMask respawnLayer;
    public float rayLength;

    [Header("Misc")]
    public float slowMoSpeed;
    public float slowMoDuration;

    private float slowMoTimer;
    [HideInInspector]
    public bool isSlowed;
    private float tempDrag;

    void Start()
    {
        slowMoTimer = 0f;
        isSlowed = false;
        tempSpeed = speed;
    }

    void Update()
    {
        velocity *= Mathf.Clamp01(1f - drag * Time.deltaTime); // Adds Drag
        HandleMovement();

        if(Input.GetKeyDown(KeyCode.LeftShift) && !GroundCheck(groundLayer) && !isSlowed)
        {
            SlowMo(true);
            Debug.Log("Pressed Shift");
            isSlowed = true;

            tempDrag = drag;
            drag = -drag;
            
        }
        if(isSlowed)
        {
            slowMoTimer += Time.deltaTime;
        }
        if(slowMoTimer > slowMoDuration * slowMoSpeed)
        {
            SlowMo(false);

            isSlowed = false;
            slowMoTimer = 0f;

            drag = tempDrag;
        }
        //Debug.Log(slowMoTimer);
    }

    void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        
        velocity.y += gravity * Time.deltaTime; //Adds Gravity
        

        Vector3 move = transform.right * x + transform.forward * z;
        //velocity = move * speed;
        controller.Move(move * speed * Time.deltaTime); //Moves charecter with WASD
        controller.Move(velocity * Time.deltaTime); //Moves accordingly to velocity

        if(GroundCheck(groundLayer))
        {
            if(velocity.y < 0) velocity.y = 0f;
            speed = tempSpeed;
            
        }
        if(GroundCheck(respawnLayer))
        {
            gameManager.Respawn(this.gameObject);
        }
        if(Input.GetKeyDown(KeyCode.Space) && GroundCheck(groundLayer))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); //Adds jump
            //velocity += Camera.main.transform.forward * speed * (move.x + move.y);
            //speed = speed / 4;
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

    public void SlowMo(bool _enable)
    {
        if(_enable)
            Time.timeScale = slowMoSpeed;
        else
            Time.timeScale = 1;
    }
}
