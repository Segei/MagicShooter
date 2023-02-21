﻿using System.Collections.Generic;
using Mirror;
using Script.Tools;
using UnityEngine;

namespace Script.Server
{
    public class CustomNetworkManager : NetworkManager
    {
        [SerializeField] private PlayerSpawner playerSpawner;
        [SerializeField] private List<GameObject> iRegisterPrefab = new List<GameObject>();
        private List<IRegisterPrefab> registerPrefabs = new List<IRegisterPrefab>();



        public override void Awake()
        {
            base.Awake();
            registerPrefabs = iRegisterPrefab.GetInterfaces<IRegisterPrefab>();
        }
        public override void OnServerConnect(NetworkConnectionToClient conn)
        {
            Debug.Log("Connect " + conn);
            base.OnServerConnect(conn);
        }

        public override void OnClientConnect()
        {
            if (!clientLoadedScene)
            {
                if (!NetworkClient.ready)
                    NetworkClient.Ready();
                NetworkClient.AddPlayer();
            }
        }

        public override void OnStartServer()
        {
            base.OnStartServer();
        }

        public override void OnServerAddPlayer(NetworkConnectionToClient conn)
        {
            Debug.Log("AddPlayer " + conn);
            GameObject player = playerSpawner.PlayerSpawn(conn);
            Debug.Log("Player Spawn.");
            NetworkServer.AddPlayerForConnection(conn, player);
        }

        public override void OnServerDisconnect(NetworkConnectionToClient conn)
        {
            Debug.Log("Disconnect " + conn);
            playerSpawner.DisconnectPlayer(conn);
        }

        protected override void OnServerAddPlayerInternal(NetworkConnectionToClient conn, AddPlayerMessage msg)
        {
            if (conn.identity != null)
            {
                Debug.LogError("There is already a player for this connection.");
                return;
            }

            OnServerAddPlayer(conn);
        }

        protected override void RegisterClientMessages()
        {
            base.RegisterClientMessages();
            foreach (var registerPrefab in registerPrefabs)
            {
                registerPrefab.RegisterPrefabToSpawn();
            }
        }
    }
}