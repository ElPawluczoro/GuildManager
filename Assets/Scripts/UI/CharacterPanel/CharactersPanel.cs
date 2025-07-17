using UnityEngine;

namespace UI.CharacterPanel
{
    public class CharactersPanel : MonoBehaviour
    {
        [SerializeField] private GameObject[] characters = new GameObject[3];

        private byte currentCharacter = 0;
        
        public void SwitchCharacter()
        {
            currentCharacter++;
            if(currentCharacter >= 3) currentCharacter = 0;
        }

        public void SwitchCharacter(int i)
        {
            currentCharacter = (byte)i;
        }
    }
}