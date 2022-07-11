using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MazeStageMove : MonoBehaviour
{
    private float add = 5f;

    private Vector3 mousePosition = Vector3.zero;
    private Vector3 newMousePosition = Vector3.zero;

    private void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        }
        if (Input.GetMouseButton(0))
        {
            newMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z)) * add;
            if ((newMousePosition - mousePosition * add) != Vector3.zero)
            {
                if (newMousePosition.x >= 30f) newMousePosition.x = 30f;
                else if (newMousePosition.x <= -30f) newMousePosition.x = -30f;
                if (newMousePosition.y >= 30f) newMousePosition.y = 30f;
                else if (newMousePosition.y <= -30f) newMousePosition.y = -30f;
                //MoveStage(newMousePosition - mousePosition);
            }
        }
    }

    //protected override void MoveStage(Vector3 pos)
    //{
    //    this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(pos.y, 0f, pos.x), 1f * Time.deltaTime);
    //}


}
