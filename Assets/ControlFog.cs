using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlFog : MonoBehaviour
{
    public Material fogMat;
    Transform playerPos;
    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.Find("player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        fogMat.SetVector("PlayerPos", playerPos.position);
    }
}
