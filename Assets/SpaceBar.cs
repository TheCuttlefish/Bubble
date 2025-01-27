using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class SpaceBar : MonoBehaviour
{
    CanvasGroup cGroup;
    float showAlpha = 0f;
    float dt;
    bool interactedWithUI = false;
    float hideUITimer = 1;
    // Start is called before the first frame update
    void Start()
    {
        cGroup = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        dt = Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) )
        {
            showAlpha = 1f;
            interactedWithUI = true;
        }

        cGroup.alpha = showAlpha * hideUITimer;
        showAlpha -= (showAlpha - 0.1f) / 0.2f * dt;



        if (interactedWithUI)
        {
            hideUITimer -= Time.deltaTime/5; //5 seconds
            if(hideUITimer < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
