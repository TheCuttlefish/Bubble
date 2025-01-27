using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPScounter : MonoBehaviour

{
    public Text fpsDisplay; // Reference to a UI Text element (optional for in-game display)

    private float deltaTime = 0.0f;
    float timerDelay;
    void Update()
    {

        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        timerDelay += Time.deltaTime;

        if (timerDelay > 0.2f)
        {
            // Calculate delta time (time between frames)


            int fps = Mathf.CeilToInt(1.0f / deltaTime);
            fpsDisplay.text = fps + " FPS";
            timerDelay = 0.0f;
        }
        
    }

}