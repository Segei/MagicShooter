using Mirror;
using Script.Interfaces;
using Script.PlayersStatistic;
using UnityEngine;

namespace Script.PlayersMovable
{
    public class Rotation : MonoBehaviour, IRotating
    {
        [SerializeField] private PlayerStatus status;
        [SerializeField] private Rigidbody mainBody;
        [SerializeField] private Rigidbody headBody;


        [Client]
        public void Rotate(Vector2 velocity)
        {
            status.OnRotated = velocity.x != 0;

            headBody.angularVelocity = headBody.transform.right * velocity.y;
            mainBody.angularVelocity = mainBody.transform.up * velocity.x;
        }
    }
}
