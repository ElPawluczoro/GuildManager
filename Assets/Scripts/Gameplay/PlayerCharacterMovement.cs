using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Gameplay
{
    public class PlayerCharacterMovement : MonoBehaviour
    {
        private Animator animator;
        private List<KeyCode> directions = new();
        private static readonly int WalkingLeft = Animator.StringToHash("WalkingLeft");
        private static readonly int WalkingRight = Animator.StringToHash("WalkingRight");
        private static readonly int WalkingUp = Animator.StringToHash("WalkingUp");
        private static readonly int WalkingDown = Animator.StringToHash("WalkingDown");
        private static readonly int Idle = Animator.StringToHash("Idle");

        public float movementSpeed = 5;

        private bool idle;
        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            directions.Clear();
            
            if (Input.GetKey(KeyCode.W))
            {
                directions.Add(KeyCode.W);
            }
            if (Input.GetKey(KeyCode.S))
            {
                directions.Add(KeyCode.S);
            }
            if (Input.GetKey(KeyCode.A))
            {
                directions.Add(KeyCode.A);
            }
            if (Input.GetKey(KeyCode.D))
            {
                directions.Add(KeyCode.D);
            }

            var multiplier = 0f;

            if (directions.Count == 2)
            {
                multiplier = math.sqrt(math.pow(movementSpeed, 2) * 0.5f);
            }
            else
            {
                multiplier = movementSpeed;
            }

            if (Input.GetKey(KeyCode.W))
            {
                idle = false;
                transform.position += new Vector3(0, multiplier * Time.deltaTime, 0);
                SetAnimatorBoolTrue(WalkingUp);
            }
            if (Input.GetKey(KeyCode.S))
            {
                idle = false;
                transform.position += new Vector3(0, -multiplier * Time.deltaTime, 0);
                SetAnimatorBoolTrue(WalkingDown);
            }
            if (Input.GetKey(KeyCode.D))
            {
                idle = false;
                transform.position += new Vector3(multiplier * Time.deltaTime, 0, 0);
                SetAnimatorBoolTrue(WalkingRight);
            }
            if (Input.GetKey(KeyCode.A))
            {
                idle = false;
                transform.position += new Vector3(-multiplier * Time.deltaTime, 0, 0);
                SetAnimatorBoolTrue(WalkingLeft);
            }

            if (idle) return;
            
            if (!Input.GetKey(KeyCode.W) & !Input.GetKey(KeyCode.S) & !Input.GetKey(KeyCode.A) &
                !Input.GetKey(KeyCode.D))
            {
                idle = true;
                SetAnimatorBoolTrue(Idle);
            }
        }

        public void SetAnimatorBoolTrue(int animationHash)
        {
            animator.SetBool(animationHash, true);
            
            if(animationHash != WalkingLeft) animator.SetBool(WalkingLeft, false);
            if(animationHash != WalkingRight) animator.SetBool(WalkingRight, false);
            if(animationHash != WalkingUp) animator.SetBool(WalkingUp, false);
            if(animationHash != WalkingDown) animator.SetBool(WalkingDown, false);
            if(animationHash != Idle) animator.SetBool(Idle, false);
        }
        
    }
}