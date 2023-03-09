using Mirror;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Server.NetworkHUD
{
    public class LinkHUD : MonoBehaviour
    {
        [SerializeField] private CustomNetworkDiscoveryHUD networkDiscovery;
        [SerializeField] private Button buttonServer, buttonHost, buttonFindServer, buttonExit;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (buttonServer != null)
            {
                if (buttonServer.onClick.GetPersistentEventCount() == 0)
                {
                    UnityEditor.Events.UnityEventTools.AddPersistentListener(buttonServer.onClick, Server);
                }
                UnityEditor.Events.UnityEventTools.RegisterPersistentListener(buttonServer.onClick, 0, Server);
            }

            if (buttonHost != null)
            {
                if (buttonHost.onClick.GetPersistentEventCount() == 0)
                {
                    UnityEditor.Events.UnityEventTools.AddPersistentListener(buttonHost.onClick, Host);
                }
                UnityEditor.Events.UnityEventTools.RegisterPersistentListener(buttonHost.onClick, 0, Host);
            }

            if (buttonFindServer != null)
            {
                if (buttonFindServer.onClick.GetPersistentEventCount() == 0)
                {
                    UnityEditor.Events.UnityEventTools.AddPersistentListener(buttonFindServer.onClick, FindServer);
                }
                UnityEditor.Events.UnityEventTools.RegisterPersistentListener(buttonFindServer.onClick, 0, FindServer);
            }

            if (networkDiscovery != null)
            {
                if (networkDiscovery.OnExit.GetPersistentEventCount() == 0)
                {
                    UnityEditor.Events.UnityEventTools.AddPersistentListener(networkDiscovery.OnExit, CloseHUD);
                }
                UnityEditor.Events.UnityEventTools.RegisterPersistentListener(networkDiscovery.OnExit, 0, CloseHUD);
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

        private void Server()
        { 
            buttonFindServer.gameObject.SetActive(false);
            buttonHost.gameObject.SetActive(false);
            buttonServer.gameObject.SetActive(false);
            NetworkManager.singleton.StartServer();
            networkDiscovery.NetworkDiscovery.AdvertiseServer();           
        }

        private void Host()
        {
            NetworkManager.singleton.StartHost();
            networkDiscovery.NetworkDiscovery.AdvertiseServer();
            Cursor.lockState = CursorLockMode.Locked;
            gameObject.SetActive(false);
        }

        private void FindServer()
        {
            networkDiscovery.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }

        private void CloseHUD()
        {
            networkDiscovery.gameObject.SetActive(false);
            gameObject.SetActive(true);
        }

        private void Exit()
        {
            Application.Quit();
        }
    }
}
