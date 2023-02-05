using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public bool playerMovement = true;
    public CharacterController controller;

    [Header("Effects")]
    public AudioSource Jump;
    public AudioSource Hurt;
    public AudioSource ItemPickup;

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
        if(playerMovement == true){
            controller.Move(move * maxSpeed * Time.deltaTime);
        }
        

        if(Input.GetButton("Jump") && isGrounded){
            Jump.Play();
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider hit){
        if(hit.gameObject.tag == "Killers"){
            playerMovement = false;
            Hurt.Play();
            Invoke("ResetScene", Hurt.clip.length);
        }

        if(hit.gameObject.tag == "Speed"){
            ItemPickup.Play();
        }
    }

    void ResetScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadSceneAsync(sceneName);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
}
