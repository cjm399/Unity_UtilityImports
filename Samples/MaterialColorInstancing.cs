using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialColorInstancing : MonoBehaviour
{
    public Color col = Color.red;
    public bool pickRandomColor = false;
    void OnValidate()
    {
        MaterialPropertyBlock mpb = new MaterialPropertyBlock();
        GetComponent<MeshRenderer>().GetPropertyBlock(mpb);
        Color colorToSet = col;
        if (pickRandomColor)
        {
            colorToSet = new Color32((byte)Random.Range(0, 256), (byte)Random.Range(0, 256), (byte)Random.Range(0, 256), 1);
        }
        mpb.SetColor("_Color", colorToSet);
        GetComponent<MeshRenderer>().SetPropertyBlock(mpb);
    }
}
