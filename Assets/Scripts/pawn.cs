using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static linePoint;

public class pawn : MonoBehaviour
{
    public delegate void DestroyPawn(pawn pawn);
    public event DestroyPawn OnDestroyPawn;

    [SerializeField]
    private Material baseMaterial;
    [SerializeField]
    private Material delMaterial;

    private linePoint[] linePoints;
    private bool IsDeleted;


    private void Start()
    {
        IsDeleted = false;
        linePoints = GetComponentsInChildren<linePoint>();
    }

    public bool CheckPoint(linePoint point) 
    {
        return linePoints.Contains(point);
    }

    public void SetConnectMaterial(Material mmat) 
    {
        foreach (linePoint point in linePoints)
        {
            point.GetComponent<Renderer>().material = mmat;
        }
    
    }

    public void SetDelete() 
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Renderer>().material = delMaterial;
        }
        IsDeleted = true;
    }

    public void RestoreMat()
    {
        if (IsDeleted)
        {
            foreach (Transform child in transform)
            {
                child.GetComponent<Renderer>().material = baseMaterial;
            }
            IsDeleted = false;
        }
    }

    public void CheckDelete() 
    {
        if (IsDeleted) { OnDestroyPawn?.Invoke(this); }
    }

}
