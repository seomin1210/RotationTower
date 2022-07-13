using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleAudioPlayer : AudioPlayer
{
    [SerializeField]
    private AudioClip rollSound = null, crashSound = null;

    public void PlayRoll()
    {
        _audioSource.loop = true;
        PlayClip(rollSound);
    }

    public void PlayCrash()
    {
        _audioSource.loop = false;
        PlayClip(crashSound);
    }
}
