using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Script.Tools
{
    public static class MethodsExtending
    {
        public static List<T> GetInterfaces<T>(this List<GameObject> list)
        {
            return list.Select(e => e.GetComponent<T>()).ToList();
        }
    }
}