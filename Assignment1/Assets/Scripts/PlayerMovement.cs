using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerMovement : Subject
{
    private int itemsToCollect = 2;
    private int itemsCollected = 0;

    public int health = 3;
    public float invulnerabilityDuration = 3f;
    private bool isInvulnerable = false;
    private float invulnerabilityTimer = 0f;

    [Header("Controller")]
    public Joystick joystick;
    public float horiSens;
    public float vertSens;


    public Canvas inventoryCanvas;
    public TMP_Text inventoryCounter;
    public Canvas winCanvas;
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
        winCanvas.gameObject.SetActive(false);
        health = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInvulnerable)
        {
            invulnerabilityTimer -= Time.deltaTime;
            if (invulnerabilityTimer <= 0f)
            {
                isInvulnerable = false;
            }
        }

        if(health == 0){
            ResetScene();
        }

        if(itemsCollected == itemsToCollect){
            winCanvas.gameObject.SetActive(true);
        }
        //ground check
        isGrounded = Physics.CheckSphere(groundCheck.position,groundRadius,groundMask);

        //makes sure we are grounded at all times
        if(!isGrounded && velocity.y < 0.0f){
            velocity.y = fallSpeed;
        }

#if !UNITY_ANDROID
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
#elif UNITY_ANDROID
        float x = joystick.Horizontal;
        float y = joystick.Vertical;
#endif

        Vector3 move = transform.right * x + transform.forward * y; 
        if(playerMovement == true){
            controller.Move(move * maxSpeed * Time.deltaTime);
        }
        
#if !UNITY_ANDROID
        if(Input.GetButton("Jump") && isGrounded){
            pJump();
        }
#endif
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

#if !UNITY_ANDROID
    if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryCanvas.gameObject.SetActive(!inventoryCanvas.gameObject.activeSelf);
        }
#endif
    inventoryCounter.text = itemsCollected.ToString();
    }



    private void OnTriggerEnter(Collider hit){
        if(hit.gameObject.tag == "Killers" && !isInvulnerable){
            //playerMovement = false;
            Hurt.Play();
            health--;
            NotifyObservers(health);
            isInvulnerable = true;
            invulnerabilityTimer = invulnerabilityDuration;
            Debug.Log(health);
            //Invoke("ResetScene", Hurt.clip.length);
        }

        if(hit.gameObject.tag == "Speed" || hit.gameObject.tag == "Jump"){
            ItemPickup.Play();
        }

        if(hit.gameObject.tag == "Win"){
            Debug.Log("you win");
            health = 3;
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        if(hit.gameObject.tag == "winCollect"){
            itemsCollected++;
            Debug.Log("collected: " + itemsCollected);
            MeshRenderer renderer = hit.GetComponent<MeshRenderer>();
            SphereCollider collider = hit.GetComponent<SphereCollider>();
            Destroy(renderer);
            Destroy(collider);
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
    public void SaveButton_Pressed()
    {
        SaveSystem.SavePlayer(this.GetComponent<PlayerMovement>());
    }
    public void LoadButton_Pressed()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        if(data != null)
        {
            controller.enabled = false;

            health = data.health;
            Vector3 position;
            position.x = data.position[0];
            position.y = data.position[1];
            position.z = data.position[2];

            transform.position = position;

            controller.enabled = true;

        }
        else
        {
            Debug.Log("No save data found");
        }
    }
    
    //jump logic
    private void pJump(){
        Jump.Play();
        velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
    }
    //jump button logic, dont need GetButton
    public void JumpButtonPressed(){
        if(isGrounded){
            pJump();
        }

    }

    public void openInv(){
        inventoryCanvas.gameObject.SetActive(!inventoryCanvas.gameObject.activeSelf);
    }
}
