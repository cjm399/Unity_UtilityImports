using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.Assertions;

namespace SpacePigs.Extensions
{
    public static class Utilities_GameObjectExtensions
    {
        public static List<GameObject> GetAllChildren(this GameObject _obj)
        {
            List<GameObject> list = new List<GameObject>();
            for (int i = 0; i < _obj.transform.childCount; i++)
            {
                list.Add(_obj.transform.GetChild(i).gameObject);
            }
            return list;
        }

        public static T[] GetInterfaces<T>(this GameObject _obj)
        {
#if UNITY_EDITOR
            Assert.IsTrue(typeof(T).IsInterface, $"Type {typeof(T).ToString()} is not an interface!");
#endif
            var monos = _obj.GetComponents<MonoBehaviour>();
            return (from comp in monos where comp.GetType().GetInterfaces().Any(t => t == typeof(T)) select (T)(object)comp).ToArray();
        }

        public static T GetInterface<T>(this GameObject _obj)
        {
#if UNITY_EDITOR
            Assert.IsTrue(typeof(T).IsInterface, $"Type {typeof(T).ToString()} is not an interface!");
#endif
            return _obj.GetInterfaces<T>().FirstOrDefault();
        }

        public static T GetInterfaceInChildren<T>(this GameObject _obj)
        {
#if UNITY_EDITOR
            Assert.IsTrue(typeof(T).IsInterface, $"Type {typeof(T).ToString()} is not an interface!");
#endif
            return _obj.GetInterfacesInChildren<T>().FirstOrDefault();
        }

        public static T[] GetInterfacesInChildren<T>(this GameObject _obj)
        {
#if UNITY_EDITOR
            Assert.IsTrue(typeof(T).IsInterface, $"Type {typeof(T).ToString()} is not an interface!");
#endif

            var monos = _obj.GetComponentsInChildren<MonoBehaviour>();

            return (from comp in monos where comp.GetType().GetInterfaces().Any(t => t == typeof(T)) select (T)(object)comp).ToArray();
        }
    }

    public static class Utilities_DateTimeExtensions
    {
        public static int GetSeason(this DateTime _date, bool _ofSouthernHemisphere = false)
        {
            int hemisphereConst = (_ofSouthernHemisphere ? 2 : 0);
            Func<int, int> getReturn = (northern) =>
            {
                return (northern + hemisphereConst) % 4;
            };
            float value = (float)_date.Month + _date.Day / 100f;  // <month>.<day(2 digit)>
            if (value < 3.21 || value >= 12.22) return getReturn(3);    // 3: Winter
            if (value < 6.21) return getReturn(0);  // 0: Spring
            if (value < 9.23) return getReturn(1);  // 1: Summer
            return getReturn(2);     // 2: Autumn
        }
    }
}