using UnityEngine;

public class PlatformDisappearOnTouch : MonoBehaviour
{
    public float disappearDuration = 3f; // The duration of the platform disappearing

    private bool isPlayerTouching = false; // Whether the player is currently touching the platform
    private float timer = 0f; // Timer used for calculating when to make the platform disappear


    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Working");
            // Set the player touching flag to true
            isPlayerTouching = true;
        }
    }
    

    private void Update()
    {
        // If the player is touching the platform, start the disappearing process
        if (isPlayerTouching)
        {
            timer += Time.deltaTime;

            if (timer >= disappearDuration)
            {
                // Deactivate the platform
                gameObject.SetActive(false);
            }
        }
    }
}
