using System;
using UnityEngine;

namespace Script.Tools
{
    public class RotateToCamera : MonoBehaviour
    {
        private Camera camera;
        
        private void Update()
        {
            if (camera == null || !camera.gameObject.activeSelf)
            {
                camera = Camera.current;
                return;
            }
            transform.rotation = Quaternion.Euler(
                Vector3.Scale(
                    Quaternion.LookRotation(transform.position - camera.transform.position).eulerAngles, Vector3.up
                    )
                );
        }
    }
}