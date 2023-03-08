using Mirror;
using System;
using UnityEngine;

namespace Script.Platforms
{
    public class Waypoint : MonoBehaviour
    {
        [SerializeField] private float radiusGizmo = 0.1f;
        public Vector3 position => transform.position;
        
        public Vector3 AuxiliaryPointStart => position + transform.up / 8;
        public Vector3 AuxiliaryPointEnd => position + (-transform.up) / 8;

        [ServerCallback]
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(AuxiliaryPointStart, radiusGizmo / 2);
            Gizmos.DrawLine(AuxiliaryPointStart, position);
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(AuxiliaryPointEnd, radiusGizmo / 2);
            Gizmos.DrawLine(AuxiliaryPointEnd, position);
            Gizmos.color = Color.black;
            Gizmos.DrawSphere(position, radiusGizmo);
        }
    }
}