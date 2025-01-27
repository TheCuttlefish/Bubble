using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{

    public GameObject collectable;
    GameObject collectableRef;

    private void OnEnable()
    {

        if (collectable != null)
        {
            collectableRef = Instantiate(collectable, transform.position, transform.rotation);
            collectableRef.transform.parent = transform;
        }
    }


    private void OnDisable()
    {
        if(collectableRef != null) Destroy(collectableRef);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0.6f, 0) ;
        Gizmos.DrawSphere(transform.position + new Vector3(0.4f,-0.3f, 0), 0.2f);
        Gizmos.DrawSphere(transform.position + new Vector3(-0.4f, -0.3f, 0), 0.2f);
        Gizmos.DrawSphere(transform.position + new Vector3(0f, 0.4f, 0), 0.2f);
    }

}
