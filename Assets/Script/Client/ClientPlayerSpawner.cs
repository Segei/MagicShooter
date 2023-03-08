using Mirror;
using Script.Server;
using Script.Tools;
using UnityEngine;
using Zenject;

namespace Script.Client
{
    public class ClientPlayerSpawner : MonoBehaviour
    {
        [Inject] private readonly GameSettings settings;
        [Inject] private readonly PlayerInstanceFactory.Factory playerLinks;


        private void Start()
        {
            Debug.Log(settings);
            NetworkClient.RegisterPrefab(settings.PrefabPlayer.gameObject, GetPlayer, DestroyPlayer);
        }

        private GameObject GetPlayer(SpawnMessage msg)
        {
            PlayerInstanceFactory instance = playerLinks.Create();
            instance.transform.position = msg.position;
            instance.transform.rotation = msg.rotation;
            foreach (SkinnedMeshRenderer mesh in instance.GetComponentsInChildren<SkinnedMeshRenderer>(true))
            {
                mesh.gameObject.layer = 6;
            }

            instance.name = $"{instance.name} [connId={msg.netId}]";
            return instance.gameObject;
        }

        private void DestroyPlayer(GameObject spawned)
        {
            Destroy(spawned);
        }
    }
}