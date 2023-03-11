using System.Collections;
using UnityEngine;

public class PlatformAppearDisappear : MonoBehaviour
{
    public float minStartDelay = 0f; // The minimum delay before the platform starts disappearing
    public float maxStartDelay = 5f; // The maximum delay before the platform starts disappearing
    public float disappearDuration = 5f; // The duration of the platform disappearing
    public float appearDuration = 1f; // The duration of the platform appearing

    private MeshRenderer meshRenderer; // Reference to the mesh renderer component
    private BoxCollider collider; // Reference to the collider component
    private bool isPlatformActive = true; // Whether the platform is currently active
    private float timer = 0f; // Timer used for calculating when to make the platform disappear or reappear

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        collider = GetComponent<BoxCollider>();

        // Generate a random start delay
        float startDelay = Random.Range(minStartDelay, maxStartDelay);

        // Delay the start of the disappearing/reappearing process
        Invoke("StartDisappearReappear", startDelay);
    }

    private void StartDisappearReappear()
    {
        // Start the disappearing/reappearing process
        StartCoroutine(DisappearReappear());
    }

    private IEnumerator DisappearReappear()
    {
        // Calculate the duration to wait before starting the disappearing process
        float waitDuration = isPlatformActive ? disappearDuration : appearDuration;

        // Wait for the specified duration
        yield return new WaitForSeconds(waitDuration);

        // Invert the platform's active state
        isPlatformActive = !isPlatformActive;

        // Activate or deactivate the mesh renderer and collider based on the active state
        meshRenderer.enabled = isPlatformActive;
        collider.enabled = isPlatformActive;

        // Start the disappearing/reappearing process again
        StartCoroutine(DisappearReappear());
    }
}
