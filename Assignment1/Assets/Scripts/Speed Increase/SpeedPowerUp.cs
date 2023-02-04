using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{
    public float speedIncrease = 2.0f;
    public float duration = 2.0f;
    private float originalMaxSpeed;

    public Color newColor = Color.yellow;


    private void Start() {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        renderer.material.color = newColor;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
            PlayerMovement playerScript = other.GetComponent<PlayerMovement>();
            originalMaxSpeed = playerScript.maxSpeed;
            playerScript.maxSpeed += speedIncrease;
            StartCoroutine(revertSpeed(playerScript));
            MeshRenderer renderer = GetComponent<MeshRenderer>();
            SphereCollider collider = GetComponent<SphereCollider>();
            Destroy(renderer);
            Destroy(collider);
        }
    }

    private IEnumerator revertSpeed(PlayerMovement playerScript){
        yield return new WaitForSeconds(duration);
        playerScript.maxSpeed = originalMaxSpeed;
    }
}
