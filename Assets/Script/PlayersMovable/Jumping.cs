using Mirror;
using Script.Interfaces;
using Script.PlayersStatistic;
using UnityEngine;

namespace Script.PlayersMovable
{
    public class Jumping : MonoBehaviour, IJumping
    {
        [SerializeField] private Rigidbody mainBody;
        [SerializeField] private PlayerStatus status;
        [SerializeField] private float power;
        
        [Client]
        public void Jump()
        {
            if (status.OnGround)
            {
                mainBody.AddForce(mainBody.transform.up * power);
            }
        }
    }
}