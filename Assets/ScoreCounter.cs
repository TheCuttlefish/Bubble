using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Image;

public class ScoreCounter : MonoBehaviour
{
    Text score;
    int scoreValue = 0;

    bool countScore = false;

    public void ShowScore()
    {
        countScore = true;
    }
    void Start()
    {
        score = GetComponent<Text>();

    }

    float timer;


    private void Update()
    {
        if (countScore)
        {
            timer += Time.deltaTime;
            if (timer > 0.05f)
            {
                string s = score.text;
                if (s.Length > 0)
                {
                    s = s.Substring(0, s.Length - 1);
                    score.text = s;
                }
                else
                {
                    score.text = scoreValue.ToString();
                    countScore = false;
                }
                timer = 0;
            }
        }
    }


    public void Add()
    {
        scoreValue++;

        score.text += "|";
        if (scoreValue % 5 == 0) score.text += " ";
        if (scoreValue % 25 == 0) score.text += "-- ";
    }
}
