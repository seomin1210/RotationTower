using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackTower : MonoBehaviour
{
    private float rotSpeed = 20f;
    void Update()
    {
        transform.Rotate(0, -1 * Time.deltaTime * rotSpeed, 0, Space.Self);
    }
}
