using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Characters
{
    public class CharacterMana : MonoBehaviour
    {
        private float mana;
        private float currentMana;
        private float manaRegeneration;

        private CharacterBasicStats bStats;
        private CharacterAdvancedStats aStats;
        private CharacterSkillContainer skillContainer;

        public float Mana { get { return mana; } }
        public float CurrentMana { get { return currentMana; } }
        public float ManaRegeneration { get { return manaRegeneration; } }

        private void Start()
        {
            bStats = GetComponent<CharacterBasicStats>();
            aStats = GetComponent<CharacterAdvancedStats>();

            mana = bStats.Mana;
            currentMana = 0;
            UpdateManaRegeneration();
        }

        public void UpdateManaRegeneration()
        {
            manaRegeneration = aStats.ManaRegen;
        }

        public void OnAttack()
        {
            currentMana += manaRegeneration;
            if(currentMana >= mana)
            {
                currentMana -= mana;
                //TODO cast skill logic
            }
        }


    }
}
