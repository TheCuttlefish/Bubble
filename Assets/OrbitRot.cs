using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitRot : MonoBehaviour
{
    float rotSpeed = 300;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,0,rotSpeed * Time.deltaTime);
           
    }
}
