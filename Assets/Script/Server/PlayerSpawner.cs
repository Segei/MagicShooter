using System.Collections.Generic;
using System.Linq;
using Mirror;
using Script.Interfaces;
using Script.Tools;
using UnityEngine;
using Zenject;

namespace Script.Server
{
    public class PlayerSpawner : MonoBehaviour, IRegisterPrefab
    {
        [Inject] private GameSettings gameSettings;
        [Inject] private PlayerInstanceFactory.Factory playerLinks;
        [SerializeField] private List<PointToSpawn> spawnPoints;

        private readonly Dictionary<NetworkConnectionToClient, GameObject> playersList =
            new Dictionary<NetworkConnectionToClient, GameObject>();

        public int NumberSpawnPointOnTheMap => spawnPoints.Count;

        
        [Server]
        public GameObject PlayerSpawn(NetworkConnectionToClient conn)
        {
            PlayerInstanceFactory instance = playerLinks.Create();
            MovePlayer(instance.gameObject);
            foreach (var respawn in instance.GetComponentsInChildren<IRespawn>())
            {
                respawn.Respawn.AddListener(Respawn);
            }
            playersList.Add(conn, instance.gameObject);
            instance.name = $"{instance.name} [connId={conn.connectionId}]";
            return instance.gameObject;
        }

        
        [Server]
        public void DisconnectPlayer(NetworkConnectionToClient conn)
        {
            GameObject player = playersList[conn];
            foreach (var respawn in player.GetComponentsInChildren<IRespawn>())
            {
                respawn.Respawn.RemoveListener(Respawn);
            }

            Destroy(player);
            playersList.Remove(conn);
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
            foreach (var spawnPoint in spawnPoints)
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