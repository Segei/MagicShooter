using UnityEngine;
using Zenject;

namespace Script.Server
{
    public class PlayerInstanceFactory : MonoBehaviour
    {
        public class Factory : PlaceholderFactory<PlayerInstanceFactory> { }
    }
}