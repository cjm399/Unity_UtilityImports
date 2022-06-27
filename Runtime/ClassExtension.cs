using UnityEngine;
using System.Collections.Generic;

public static class GameObjectExtensions
{
    public static List<GameObject> GetAllChildren(this GameObject Go)
    {
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < Go.transform.childCount; i++)
        {
            list.Add(Go.transform.GetChild(i).gameObject);
        }
        return list;
    }
}