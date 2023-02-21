using System.Collections.Generic;
using System.Linq;
using Mirror;
using Script.Interfaces;
using Script.PlayersStatistic;
using UnityEngine;

namespace Script.Server
{
    public class PlayerSpawner : MonoBehaviour, IRegisterPrefab
    {
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private List<PointToSpawn> spawnPoints;

        private readonly Dictionary<NetworkConnectionToClient, GameObject> playersList =
            new Dictionary<NetworkConnectionToClient, GameObject>();

        public int NumberSpawnPointOnTheMap => spawnPoints.Count;

        
        public GameObject PlayerSpawn(NetworkConnectionToClient conn)
        {
            if (playerPrefab == null)
            {
                Debug.LogError("The PlayerPrefab is empty on the NetworkManager. Please setup a PlayerPrefab object.");
                return null;
            }

            if (!playerPrefab.TryGetComponent(out NetworkIdentity _))
            {
                Debug.LogError("The PlayerPrefab does not have a NetworkIdentity. Please add a NetworkIdentity to the player prefab.");
                return null;
            }
            
            GameObject instance = Instantiate(playerPrefab);
            MovePlayer(instance);
            foreach (var respawn in instance.GetComponentsInChildren<IRespawn>())
            {
                respawn.Respawn.AddListener(Respawn);
            }
            playersList.Add(conn, instance);
            instance.name = $"{playerPrefab.name} [connId={conn.connectionId}]";
            return instance;
        }

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

        private void Respawn(GameObject instancePlayer)
        {
            UpdateHealth(instancePlayer);
            MovePlayer(instancePlayer);
        }

        public void UpdateHealth(GameObject player)
        {
            player.GetComponent<IHealth>().UpdateHealth();
        }

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

        public void RegisterPrefabToSpawn()
        {
            NetworkClient.RegisterPrefab(playerPrefab);
        }
    }
}