using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniCollectable : MonoBehaviour
{


    Transform pivotPoint;
    // Start is called before the first frame update
    void Start()
    {
        pivotPoint = transform.parent.transform;
        transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (pivotPoint != null) transform.position = pivotPoint.position;
        else Destroy(gameObject);

    }
}
