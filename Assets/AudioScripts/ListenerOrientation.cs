using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenerOrientation : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform cameraTransform;

    void Update()
    {
        // Match player position but use camera rotation
        transform.position = playerTransform.position;
        transform.rotation = cameraTransform.rotation;
    }
}

