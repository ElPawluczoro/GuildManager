using UnityEngine;

namespace Characters.Player.CharacterItem
{
    [System.Serializable]
    public class Affix
    {
        [SerializeField] private AffixType affixType; //TODO should be readonly, after tests
        [SerializeField] private Affixes affix; //TODO should be readonly, after tests
        [SerializeField] private float value;
        [SerializeField] private byte tier;

        public AffixType AffixType => affixType;

        public Affixes Affix1 => affix;

        public float Value => value;

        public byte Tier => tier;

        public Affix(AffixType affixType, Affixes affix, float value, byte tier)
        {
            this.affixType = affixType;
            this.affix = affix;
            this.value = value;
            this.tier = tier;
        }
    }
}