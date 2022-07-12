using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public bool isBeSavePoint = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Destination" && isBeSavePoint == false)
        {
            GameManager.Instance.ClearStage();
            Destroy(transform.parent.parent.gameObject);
        }
        if (other.tag == "SavePoint" && isBeSavePoint == true)
        {
            isBeSavePoint = false;
        }
        if(other.tag == "Deadzone")
        {
            GameManager.Instance.RestartStage();
        }
    }
}
