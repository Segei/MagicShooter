using System;
using Script.Server;
using UnityEngine;

namespace Script.Tools
{
    [Serializable]
    public class GameSettings
    {
        [Range(0.01f, 10)] public float HorizontalSensitivity = 1f;
        [Range(0.01f, 10)] public float VerticalSensitivity = 1f;
        public PlayerInstanceFactory PrefabPlayer;
    }
}