using System.Collections.Generic;
using System;
using UnityEngine;

namespace Characters
{
    public class CharacterStatus : MonoBehaviour
    {
        CharacterHealth health;

        List<OvertimeEffect> effects = new();

        private void Start()
        {
            health = GetComponent<CharacterHealth>();
        }

        private void Update()
        {
            List<OvertimeEffect> toRemove = new List<OvertimeEffect>();
            foreach (OvertimeEffect effect in effects)
            {
                effect.DoEffect(health);
                if(effect.remainingTicks <= 0) toRemove.Add(effect);
            }

            foreach (OvertimeEffect tr_effect in toRemove)
            {
                effects.Remove(tr_effect);
            }
        }

        public void AddNewEffect(EffectType effectType, float dmagePerTick, Guid source)
        {
            OvertimeEffect newEffect = new OvertimeEffect(effectType, dmagePerTick, source);
            OvertimeEffect flaggedToDestroy = null;
            bool added = false;
            if (effects.Count == 0) { effects.Add(newEffect); return; }

            if (newEffect.effectType == EffectType.STUN)
            {
                foreach (OvertimeEffect effect in effects)
                {
                    if (effect.effectType == EffectType.STUN) flaggedToDestroy = effect;
                    break;
                }
                if (flaggedToDestroy != null) effects.Remove(flaggedToDestroy);
            }

            foreach (OvertimeEffect effect in effects)
            {
                if(effect == newEffect)
                {
                    switch (effectType)
                    {
                        case EffectType.BLEED: effect.remainingTicks = OvertimeEffect.bleedDuration; break;
                        case EffectType.BURN: effect.remainingTicks = OvertimeEffect.burnDuration; break;
                    }

                    added = true;
                    break;
                }
                else if (effect.source == newEffect.source && effect.damage < newEffect.damage)
                {
                    effects.Add(newEffect);
                    added = true;
                    flaggedToDestroy = effect;
                }
            }

            if(flaggedToDestroy != null) effects.Remove(flaggedToDestroy);
            if (!added) effects.Add(newEffect);

        }
    }

    public class OvertimeEffect
    {
        public readonly EffectType effectType;
        public readonly float damage;
        public readonly Guid source;
        public int remainingTicks;

        public static readonly int bleedDuration = 3;
        public static readonly int burnDuration = 4;
        public static readonly int stunDuration = 1;


        public OvertimeEffect(EffectType effectType, float damage, Guid source)
        {
            this.effectType = effectType;
            this.damage = damage;
            this.source = source;

            switch (effectType)
            {
                case EffectType.BLEED: remainingTicks = bleedDuration; break;
                case EffectType.BURN: remainingTicks = burnDuration; break;
                case EffectType.STUN: remainingTicks = stunDuration; this.damage = 0; break;
            }
        }

        public void DoEffect(CharacterHealth characterHealth)
        {
            switch (effectType)
            {
                case EffectType.BLEED:
                    characterHealth.RecieveDamage(DamageType.PHYSICAL, this.damage);
                    break;
                case EffectType.BURN:
                    characterHealth.RecieveDamage(DamageType.PHYSICAL, this.damage);
                    break;
                case EffectType.STUN:
                    //TODO stun logic
                    break;
            }
            remainingTicks--;
        }

        public static bool operator ==(OvertimeEffect a, OvertimeEffect b)
        {
            return a.effectType == b.effectType && a.damage == b.damage && a.source == b.source;
        }

        public static bool operator !=(OvertimeEffect a, OvertimeEffect b)
        {
            return a.effectType != b.effectType || a.damage != b.damage || a.source != b.source;
        }
    }

    public enum EffectType
    {
        BLEED, BURN, STUN
    }
}
