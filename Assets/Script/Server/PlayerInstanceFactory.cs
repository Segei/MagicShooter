using UnityEngine;
using Zenject;

namespace Script.Server
{
    [System.Serializable]
    public class PlayerInstanceFactory : MonoBehaviour
    {
        public class Factory : PlaceholderFactory<PlayerInstanceFactory> { }
    }
}