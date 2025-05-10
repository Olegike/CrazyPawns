using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeinfo : MonoBehaviour
{

    public void setMaterial(Material new_mat) 
    {
        Renderer cube_rend = GetComponent<Renderer>();
        if (cube_rend != null)
            cube_rend.material = new_mat;
        else 
        {
            print("NOT MATERIAL!!");
        }
    }


}
