using System;
using UnityEngine;

namespace Gameplay.Combat
{
    public class FollowTarget : MonoBehaviour
    {
        public float attackRange;
        public float movementSpeed;
        public Transform target;
        
        
        public void Update()
        {
            if (!target) return;
            
            Follow();
        }

        private void Follow()
        {
            float distance = Vector3.Distance(target.position, transform.position);
            if (distance <= attackRange) return;
            transform.position = Vector3.MoveTowards(transform.position, target.position, movementSpeed * Time.deltaTime);
        }
        
    }
}