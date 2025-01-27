using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleRotation : MonoBehaviour
{

    float dt;
    float xRot, yRot, zRot;
    void Start()
    {
        xRot = Random.Range(-10f, 10f);
        yRot = Random.Range(-10f, 10f);
        zRot = Random.Range(-10f, 10f);
        transform.localEulerAngles = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
    }


    void Update()
    {
        dt = Time.deltaTime;
        transform.Rotate(xRot * dt, yRot * dt, zRot * dt);
    }


    private void OnDrawGizmos()
    {
        if (transform.localScale.x < 1f)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 1f);
        }
    }
}
