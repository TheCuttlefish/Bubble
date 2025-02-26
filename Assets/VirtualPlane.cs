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

    Seed seed;
    float timer;

    void Start()
    {
        seed = transform.parent.GetComponent<Seed>();
 
        player = GameObject.Find("player");
        foreach (Transform child in transform) maps.Add(child.transform); // add all maps to the list

        InvokeRepeating("CheckAndMoveChunk",0, 0.5f); // check to swap every half a second
        if (transform.position != Vector3.zero) SwapMap();// add a map if you're not at origin point!

    }

    


    void CheckAndMoveChunk()
    {

        if (Vector2.Distance(transform.position, player.transform.position) < 100) return; //only swap when dist is more than 100 units!
        //this is optimisation in case player spends time in same area for a while, then chunks will not try to update

        Vector3 playerPos = player.transform.position;
        Vector3 moveDirection = Vector3.zero;  // Default to no movement

        // Check if the chunk is too far from the player
        if (transform.position.x > playerPos.x + planeHalf)moveDirection.x = -planeSize;  // Move left by 50
        if (transform.position.x < playerPos.x - planeHalf) moveDirection.x = planeSize;   // Move right by 50
        if (transform.position.y > playerPos.y + planeHalf)  moveDirection.y = -planeSize;  // Move down by 50
        if (transform.position.y < playerPos.y - planeHalf) moveDirection.y = planeSize;   // Move up by 50

        // If movement is required, move the chunk and swap its contents
        if (moveDirection != Vector3.zero)
        {
            transform.position += moveDirection;
            SwapMap();  // Only swap if movement happened
        }

    }



    private Transform activeMap;
    void SwapMap()
    {
        if (activeMap) activeMap.gameObject.SetActive(false);

        int chunkHash = Mathf.Abs((int)transform.position.x * 73856093 ^ (int)transform.position.y * 19349663);

        float distance = Vector2.Distance(transform.position, Vector2.zero)/1000;

        //-- difficulty based on distance
   
        if (distance < 0.5f)                        activeMap = maps[    seed.Next(chunkHash, 4)];    // chunk hash, number of outcomes    (start from 0 and add random 4 )
        else if (distance > 0.5f || distance < 10)  activeMap = maps[4 + seed.Next(chunkHash, 5)];// chunk hash, number of outcomes + 4 (start from 4 and add random 5)

        activeMap.gameObject.SetActive(true);
        transform.localEulerAngles = new Vector3(0, 0, 90 * seed.Next(chunkHash, 4));

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
        Gizmos.DrawCube(transform.position + new Vector3(0, 100 / 4,  0), new Vector3(100, 5f, 1));
        Gizmos.DrawCube(transform.position + new Vector3(0, -100 / 4, 0), new Vector3(100, 5f, 1));
        Gizmos.DrawCube(transform.position + new Vector3(100 / 4, 0 , 0), new Vector3(5f, 100, 1));
        Gizmos.DrawCube(transform.position + new Vector3( -100 / 4,0, 0), new Vector3(5f, 100, 1));
    }
}