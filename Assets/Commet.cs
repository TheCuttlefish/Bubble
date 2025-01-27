using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commet : MonoBehaviour
{
    Vector2 dir;
    Transform player;
    float timer;
    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.Find("player").transform;
        dir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        
   
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;

        if(timer > 3) transform.Translate( dir * Time.deltaTime * 30, Space.Self); // wait 3 seconds

        if (transform.position.x > player.transform.position.x + 50)
        {
            ChangeDir();
            transform.Translate(-100, 0, 0);
        }
        if (transform.position.x < player.transform.position.x - 50)
        {
            ChangeDir();
            transform.Translate(100, 0, 0);
        }
        if (transform.position.y > player.transform.position.y + 50)
        {
            ChangeDir();
            transform.Translate(0, -100, 0);
        }
        if (transform.position.y < player.transform.position.y - 50)
        {
            ChangeDir();
            transform.Translate(0, 100, 0);
        }



    }


    void ChangeDir()
    {
        timer = 0;
        GetComponent<TrailRenderer>().Clear();
        dir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    } 
}
