using Mirror.Discovery;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Script.Server.NetworkHUD
{
    public class ViewConnectToServer : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private Button button;
        private ServerResponse response;

        public UnityEvent<ServerResponse> OnConnectTo;
        public ServerResponse Response => response;


        private void Start()
        {
            button.onClick.AddListener(Connect);
        }

        public void Instance(ServerResponse serverResponse)
        {
            response = serverResponse;
            text.text = serverResponse.EndPoint.Address.ToString();
        }

        private void Connect()
        {
            OnConnectTo?.Invoke(response);
        }

    }
}
