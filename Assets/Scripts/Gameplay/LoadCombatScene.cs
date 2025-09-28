using System;
using System.Collections.Generic;
using Characters;
using UnityEngine;

namespace Gameplay
{
    public class LoadCombatScene : MonoBehaviour
    {
        [SerializeField] private List<GameObject> playerCharactersList = new();
        [SerializeField] private List<GameObject> enemyCharactersList = new();

        public List<SOCharacter> testPlayerCharacters;
        private void Start()
        {
            LoadScene();
        }

        public void LoadScene()
        {
            CombatInformation combatInformation =
                GameObject.FindGameObjectWithTag("CombatInformation").GetComponent<CombatInformation>();

            SetupCharacters(combatInformation.PlayerCharacters, playerCharactersList);
            SetupCharacters(combatInformation.EnemyCharacters, enemyCharactersList);
        }

        private void SetupCharacters(List<GameObject> origin, List<GameObject> target)
        {
            for (int i = 0; i < origin.Count; i++)
            {
                var newCharacter = target[i];
                newCharacter.SetActive(true);
                newCharacter.GetComponent<Animator>().runtimeAnimatorController =
                    origin[i].GetComponent<BasicCharacter>().CharacterSO
                        .GetAnimatorOverrideController();
                
                origin[i].transform.SetParent(newCharacter.transform);
            }
        }
    }
}