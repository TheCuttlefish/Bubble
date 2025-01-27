using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class VirtualPlane : MonoBehaviour
{

    GameObject player;
    public Color myColour;
    public List<Transform> maps = new List<Transform>();

    int planeSize = 200;
    int planeHalf = 100;


    float timer;

    void Start()
    {

 
        player = GameObject.Find("player");
        foreach (Transform child in transform) maps.Add(child.transform);
    }

    void Update()
    {

        timer += Time.deltaTime;

        if (timer > 0.5f)
        {

            if (transform.position.x > player.transform.position.x + planeHalf)
            {
                transform.position += new Vector3(-planeSize, 0, 0);
               
                SwapMap();

            }

            if (transform.position.x < player.transform.position.x - planeHalf)
            {
                transform.position += new Vector3(planeSize, 0, 0);
                //transform.Translate(planeSize, 0, 0);
                SwapMap();

            }

            if (transform.position.y > player.transform.position.y + planeHalf)
            {
                transform.position += new Vector3(0, -planeSize, 0);
             
                SwapMap();

            }

            if (transform.position.y < player.transform.position.y - planeHalf)
            {
                transform.position += new Vector3(0, planeSize, 0);
                SwapMap();

            }
            timer = 0;

        }
    }

    
    void SwapMap()
    {
        foreach(var _m in maps) _m.transform.gameObject.SetActive(false);
        maps[ Random.Range(0,maps.Count) ].gameObject.SetActive(true);
        transform.localEulerAngles = new Vector3(0, 0, 90 * Random.Range(0,5) );
    }


    private void OnDrawGizmos()
    {
        // Draw a semitransparent red cube at the transforms position
        Gizmos.color = myColour;
        Gizmos.DrawCube(transform.position, new Vector3(100, 100, 1));

        Gizmos.color = new Color(1, 1, 1, 0.1f);
        Gizmos.DrawCube(transform.position, new Vector3(100, 10f, 1));
        Gizmos.DrawCube(transform.position, new Vector3(10f, 100, 1));

        Gizmos.color = new Color(0, 0, 0, 0.4f);
        Gizmos.DrawCube(transform.position + new Vector3(0, 100 / 4,0), new Vector3(100, 5f, 1));
        Gizmos.DrawCube(transform.position + new Vector3(0, -100 / 4, 0), new Vector3(100, 5f, 1));
        Gizmos.DrawCube(transform.position + new Vector3(100 / 4, 0 , 0), new Vector3(5f, 100, 1));
        Gizmos.DrawCube(transform.position + new Vector3( -100 / 4,0, 0), new Vector3(5f, 100, 1));
    }
}