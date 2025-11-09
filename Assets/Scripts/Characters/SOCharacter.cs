using UnityEditor.Animations;
using UnityEngine;

namespace Characters
{
    [CreateAssetMenu(fileName = "SOCharacter", menuName = "Characters/Character", order = 0)]
    public class SOCharacter : ScriptableObject
    {
        [SerializeField] private AnimatorOverrideController animatorOverrideController;

        public AnimatorOverrideController GetAnimatorOverrideController() { return animatorOverrideController; }
}
}