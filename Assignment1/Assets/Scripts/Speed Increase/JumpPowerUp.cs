using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPowerUp : MonoBehaviour
{
    public float jumpIncrease = 2.0f;
    public float duration = 2.0f;
    private float originalJumpHeight;

    public Color newColor = Color.white;


    private void Start() {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        renderer.material.color = newColor;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
            PlayerMovement playerScript = other.GetComponent<PlayerMovement>();
            originalJumpHeight = playerScript.jumpHeight;
            playerScript.jumpHeight += jumpIncrease;
            StartCoroutine(revertSpeed(playerScript));
            MeshRenderer renderer = GetComponent<MeshRenderer>();
            SphereCollider collider = GetComponent<SphereCollider>();
            Destroy(renderer);
            Destroy(collider);
        }
    }

    private IEnumerator revertSpeed(PlayerMovement playerScript){
        yield return new WaitForSeconds(duration);
        playerScript.jumpHeight = originalJumpHeight;
    }
}
