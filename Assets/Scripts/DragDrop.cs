using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    private Camera mainCam;
    private float CamDistanceZ;
    private float posy;

    private Transform parent;
    private pawn pawnParent;

    private SimpleCamera camControl;

    void Start()
    {
        parent = transform.parent;
        posy = parent.position.y;
        pawnParent = parent.GetComponent<pawn>();

        mainCam = Camera.main;
        camControl = mainCam.GetComponent<SimpleCamera>();  

        CamDistanceZ = mainCam.WorldToScreenPoint(transform.position).z;

    }

    private void OnMouseDrag()
    {
        camControl.IsMovePawn = true;

        Vector3 screenPos = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            CamDistanceZ
            );

        Vector3 newWorldPos = mainCam.ScreenToWorldPoint(screenPos);
        parent.position = newWorldPos;

    }

    private void OnMouseUp()
    {
        camControl.IsMovePawn = false;
        pawnParent.CheckDelete(); 
    }

    private void Update()
    {
        Vector3 pos = parent.position;
        pos.y = posy;
        parent.position = pos;
        
        Renderer rend = this.GetComponent<Renderer>();

        RaycastHit hit;
        Ray downRay = new Ray(transform.position, -Vector3.up);

        if (Physics.Raycast(downRay, out hit))
        {
            pawnParent.RestoreMat();
        }
        else 
        {
            pawnParent.SetDelete();
        }

    }
}
