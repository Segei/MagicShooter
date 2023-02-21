using System;
using Script.Interfaces;
using Script.PlayersStatistic;
using UnityEngine;
using Zenject;

namespace Script.PlayersMovable
{
    public class Rotation : MonoBehaviour, IRotating
    {
        [SerializeField] private PlayerStatus status;
        [SerializeField] private Rigidbody mainBody;
        [SerializeField] private Rigidbody headBody;
        
        public void Rotate(Vector2 velocity)
        {
            status.OnRotated = velocity.x != 0;
            
            headBody.angularVelocity = headBody.transform.right * velocity.y * 10;
            mainBody.angularVelocity = mainBody.transform.up * velocity.x * 10;
        }
    }
}
