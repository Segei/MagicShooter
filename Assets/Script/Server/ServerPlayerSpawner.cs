using Mirror;
using Script.Interfaces;
using Script.Tools;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Script.Server
{
    public class ServerPlayerSpawner : MonoBehaviour, IRegisterPrefab
    {
        [Inject] private readonly GameSettings gameSettings;
        [Inject] private readonly PlayerInstanceFactory.Factory playerLinks;
        [SerializeField] private List<PointToSpawn> spawnPoints;

        private readonly Dictionary<NetworkConnectionToClient, GameObject> playersList =
            new Dictionary<NetworkConnectionToClient, GameObject>();

        public int NumberSpawnPointOnTheMap => spawnPoints.Count;


        [Server]
        public GameObject PlayerSpawn(NetworkConnectionToClient conn)
        {
            GameObject instance = playerLinks.Create().gameObject;
            MovePlayer(instance);
            foreach (IRespawn respawn in instance.GetComponentsInChildren<IRespawn>())
            {
                respawn.Respawn.AddListener(Respawn);
            }

            if (conn.connectionId == 0) 
            {
                foreach (SkinnedMeshRenderer mesh in instance.GetComponentsInChildren<SkinnedMeshRenderer>(true))
                {
                    mesh.gameObject.layer = 6;
                }
            }

            playersList.Add(conn, instance);
            instance.name = $"{instance.name} [connId={conn.connectionId}]";
            return instance;
        }


        [Server]
        public void DisconnectPlayer(NetworkConnectionToClient conn)
        {
            GameObject player = playersList[conn];
            foreach (IRespawn respawn in player.GetComponentsInChildren<IRespawn>())
            {
                respawn.Respawn.RemoveListener(Respawn);
            }

            Destroy(player);
            _ = playersList.Remove(conn);
        }



        [Server]
        private void Respawn(GameObject instancePlayer)
        {
            UpdateHealth(instancePlayer);
            MovePlayer(instancePlayer);
        }

        [Server]
        private void UpdateHealth(GameObject player)
        {
            player.GetComponent<IHealth>().UpdateHealth();
        }

        [Server]
        private void MovePlayer(GameObject instancePlayer)
        {
            List<PointToSpawn> freePoints = spawnPoints.Where(e => e.Free).ToList();
            if (freePoints.Count > 0)
            {
                freePoints[Random.Range(0, freePoints.Count)].MoveToPoint(instancePlayer.transform);
            }
            else
            {
                GetSpawnWithMaxRangeToOtherPlayer().MoveToPoint(instancePlayer.transform);
            }
        }

        [Server]
        private PointToSpawn GetSpawnWithMaxRangeToOtherPlayer()
        {
            PointToSpawn result = null;
            float maxRange = 0;
            foreach (PointToSpawn spawnPoint in spawnPoints)
            {
                float distance = spawnPoint.GetMinimalDistance();
                if (distance > maxRange)
                {
                    maxRange = distance;
                    result = spawnPoint;
                }
            }

            return result;
        }

        [Server]
        public void RegisterPrefabToSpawn()
        {
            NetworkClient.RegisterPrefab(gameSettings.PrefabPlayer.gameObject);
        }
    }
}