using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static Vector2 DefaultPos;
    Transform StartParent;
    GameObject cam;


    void Start()
    {
        cam = GameObject.Find("Main Camera");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        cam.GetComponent<CameraMove>().isDragging = true;
        DefaultPos = this.transform.position;
        StartParent = transform.parent;
        transform.SetParent(GameObject.Find("Canvas").transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        cam.GetComponent<CameraMove>().isDragging = true;
        Vector2 currentPos = eventData.position;
        this.transform.position = currentPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.transform.position = DefaultPos;
        this.transform.SetParent(StartParent.transform, true);
        cam.GetComponent<CameraMove>().isDragging = false;
    }

}
