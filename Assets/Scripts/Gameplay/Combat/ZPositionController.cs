using System;
using UnityEngine;

namespace Gameplay.Combat
{
    public class ZPositionController : MonoBehaviour
    {
        private void LateUpdate()
        {
            Vector3 pos = transform.position;
            pos.z = pos.y * 0.01f;
            transform.position = pos;
        }
    }
}