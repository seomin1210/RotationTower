using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marble : MonoBehaviour
{
    private MarbleAudioPlayer audioPlayer;
    private Rigidbody rigid;

    private bool isRoll = false;

    void Start()
    {
        audioPlayer = GetComponentInChildren<MarbleAudioPlayer>();
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(rigid.velocity.magnitude > 1.5f && isRoll == false)
        {
            isRoll = true;
            audioPlayer.PlayRoll();
        }
        else if(rigid.velocity.magnitude < 1.5f && isRoll == true)
        {
            isRoll = false;
            audioPlayer.PlayCrash();
        }
    }
    
}
