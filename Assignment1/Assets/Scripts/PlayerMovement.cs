using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    [Header("Movement")]
    public float maxSpeed = 3.0f;
    public float gravity = -9.81f;

    public float fallSpeed = -3.0f;
    public float jumpHeight = 1.0f; 
    public Vector3 velocity;

    [Header("Jump")]
    public Transform groundCheck;
    public float groundRadius = 0.15f; 
    public LayerMask groundMask;
    public bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //ground check
        isGrounded = Physics.CheckSphere(groundCheck.position,groundRadius,groundMask);

        //makes sure we are grounded at all times
        if(!isGrounded && velocity.y < 0.0f){
            velocity.y = fallSpeed;
        }

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * y; 
        controller.Move(move * maxSpeed * Time.deltaTime);

        if(Input.GetButton("Jump") && isGrounded){
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider hit){
        if(hit.gameObject.tag == "Killers"){
            Debug.Log("reset player now");
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
}
