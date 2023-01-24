using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpacePigs.Utilities;
using SpacePigs.Extensions;

namespace Utilities.Samples
{
    public class PrintMyChildren : MonoBehaviour
    {
        void Start()
        {
            foreach (GameObject go in gameObject.GetAllChildren())
            {
                Log.Verbose(go.ToString());
            }
        }
    }
}