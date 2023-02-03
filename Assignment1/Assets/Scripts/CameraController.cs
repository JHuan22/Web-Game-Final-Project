using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float mouseSens = 10.0f;
    public Transform playerBody;
    private float XRotation = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        //locks cursor
        //escape to exit
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Mouse X") * mouseSens;
        float y = Input.GetAxis("Mouse Y") * mouseSens;

        XRotation -= y;
        XRotation = Mathf.Clamp(XRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(XRotation, 0,0);
        playerBody.Rotate(Vector3.up * x);
    }
}
