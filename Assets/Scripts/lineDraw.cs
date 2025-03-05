using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineDraw : MonoBehaviour
{
    [SerializeField]
    private LineRenderer line;

    public Transform pos1;
    public Transform pos2;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;
    }

    void Update()
    {
        try
        {
            line.SetPosition(0, pos1.position);
            line.SetPosition(1, pos2.position);
        }
        catch 
        {
            Destroy(line.gameObject);
        }
    }
}
