using System;
using UnityEngine;

namespace Script.Platforms
{
    [Serializable]
    public class Segment
    {
        [HideInInspector] public string name;
        public Waypoint Start;
        public Waypoint End;
        public float Time = 1f;
    }
}