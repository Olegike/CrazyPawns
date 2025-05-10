using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class linePoint : MonoBehaviour
{
    public delegate void LineCreator(linePoint point, pawn paw);
    public event LineCreator OnLineCreat;

    private pawn pawnParent;
    private SimpleCamera camControl;


    void Start()
    {
        pawnParent = GetComponentInParent<pawn>();
        camControl = Camera.main.GetComponent<SimpleCamera>();
    }


    private void OnMouseDown()
    {
        OnLineCreat?.Invoke(this, pawnParent);
    }

    private void OnMouseUp()
    {
        camControl.IsConnectPawn = false;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            linePoint lp = hit.collider.GetComponent<linePoint>();
            if (lp && (lp != this))
            {
                pawnParent = lp.GetComponentInParent<pawn>();
                OnLineCreat?.Invoke(lp, pawnParent);
                return;
            }
        }

    }

    private void OnMouseDrag()
    {
        camControl.IsConnectPawn = true;
    }
}
