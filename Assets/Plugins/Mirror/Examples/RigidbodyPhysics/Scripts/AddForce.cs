using UnityEngine;

namespace Mirror.Examples.RigidbodyPhysics
{
    [RequireComponent(typeof(Rigidbody))]
    public class AddForce : MonoBehaviour
    {
        public Rigidbody rigidbody3d;
        public float force = 500f;

        [Client]
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                rigidbody3d.AddForce(Vector3.up * force);
        }
    }
}
