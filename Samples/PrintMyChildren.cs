using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintMyChildren : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject go in gameObject.GetAllChildren())
        {
            Utilities.Log.Verbose(go.ToString());
        }
    }
}
