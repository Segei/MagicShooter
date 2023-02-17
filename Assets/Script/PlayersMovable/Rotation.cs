using System;
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
            Vector3 rotateX = new Vector3()
            {
                x = velocity.y
            };
            Vector3 rotateY = new Vector3()
            {
                y = velocity.x
            };
            
            headBody.angularVelocity = rotateX;
            mainBody.angularVelocity = rotateY;
        }
    }

    public interface IRotating
    {
        void Rotate(Vector2 velocity);
    }
}
