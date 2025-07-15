using System;
using UnityEngine;

namespace Characters.Player.CharacterItem
{
    public abstract class Item : MonoBehaviour
    {
        [SerializeField] protected string itemName;
        [SerializeField] protected Int16 itemValue;
        protected Guid guid;

        public string ItemName => itemName;

        public short ItemValue => itemValue;
        
        public Guid Guid => guid;
        
        //public abstract void CreateItem();
    }
}
