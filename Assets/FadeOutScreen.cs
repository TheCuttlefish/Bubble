using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOutScreen : MonoBehaviour
{
    CanvasGroup cGroup;
    float showAlpha = 0f;
    float timer = 0;

    private void Awake()
    {
        cGroup = GetComponent<CanvasGroup>();
        cGroup.alpha = 0;
    }

    void Update()
    {

        timer += Time.deltaTime *2;
        cGroup.alpha = timer;

    }
}