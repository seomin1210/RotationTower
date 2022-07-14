using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MazeStageMove : MonoBehaviour
{
    private float add = 90f;

    private Vector3 mousePosition = Vector3.zero;
    private Vector3 newMousePosition = Vector3.zero;


    private void Update()
    {
        if(GameManager.Instance.isOpenSetting == false)
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
    }

    protected virtual void MoveStage(Vector3 pos)
    {
        if (pos.y >= 40f) pos.y = 40f;
        else if (pos.y <= -40f) pos.y = -40f;
        if (pos.x >= 40f) pos.x = 40f;
        else if (pos.x <= -40f) pos.x = -40f;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(pos.y, 0f, -pos.x), 1f * Time.deltaTime);
    }

    //void Start()
    //{
    //    Input.gyro.enabled = true;
    //}

    //void Update()
    //{
    //    //float x = Input.gyro.rotationRateUnbiased.x;
    //    //float z = Input.gyro.rotationRateUnbiased.z;
    //    //if (x >= 30f) x = 30f;
    //    //else if (x <= -30f) x = -30f;
    //    //if (z >= 30f) z = 30f;
    //    //else if (z <= -30f) z = -30f;
    //    //transform.Rotate(-x, 0f, z);

    //    Quaternion q = Input.gyro.attitude;
    //    q.y = 0f;
    //    if (q.x >= 30f) q.x = 30f;
    //    else if (q.x <= -30f) q.x = -30f;
    //    if (q.z >= 30f) q.z = 30f;
    //    else if (q.z <= -30f) q.z = -30f;
    //    gameObject.transform.rotation = q;
    //}
}
