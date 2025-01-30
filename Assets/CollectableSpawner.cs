using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{

    public GameObject collectable;
    private GameObject collectableRef;

    private void OnEnable()
    {
        if (collectable) // Only instantiate if a collectable is assigned
        {
            collectableRef = Instantiate(collectable, transform.position, transform.rotation);
            collectableRef.transform.parent = transform;
        }
    }

    private void OnDisable()
    {
        if (collectableRef) // Destroy the collectable when the chunk is disabled
        {
            Destroy(collectableRef);
            collectableRef = null; // Ensure it's reset to null
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0.6f, 0) ;
        Gizmos.DrawSphere(transform.position + new Vector3(0.4f,-0.3f, 0), 0.2f);
        Gizmos.DrawSphere(transform.position + new Vector3(-0.4f, -0.3f, 0), 0.2f);
        Gizmos.DrawSphere(transform.position + new Vector3(0f, 0.4f, 0), 0.2f);
    }

}
