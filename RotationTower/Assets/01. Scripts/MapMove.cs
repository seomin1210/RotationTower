using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapMove : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public void OnBeginDrag(PointerEventData eventData) //Drag 시작
    {
        Debug.Log("Drag Start");
    }

    public void OnDrag(PointerEventData eventData) //Drag 중
    {
        Debug.Log("Draging");

    }

    public void OnEndDrag(PointerEventData eventData) //Drag 끝
    {
        Debug.Log("Drag End");

    }
}
