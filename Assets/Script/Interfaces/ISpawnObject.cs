using System.Collections.Generic;
using UnityEngine;

namespace Script.Interfaces
{
    public interface ISpawnObject
    {
        List<GameObject> Objects { get; }
        void SpawnObject();
        void DestroyObject();
    }
}