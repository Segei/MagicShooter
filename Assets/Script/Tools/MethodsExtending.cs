using Mirror;
using Script.PlayerAction;
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

        public static List<T> CopyList<T>(this List<T> list)
        {
            List<T> result = new List<T>();
            foreach (T element in list)
            {
                result.Add(element);
            }
            return result;
        }
    }
}