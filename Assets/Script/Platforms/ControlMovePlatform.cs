using Mirror;
using NaughtyAttributes;
using UnityEngine;

namespace Script.Platforms
{
    public class ControlMovePlatform : NetworkBehaviour
    {
        [SerializeField] private Router router;
        [SerializeField] private Rigidbody controlledBody;
        [SerializeField] private bool move, rotate;
        [SerializeField] private Vector3 direction;
        private bool forward, backward, forwardMove, block;

        [Server]
        private void OnServerInitialized()
        {
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
        }

        [Server]
        private void FixedUpdate()
        {
            PathPoint pathPoint = forward ? router.GetNextPoint() :
                backward ? router.GeеPreliminaryPoint() : router.Wait();
            controlledBody.velocity =
                move ? (pathPoint.Position - controlledBody.transform.position) * 10 : Vector3.zero;
            controlledBody.angularVelocity = rotate
                ? Vector3.Cross(controlledBody.transform.rotation * direction, pathPoint.Rotation)
                : Vector3.zero;
        }

        private void OnValidate()
        {
            controlledBody.isKinematic = true;
        }

        [Button, ContextMenu("SwitchMove"), Command]
        public void SwitchMove()
        {
            if (block)
            {
                return;
            }

            forward = forwardMove;
            backward = !forwardMove;
            forwardMove = !forwardMove;
            block = true;
        }
    }
}