using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CollectablePull : MonoBehaviour
{

    bool pull;
    Transform player;
    float pullForce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pull)
        {
            pullForce += Time.deltaTime * 10;
            transform.parent.position -= ( transform.parent.position - player.position) * Time.deltaTime* ( pullForce);
        }

        

    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.transform.name == "player")
        {
            player = collision.transform;
            pull = true;
            
        }
    }
}
