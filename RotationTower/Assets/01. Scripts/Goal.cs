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
            GameManager.Instance.NextStage();
            Destroy(transform.parent.parent.gameObject);
        }
        if (other.tag == "SavePoint" && isBeSavePoint == true)
        {
            isBeSavePoint = false;
        }
        if(other.tag == "Deadzone")
        {
            Destroy(transform.parent.gameObject);
            GameManager.Instance.Die();
        }
        if(other.tag == "Cube")
        {
            Destroy(other.gameObject);
        }
    }
}
