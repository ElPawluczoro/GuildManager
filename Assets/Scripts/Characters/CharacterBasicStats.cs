using System;
using UnityEngine;
using ProjectEnums;

namespace Characters
{
    public class CharacterBasicStats : MonoBehaviour
    {
        [SerializeField] private Int16 strength;
        [SerializeField] private Int16 dexterity;
        [SerializeField] private Int16 intelligence;
        [SerializeField] private Int16 vitality;
        [SerializeField] private Int16 armour;
        [SerializeField] private Int16 magicResistance;
        [SerializeField] private Int16 dodge;
        [SerializeField] private Int16 attackRange;
        [SerializeField] private Int16 attackSpeed;
        [SerializeField] private Int16 castSpeed;
        [SerializeField] private Int16 movementSpeed;
        [SerializeField] private Int16 mana;
        
        public Int16 Strength { get { return strength; } }
        public Int16 Dexterity { get { return dexterity; } }
        public Int16 Intelligence { get { return intelligence; } }
        public Int16 Vitality { get {return vitality; } }   
        public Int16 Armour { get { return armour; } }  
        public Int16 MagicResistance { get { return magicResistance; } }  
        public short Dodge => dodge;
        public Int16 AttackRange { get { return attackRange; } }    
        public Int16 AttackSpeed { get { return attackSpeed; } }    
        public Int16 CastSpeed { get {return castSpeed; } } 
        public Int16 MovementSpeed { get { return movementSpeed; } }
        public Int16 Mana { get { return mana; } }

        public void ModifyStrength(EOperation op, Int16 val) => Modify(ref strength, op, val);
        public void ModifyDexterity(EOperation op, Int16 val) => Modify(ref dexterity, op, val);
        public void ModifyIntelligence(EOperation op, Int16 val) => Modify(ref intelligence, op, val);
        public void ModifyVitality(EOperation op, Int16 val) => Modify(ref vitality, op, val);
        public void ModifyArmour(EOperation op, Int16 val) => Modify(ref armour, op, val);
        public void ModifyMagicResistance(EOperation op, Int16 val) => Modify(ref magicResistance, op, val);
        public void ModifyDodge(EOperation op, Int16 val) => Modify(ref dodge, op, val);
        public void ModifyAttackRange(EOperation op, Int16 val) => Modify(ref attackRange, op, val);
        public void ModifyAttackSpeed(EOperation op, Int16 val) => Modify(ref attackSpeed, op, val);
        public void ModifyCastSpeed(EOperation op, Int16 val) => Modify(ref castSpeed, op, val);
        public void ModifyMovementSpeed(EOperation op, Int16 val) => Modify(ref movementSpeed, op, val);
        public void ModifyMana(EOperation op, Int16 val) => Modify(ref mana, op, val);

        private void Modify(ref Int16 field, EOperation operation, Int16 value)
        {
            if (operation == EOperation.SET) { field = value; return; }
            if (operation == EOperation.SUBSTRACT) { value *= -1; }
            field += value;
        }


    }
}
