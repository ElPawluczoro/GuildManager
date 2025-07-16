using UnityEngine;

namespace Characters.Player.CharacterItem.SEquipmentBases
{
    public abstract class SEquipmentBase : ScriptableObject
    {
        [SerializeField] private string baseName;
        [SerializeField] private Vector3Int statRequirements;
        [SerializeField] private byte levelRequirement;
        [SerializeField] private EquipmentType equipmentType;
        [SerializeField] private Sprite icon;
        [SerializeField] private Vector2 size;

        public string BaseName => baseName;
        public Vector3Int StatRequirements => statRequirements;
        public byte LevelRequirement => levelRequirement;
        public EquipmentType EquipmentType => equipmentType;
        public Sprite Icon => icon;
        public Vector2 Size => size;
    }
}