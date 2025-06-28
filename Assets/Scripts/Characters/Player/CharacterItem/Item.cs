using System;
using UnityEngine;

namespace Characters.Player.CharacterItem
{
    public abstract class Item : MonoBehaviour
    {
        [SerializeField] protected string itemName;
        [SerializeField] protected Int16 itemValue;

        public string ItemName => itemName;

        public short ItemValue => itemValue;

        //public abstract void CreateItem();
    }
}
