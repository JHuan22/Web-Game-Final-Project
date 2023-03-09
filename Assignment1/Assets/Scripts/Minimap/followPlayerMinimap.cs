using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayerMinimap : MonoBehaviour
{
    public Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = transform.position;
        newPosition.x = playerTransform.position.x;
        newPosition.z = playerTransform.position.z;
        transform.position = newPosition;
    }
}
