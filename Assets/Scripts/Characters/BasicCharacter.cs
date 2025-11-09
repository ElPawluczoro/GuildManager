using System;
using UnityEngine;

namespace Characters
{
    public class BasicCharacter : MonoBehaviour
    {
        [SerializeField] private Guid ID;
        string characterName;

        public Guid _ID { get { return ID; } }

        [SerializeField] private SOCharacter character;
        public SOCharacter CharacterSO { get { return character; } }
        
       public void InitializeCharacter()
        {
            ID = Guid.NewGuid();
            characterName = ID.ToString(); //TODO change after test
        }



    }
}

