using System.Collections.Generic;
using System.Linq;
using Mirror;
using Script.PlayersStatistic;
using UnityEngine;

namespace Script.Server
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject playerPrefs;
        [SerializeField] private List<PointToSpawn> spawnPoints;

        private readonly Dictionary<NetworkConnectionToClient, GameObject> playersList =
            new Dictionary<NetworkConnectionToClient, GameObject>();

        public int NumberSpawnPointOnTheMap => spawnPoints.Count;


        public GameObject PlayerSpawn(NetworkConnectionToClient conn)
        {
            GameObject instance = Instantiate(playerPrefs);
            MovePlayer(instance);
            foreach (var respawn in instance.GetComponentsInChildren<IRespawn>())
            {
                respawn.Respawn.AddListener(Respawn);
            }
            playersList.Add(conn, instance);
            NetworkClient.Ready();
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
    }
}