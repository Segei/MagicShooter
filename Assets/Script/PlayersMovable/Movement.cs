using Mirror;
using Script.Interfaces;
using Script.PlayersStatistic;
using UnityEngine;

namespace Script.PlayersMovable
{
    public class Movement : MonoBehaviour, IMovable
    {
        [SerializeField] private float forwardSpeed;
        [SerializeField] private float backwardSpeed;
        [SerializeField] private float lateralSpeed;
        [SerializeField] private Rigidbody mainBody;
        [SerializeField] private PlayerStatus status;
        private float forwardVelocity, rightVelosity;


        [Client]
        public void Move(Vector2 velocity)
        {
            status.OnMoved = velocity.magnitude != 0;
            forwardVelocity = velocity.y > 0 ? velocity.y * forwardSpeed  : velocity.y * backwardSpeed;
            rightVelosity = velocity.x * lateralSpeed;
        }

        [Client]
        private void Update()
        {
            var body = mainBody.transform;
            mainBody.velocity = (body.forward * forwardVelocity)
                                + (body.right * rightVelosity)
                                + (body.up * mainBody.velocity.y);
        }

    }
}