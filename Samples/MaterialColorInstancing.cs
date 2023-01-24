using SpacePigs.Math;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Samples
{
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
                colorToSet = new Color(Rand.GetRandomPercentage(), Rand.GetRandomPercentage(), Rand.GetRandomPercentage(), 1);
            }
            mpb.SetColor("_Color", colorToSet);
            GetComponent<MeshRenderer>().SetPropertyBlock(mpb);
        }
    }
}