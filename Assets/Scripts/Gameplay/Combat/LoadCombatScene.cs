using System;
using System.Collections.Generic;
using Characters;
using UnityEngine;
using Utilities;

namespace Gameplay.Combat
{
    public class LoadCombatScene : MonoBehaviour
    {
        [SerializeField] private List<GameObject> playerCharactersList = new();
        [SerializeField] private List<GameObject> enemyCharactersList = new();
        
        private CharacterGridBackend characterGridBackend;
        private Grid grid;
            
        public List<SOCharacter> testPlayerCharacters;
        private void Start()
        {
            characterGridBackend = FindAnyObjectByType<CharacterGridBackend>();
            grid = FindAnyObjectByType<Grid>();
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
                
                
                Utils.CopyValues(origin[i].GetComponent<CharacterBasicStats>(),
                    newCharacter.GetComponent<CharacterBasicStats>());
                
                Utils.CopyValues(origin[i].GetComponent<CharacterAdvancedStats>(),
                    newCharacter.GetComponent<CharacterAdvancedStats>());
                
                /*Utils.CopyValues(origin[i].GetComponent<CharacterHealth>(),
                    newCharacter.GetComponent<CharacterHealth>());*/
                
                newCharacter.GetComponent<CharacterHealth>().SetUpDependences();
                newCharacter.GetComponent<CharacterHealth>().UpdateMaxHealth();



                //origin[i].transform.SetParent(newCharacter.transform);
            }
        }
    }
}
















