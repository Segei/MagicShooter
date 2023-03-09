using Mirror;
using Mirror.Discovery;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Script.Server.NetworkHUD
{
    public class CustomNetworkDiscoveryHUD : MonoBehaviour
    {
        [SerializeField] private Canvas canvasDiscoveryHUD;
        [SerializeField] private RectTransform conteiner;
        [SerializeField] private NetworkDiscovery networkDiscovery;
        [SerializeField] private Button buttonFind;
        [SerializeField] private Button buttonStopFind;
        [SerializeField] private Button buttonExit;
        [SerializeField] private ViewConnectToServer prefabConnectToServer;

        private List<ViewConnectToServer> viewConnectToServers = new List<ViewConnectToServer>();
        public NetworkDiscovery NetworkDiscovery => networkDiscovery;
        public UnityEvent OnExit;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (networkDiscovery != null)
            {
                if (networkDiscovery.OnServerFound.GetPersistentEventCount() == 0)
                {
                    UnityEditor.Events.UnityEventTools.AddPersistentListener(networkDiscovery.OnServerFound, OnDiscoveredServer);
                }
                UnityEditor.Events.UnityEventTools.RegisterPersistentListener(networkDiscovery.OnServerFound, 0, OnDiscoveredServer);
            }

            if (buttonFind != null)
            {
                if (buttonFind.onClick.GetPersistentEventCount() == 0)
                {
                    UnityEditor.Events.UnityEventTools.AddPersistentListener(buttonFind.onClick, StartDiscovery);
                }
                UnityEditor.Events.UnityEventTools.RegisterPersistentListener(buttonFind.onClick, 0, StartDiscovery);
            }

            if (buttonStopFind != null)
            {
                if (buttonStopFind.onClick.GetPersistentEventCount() == 0)
                {
                    UnityEditor.Events.UnityEventTools.AddPersistentListener(buttonStopFind.onClick, StopDiscovery);
                }
                UnityEditor.Events.UnityEventTools.RegisterPersistentListener(buttonStopFind.onClick, 0, StopDiscovery);

                buttonStopFind.interactable = false;
            }

            if (buttonExit != null)
            {
                if (buttonExit.onClick.GetPersistentEventCount() == 0)
                {
                    UnityEditor.Events.UnityEventTools.AddPersistentListener(buttonExit.onClick, Exit);
                }
                UnityEditor.Events.UnityEventTools.RegisterPersistentListener(buttonExit.onClick, 0, Exit);
            }
        }
#endif
        private void StartDiscovery()
        {
            networkDiscovery.StartDiscovery();
            buttonStopFind.interactable = true;
        }

        private void StopDiscovery()
        {
            networkDiscovery.StopDiscovery();
            buttonStopFind.interactable = false;
        }

        private void OnDiscoveredServer(ServerResponse serverResponse)
        {
            if (viewConnectToServers.Count(e=> e.Response.Equals(serverResponse)) > 0)
            {
                return;
            }
            ViewConnectToServer viewConnect = Instantiate(prefabConnectToServer, conteiner);
            viewConnect.Instance(serverResponse);
            viewConnect.OnConnectTo.AddListener(ConnectTo);
            viewConnectToServers.Add(viewConnect);
        }

        private void Clear()
        {
            foreach (ViewConnectToServer viewConnect in viewConnectToServers)
            {
                Destroy(viewConnect);
            }
            viewConnectToServers.Clear();
        }

        private void ConnectTo(ServerResponse serverResponse)
        {
            Clear();
            canvasDiscoveryHUD.gameObject.SetActive(false);
            NetworkManager.singleton.StartClient(serverResponse.uri);
            gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Exit()
        {
            OnExit?.Invoke();
        }
    }
}
