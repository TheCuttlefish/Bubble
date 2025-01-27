using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class BG_bubble : MonoBehaviour
{

    Transform player;
    float dist;
    Vector2 dir;
    float burst = 0;
    float posX;
    float posY;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player").transform;
        posX = transform.position.x;
        posY = transform.position.y;
        transform.localScale = new Vector3(1, 1, 1) * Random.Range(0.01f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {

        dist = Vector2.Distance(transform.position, player.position);
        dir = transform.position - player.position;
        if(dist < 1.2f)
        {
            burst = Random.Range(3f,5f);
            
        }

        burst -= (burst - 0) / 0.2f * Time.deltaTime;
        transform.Translate(dir * Time.deltaTime * burst + new Vector2( Mathf.Cos(posX + Time.time)/3, Mathf.Cos(posY + Time.time)/3) * Time.deltaTime, Space.World);


        if (transform.position.x > player.transform.position.x + 50) transform.Translate(-100, 0, 0);
        if (transform.position.x < player.transform.position.x - 50) transform.Translate(100, 0, 0);
        if (transform.position.y > player.transform.position.y + 45) transform.Translate(0, 0, -90);
        if (transform.position.y < player.transform.position.y - 45) transform.Translate(0, 0, 90);
           
        

    }
}
