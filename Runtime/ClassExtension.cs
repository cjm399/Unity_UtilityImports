using UnityEngine;
using System.Collections.Generic;
using System;

public static class Utilities_GameObjectExtensions
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

public static class Utilities_DateTimeExtensions
{
    public static int GetSeason(this DateTime date, bool ofSouthernHemisphere = false)
    {
        int hemisphereConst = (ofSouthernHemisphere ? 2 : 0);
        Func<int, int> getReturn = (northern) => {
            return (northern + hemisphereConst) % 4;
        };
        float value = (float)date.Month + date.Day / 100f;  // <month>.<day(2 digit)>
        if (value < 3.21 || value >= 12.22) return getReturn(3);    // 3: Winter
        if (value < 6.21) return getReturn(0);  // 0: Spring
        if (value < 9.23) return getReturn(1);  // 1: Summer
        return getReturn(2);     // 2: Autumn
    }
}