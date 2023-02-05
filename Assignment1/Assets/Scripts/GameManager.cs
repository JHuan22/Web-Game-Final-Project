using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject canvas;
    public static bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            canvas.SetActive(!canvas.activeSelf);
            Time.timeScale = canvas.activeSelf ? 0 : 1;
            isPaused = canvas.activeSelf;
            Cursor.lockState = canvas.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }
}
