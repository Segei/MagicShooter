using Mirror;
using UnityEngine;

namespace Script.Server
{
    public class CustomNetworkManager : NetworkManager
    {
        [SerializeField] private PlayerSpawner playerSpawner;
        public override void OnServerConnect(NetworkConnectionToClient conn)
        {
            Debug.Log("Connect "+conn);
            NetworkServer.AddPlayerForConnection(conn,  playerSpawner.PlayerSpawn(conn));
            //base.OnServerConnect(conn);
        }

        public override void OnServerAddPlayer(NetworkConnectionToClient conn)
        {
            Debug.Log("AddPlayer "+conn); 
            NetworkServer.AddPlayerForConnection(conn,  playerSpawner.PlayerSpawn(conn));
        }

        public override void OnServerDisconnect(NetworkConnectionToClient conn)
        {
            Debug.Log("Disconnect "+conn);
            playerSpawner.DisconnectPlayer(conn);
        }
    }
}