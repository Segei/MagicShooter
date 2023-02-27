using System;
using Script.Interfaces;
using Script.PlayersStatistic;
using Script.Tools;
using UnityEngine;
using Zenject;

namespace Script.PlayersMovable
{
    public class Rotation : MonoBehaviour, IRotating
    {
        [SerializeField] private PlayerStatus status;
        [SerializeField] private Rigidbody mainBody;
        [SerializeField] private Rigidbody headBody;
        [Inject] private GameSettings settings;

        private void Start()
        {
            Debug.LogError(settings);
        }

        public void Rotate(Vector2 velocity)
        {
            status.OnRotated = velocity.x != 0;
            
            headBody.angularVelocity = headBody.transform.right * velocity.y * settings.VerticalSensitivity;
            mainBody.angularVelocity = mainBody.transform.up * velocity.x * settings.HorizontalSensitivity;
        }
    }
}
