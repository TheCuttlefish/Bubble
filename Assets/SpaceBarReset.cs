using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
public class SpaceBarReset : MonoBehaviour
{
    CanvasGroup cGroup;
    float showAlpha = 0f;
    float dt;
    float showUITimer = 0;
    float exitTimer = 0;
    bool startExitTimer = false;
    public UnityEvent fadeScreen;
    // Start is called before the first frame update
    void Start()
    {
        cGroup = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {

        showUITimer += Time.deltaTime;

        if (showUITimer > 3) // wait for 3 seconds
        {

            dt = Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                if (!startExitTimer) {  

                    startExitTimer = true;
                    fadeScreen.Invoke();

                }

                showAlpha = 1f;


            }

            cGroup.alpha = showAlpha * Mathf.Clamp( showUITimer - 2,0,1 ) ;
            showAlpha -= (showAlpha - 0.1f) / 0.2f * dt;


        }

        if (startExitTimer)
        {

            

            exitTimer += Time.deltaTime;
            if(exitTimer > 0.5f) SceneManager.LoadScene(0);

        }

    }
}
