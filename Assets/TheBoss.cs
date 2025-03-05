using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheBoss : MonoBehaviour
{

    public AudioListener allSound;
    public AudioSource musicSource;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))// toggle all sound
        {
            THE_BOSS.ALL_SOUND_ON = !THE_BOSS.ALL_SOUND_ON;
            allSound.enabled = THE_BOSS.ALL_SOUND_ON;
        }
        if (Input.GetKeyDown(KeyCode.N))// toggle music on/ off
        {
            THE_BOSS.MUSIC_ON = !THE_BOSS.MUSIC_ON;
            musicSource.enabled = THE_BOSS.MUSIC_ON;
        }
    }
}
