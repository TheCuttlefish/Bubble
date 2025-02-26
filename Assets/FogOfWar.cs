using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : MonoBehaviour
{

    Transform player;
    float scale = 1;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, player.position) < 7)
        {
            if(scale > 0) 
            scale -= Time.deltaTime * 3;
            else Destroy(gameObject);

            transform.localScale = new Vector3 (scale, scale, scale);
        }
    }
}
