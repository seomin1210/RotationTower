using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleAudioPlayer : AudioPlayer
{
    [SerializeField]
    private AudioClip rollSound = null, crashSound = null;

    public void PlayRoll()
    {
        PlayClip(rollSound);
    }

    public void PlayCrash()
    {
        PlayClip(crashSound);
    }
}
