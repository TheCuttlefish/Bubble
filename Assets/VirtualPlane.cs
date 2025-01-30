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
        foreach (Transform child in transform) maps.Add(child.transform);

        InvokeRepeating("CheckAndMoveChunk",0, 0.5f);

    }

   
    void CheckAndMoveChunk()
    {

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

        activeMap = maps[seed.Next(chunkHash, maps.Count)]; // Directly store GameObject
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
        Gizmos.DrawCube(transform.position + new Vector3(0, 100 / 4,0), new Vector3(100, 5f, 1));
        Gizmos.DrawCube(transform.position + new Vector3(0, -100 / 4, 0), new Vector3(100, 5f, 1));
        Gizmos.DrawCube(transform.position + new Vector3(100 / 4, 0 , 0), new Vector3(5f, 100, 1));
        Gizmos.DrawCube(transform.position + new Vector3( -100 / 4,0, 0), new Vector3(5f, 100, 1));
    }
}