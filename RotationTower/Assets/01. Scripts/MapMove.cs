using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapMove : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public void OnBeginDrag(PointerEventData eventData) //Drag ����
    {
        Debug.Log("Drag Start");
    }

    public void OnDrag(PointerEventData eventData) //Drag ��
    {
        Debug.Log("Draging");

    }

    public void OnEndDrag(PointerEventData eventData) //Drag ��
    {
        Debug.Log("Drag End");

    }
}
