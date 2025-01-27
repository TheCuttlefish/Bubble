using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInScreen : MonoBehaviour
{
    CanvasGroup cGroup;
    float showAlpha = 1f;
    float timer = 1;

    private void Awake()
    {
        cGroup = GetComponent<CanvasGroup>();
        cGroup.alpha = 1;
    }

    void Update()
    {

        timer -= Time.deltaTime;
        cGroup.alpha = timer;

        if (timer < 0) Destroy(gameObject);
    }
}
