using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MazeStageMove : MonoBehaviour
{
    private float add = 60f;

    private Vector3 mousePosition = Vector3.zero;
    private Vector3 newMousePosition = Vector3.zero;


    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            mousePosition = Camera.main.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z) * add);
        }
        if (Input.GetMouseButton(0))
        {
            newMousePosition = Camera.main.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z) * add);
            if ((newMousePosition - mousePosition * add) != Vector3.zero)
            {
                MoveStage(newMousePosition - mousePosition);
            }
        }
    }

    protected virtual void MoveStage(Vector3 pos)
    {
        if (pos.y >= 30f) pos.y = 30f;
        else if (pos.y <= -30f) pos.y = -30f;
        if (pos.x >= 30f) pos.x = 30f;
        else if (pos.x <= -30f) pos.x = -30f;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(pos.y, 0f, -pos.x), 1f * Time.deltaTime);
    }

    //void Start()
    //{
    //    Input.gyro.enabled = true;
    //}

    //void Update()
    //{
    //    transform.Rotate(Input.gyro.rotationRateUnbiased.x, Input.gyro.rotationRateUnbiased.y, Input.gyro.rotationRateUnbiased.z);
    //}
}
