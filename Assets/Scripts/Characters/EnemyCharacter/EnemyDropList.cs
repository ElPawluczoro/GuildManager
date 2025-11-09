using System.Collections.Generic;
using UnityEngine;

namespace Characters.EnemyCharacter
{
    public class EnemyDropList : MonoBehaviour
    {
        [SerializeField] private List<ScriptableObject> dropList;
        [SerializeField] private int minimumDroppedItems = 1;
        [SerializeField] private int maximumDroppedItems = 3;
        
        public void ReturnDrop()
        {
            
        }
    }
}