using System;
using UnityEngine;

namespace Characters
{
    public class BasicCharacter : MonoBehaviour
    {
        [SerializeField] private Guid ID;
        string characterName;

        public Guid _ID { get { return ID; } }

       public void InitializeCharacter()
        {
            ID = Guid.NewGuid();
            characterName = ID.ToString(); //TODO change after test
        }



    }
}

