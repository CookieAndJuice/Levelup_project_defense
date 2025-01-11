using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMove : MonoBehaviour
{
    public float dragSpeed = 2;
    public float minX = -7;
    public float maxX = 22;
    public float inertiaDuration = 1f;

    private Vector3 dragOrigin;
    private Vector3 currentVelocity;
    private float timeSinceLastInput;

    public bool isDragging = false;

    // https://bloodstrawberry.tistory.com/884
    void Update()
    {
        //if (!EventSystem.current.IsPointerOverGameObject(0)) 
        //{
            if (Input.GetMouseButtonDown(0))
            {
                dragOrigin = Input.mousePosition;
                currentVelocity = Vector3.zero;
                timeSinceLastInput = 0;
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 difference = (dragOrigin - Input.mousePosition) * dragSpeed * Time.deltaTime;
                difference.y = 0;
                Vector3 newPosition = Camera.main.transform.position + difference;
                newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);

                Camera.main.transform.position = newPosition;
                dragOrigin = Input.mousePosition;
                currentVelocity = difference;
                timeSinceLastInput = 0;

            }
            else
            {
                timeSinceLastInput += Time.deltaTime;

                if (timeSinceLastInput <= inertiaDuration)
                {
                    Vector3 newPosition = Camera.main.transform.position + currentVelocity;
                    newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
                    Camera.main.transform.position = newPosition;
                    currentVelocity = Vector3.Lerp(currentVelocity, Vector3.zero, timeSinceLastInput / inertiaDuration);
                }
            }
        //}

        


    }

    

}


