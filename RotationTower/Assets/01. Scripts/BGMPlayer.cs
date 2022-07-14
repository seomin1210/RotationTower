using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : AudioPlayer
{
    [SerializeField]
    private AudioClip startBGM;
    [SerializeField]
    private AudioClip gameBGM;

    public void OnStartBGM()
    {
        PlayClip(startBGM);
    }

    public void OnGameBGM()
    {
        PlayClip(gameBGM);
    }
}
