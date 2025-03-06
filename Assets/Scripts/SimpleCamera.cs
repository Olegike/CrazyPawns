using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCamera : MonoBehaviour
{
    public StartTest StartPointStartTest;

    public float moveSpeed = 250.0f;
    public float zoomSpeed = 2.0f;

    public bool IsMovePawn;
    public bool IsConnectPawn;

    private void Start()
    {
        IsMovePawn = false;
        IsConnectPawn = false;
    }


    private void MouseCamMove()
    {
        if (Input.GetMouseButton(0))
        {
            float mousex = -Input.GetAxis("Mouse X");
            float mousey = -Input.GetAxis("Mouse Y");

            Vector3 forward = transform.forward * Time.deltaTime;
            Vector3 right = transform.right * Time.deltaTime;

            forward.y = 0f;

            Vector3 move = (forward * mousey + right * mousex) * moveSpeed;
            Vector3 newPos = transform.position + move;

            newPos.y = transform.position.y;
            transform.position = newPos;

        }
    }

    private void MouseWheelZoom()
    {
        float wheel = Input.GetAxis("Mouse ScrollWheel");

        if (wheel != 0)
        {
            Vector3 pos = transform.position;
            pos.y -= wheel * zoomSpeed;
            transform.position = pos;
        }
    
    
    }


    private void Update()
    {

        if (!StartPointStartTest) return;
        if (IsMovePawn) return;
        if (IsConnectPawn) return;

        MouseCamMove();
        MouseWheelZoom();

    }

}
