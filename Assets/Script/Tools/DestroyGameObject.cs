using Mirror;
using UnityEngine;

namespace Assets.Script.Tools
{
    public class DestroyGameObject : MonoBehaviour
    {
        [Server]
        public void DestroyThisGameObject()
        {
            NetworkServer.Destroy(gameObject);
        } 
    }
}
