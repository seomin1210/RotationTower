using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StageMove : MonoBehaviour
{
    Vector3 mousePosition = Vector3.zero;
    Vector3 newMousePosition = Vector3.zero;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        }
        if (Input.GetMouseButton(0))
        {
            newMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
            if ((newMousePosition - mousePosition) != Vector3.zero)
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(newMousePosition.y - mousePosition.y, 0f, mousePosition.x - newMousePosition.x), 1f * Time.deltaTime);
        }
    }
}
