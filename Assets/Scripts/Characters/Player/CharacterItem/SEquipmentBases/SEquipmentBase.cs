using UnityEngine;

namespace Characters.Player.CharacterItem.SEquipmentBases
{
    [CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public class SEquipmentBase : ScriptableObject
    {
        private string baseName;
        private Vector3Int statRequirements;
        private byte levelRequirement;
        private EquipmentType equipmentType;

        public string BaseName => baseName;
        public Vector3Int StatRequirements => statRequirements;

        public byte LevelRequirement => levelRequirement;

        public EquipmentType EquipmentType => equipmentType;
    }
}