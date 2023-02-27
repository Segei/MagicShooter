using Mirror;
using NaughtyAttributes;
using UnityEngine;

namespace Script.Platforms
{
    public class ControlMovePlatform : NetworkBehaviour
    {
        [SerializeField] private Router router;
        [SerializeField] private Rigidbody controlledBody;
        [SerializeField, SyncVar] private bool move = true, rotate;
        [SerializeField] private Vector3 direction;
        [SerializeField, SyncVar] private bool forward, backward, forwardMove, block;

        [Server]
        public override void OnStartServer()
        {
            router.Instance();
            syncDirection = SyncDirection.ClientToServer;
            if (router == null)
            {
                Debug.LogError("Controller has null router.", gameObject);
                this.enabled = false;
                return;
            }

            router.OnStartPath.AddListener(() =>
            {
                backward = false;
                block = false;
            });
            
            router.OnEndPath.AddListener(() =>
            {
                forward = false;
                block = false;
            });
            
            forwardMove = router.Direction;
            if (isServer)
            {
                controlledBody.isKinematic = false;
            }

            if (isLocalPlayer)
            {
                this.enabled = false;
            }

            controlledBody.constraints = move ? RigidbodyConstraints.None : RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
            controlledBody.constraints = rotate ? RigidbodyConstraints.None : RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }
        
        [Server]
        private void FixedUpdate()
        {
            PathPoint pathPoint = forward ? router.GetNextPoint() :
                backward ? router.GeеPreliminaryPoint() : router.Wait();
            controlledBody.velocity =
                move ? (pathPoint.Position - controlledBody.transform.position) * 10 : Vector3.zero;
            controlledBody.angularVelocity = rotate
                ? Vector3.Cross(controlledBody.transform.rotation * direction, pathPoint.Rotation) * 10
                : Vector3.zero;
        }

        private void OnValidate()
        {
            if (controlledBody == null)
            {
                Debug.LogError("Platform not add.", gameObject);
                return;
            }
            controlledBody.isKinematic = true;
            controlledBody.useGravity = false;
        }

        [Button, ContextMenu("SwitchMove"), ]
        public void SwitchMove()
        {
            Debug.Log("Call switch.");
            if (block)
            {
                return;
            }
            Debug.Log("Run switch.");
            forward = forwardMove;
            backward = !forwardMove;
            forwardMove = !forwardMove;
            block = true;
        }
    }
}