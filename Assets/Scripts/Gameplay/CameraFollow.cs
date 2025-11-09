using System;
using UnityEngine;

namespace Gameplay
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset = Vector3.one;
        [SerializeField] private float smoothSpeed = 0.125f;
        private void Update()
        {
            Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, -10) + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}