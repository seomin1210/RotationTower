using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraBhv : MonoBehaviour
{
    public Transform target;

    private void Update()
    {
        this.transform.LookAt(target);
    }
}
