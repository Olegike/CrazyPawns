using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class StartTest : MonoBehaviour
{
    public CrazyPawn.CrazyPawnSettings settingsBase;

    [SerializeField]
    private int MaxXcube;
    [SerializeField]
    private int MaxPanw;
    [SerializeField]
    private int MaxRadius;



    [SerializeField]
    private GameObject defaultCube;
    [SerializeField]
    Material materialPole01;
    [SerializeField]
    Material materialPole02;

    [SerializeField]
    Material activePoint;
    [SerializeField]
    Material defaultPoint;


    [SerializeField]
    private GameObject pawnPrefab;

    [SerializeField]
    private GameObject drawLine;

    private List<pawn> pawnList = new List<pawn>();
    private List<linePoint> posList = new List<linePoint>();


    private void InitData() 
    {
        MaxRadius = (int)settingsBase.InitialZoneRadius;
        MaxXcube = settingsBase.CheckerboardSize;
        MaxPanw = settingsBase.InitialPawnCount;

        materialPole01.color = settingsBase.BlackCellColor;
        materialPole02.color = settingsBase.WhiteCellColor;

    }

    void CreateDesck()
    {
        Vector3 zeropos = transform.position;
        Quaternion zerorot = transform.rotation;
        int number = 0;

        for (int i = 0; i < MaxXcube; i++)
        {
            zeropos.z = 0;
            for (int j = 0; j < MaxXcube; j++)
            {
                zeropos.z += defaultCube.transform.localScale.z;
                GameObject cub = Instantiate(defaultCube, zeropos, zerorot);

                if (number % 2 == 0) { cub.GetComponent<cubeinfo>().setMaterial(materialPole01); } 
                else { cub.GetComponent<cubeinfo>().setMaterial(materialPole02); }

                cub.transform.parent = this.transform;
                number++;
            }
            zeropos.x += defaultCube.transform.localScale.x;
        }
    }

    void CreatePawn()
    {
        Vector3 zeropos = transform.position;
        Quaternion zerorot = transform.rotation;

        int num = Random.Range(3, MaxPanw);

        //for test
        //num = 4;

        for (int i = 0; i < num; i++)
        {
            int zpos = Random.Range(1, MaxRadius);
            int xpos = Random.Range(1, MaxRadius);
            int ypos = Random.Range(0, 360);

            zeropos.z = zpos;
            zeropos.x = xpos;
            zeropos.y = 0.5f;

            zerorot.y = ypos;

            GameObject pawn = Instantiate(pawnPrefab, zeropos, zerorot);
            pawn mpawn = pawn.GetComponent<pawn>();
            mpawn.OnDestroyPawn += PawnDestroy;
            pawnList.Add(mpawn);

            linePoint[] lp = pawn.GetComponentsInChildren<linePoint>();
            foreach (linePoint lp2 in lp)
            {
                lp2.OnLineCreat += LineCreation;
            }

        }

    }

    private void PawnDestroy(pawn pawn)
    {
        pawnList.Remove(pawn);
        Destroy(pawn.gameObject);
    }

    private void Start()
    {
        InitData();
        CreateDesck();
        CreatePawn();
    }

    private void DefaultConnectMaterial() 
    {
        foreach (pawn pawnP in pawnList)
        {
            pawnP.SetConnectMaterial(defaultPoint);
        }
    }

    private void ActiveConnectMaterial(linePoint point)
    {
        foreach (pawn pawnP in pawnList)
        {
            if (!pawnP.CheckPoint(point))
            {
                pawnP.SetConnectMaterial(activePoint);
            }
        }
    }



    private void LineWrong() 
    {
        posList.Clear();
        DefaultConnectMaterial();
    }

    private void LineCreation(linePoint point, pawn pawnParent)
    {
        print("LineCreation");

        if (posList.Count == 0) 
        {
            print("Add First");
            posList.Add(point);
            ActiveConnectMaterial(point);
            return;
        }

        if (posList.Count == 1)
        {
            if (pawnParent.CheckPoint(posList[0])) 
            {
                LineWrong();
                return;
            }

            print("Add Second and Complited");
            posList.Add(point);
            GameObject line = Instantiate(drawLine, posList[0].transform.position, Quaternion.identity);
            lineDraw ld = line.GetComponent<lineDraw>();

            ld.pos1 = posList[0].transform;
            ld.pos2 = posList[1].transform;


            DefaultConnectMaterial();
            posList.Clear();
            
        }
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                linePoint lp = hit.collider.GetComponent<linePoint>();
                if (!lp)
                {
                    LineWrong();
                }
            }
            else
            {
                LineWrong();
            }
        }
    }



}
