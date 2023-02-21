using UnityEngine;
using UnityEngine.Events;

namespace Script.Interfaces
{
    public interface IRespawn
    {
        UnityEvent<GameObject> Respawn { get; }
    }
}