using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Characters;
using ProjectEnums;
using UnityEngine;
using Random = System.Random;

namespace Gameplay.Combat
{
    public class FightingScript : MonoBehaviour
    {
        private static readonly int Attacking = Animator.StringToHash("attacking");
        public float attackRange;
        public float movementSpeed;
        public Transform target;
        public bool alive = true;

        private CharacterBasicStats basicStats;
        
        private Random random = new Random();
        
        [SerializeField] private Side side; 
        
        public Side Side => side;
        
        private void Update()
        {
            if (!alive)
            {
                StopCoroutine(KeepClosestTarget());
                StopAttacking();
                //this.enabled = false;
            }
            
            if (!target)
            {
                StopAttacking();
                return;
            }
            float distance = Vector3.Distance(target.position, transform.position);
            if (distance > attackRange) return;

            if (target.GetComponent<CharacterHealth>().IsDead())
            {
                StopAttacking();
                SetTarget();
                return;
            }
            
            StartAttacking();
        }

        public void StartFighting()
        {
            if (!alive) return;
            StartCoroutine(KeepClosestTarget());
            basicStats = GetComponent<CharacterBasicStats>();
        }

        private void SetTarget()
        {
            List<FightingScript> characters =
                FindObjectsByType<FightingScript>(FindObjectsInactive.Exclude, FindObjectsSortMode.None).ToList();
            List<FightingScript> toRemove = new List<FightingScript>();
            for (int i = characters.Count - 1; i >= 0; i--)
            {
                if (characters[i].Side == side || !characters[i].alive) toRemove.Add(characters[i]);
            }

            foreach (FightingScript character in toRemove)
            {
                characters.Remove(character);
            }

            if (characters.Count == 0) 
            {
                target = null;
                return;
            }

            Transform tar = characters[0].transform;
            float distance = Mathf.Infinity;
            foreach (var character in characters)
            {
                var dis = Vector3.Distance(character.transform.position, transform.position);
                if (dis < distance)
                {
                    distance = dis;
                    tar = character.transform;
                }
            }
            
            GetComponent<FollowTarget>().target = tar;
            target = tar;
        }

        private IEnumerator KeepClosestTarget()
        {
            while (alive)
            {
                SetTarget();
                yield return new WaitForSeconds(0.1f);
            }
        }

        private void StartAttacking()
        {
            if (!alive) return;
            //Debug.Log("Attacking started");
            GetComponent<Animator>().SetBool(Attacking, true);
        }

        private void StopAttacking()
        {
            //Debug.Log("Attacking stopped");
            GetComponent<Animator>().SetBool(Attacking, false);
        }

        private void Attack()
        {
            if (target == null) return;
            var damage = random.Next(basicStats.GetStat(ECharacterBasicStat.MIN_ATTACK_DAMAGE),
                basicStats.GetStat(ECharacterBasicStat.MAX_ATTACK_DAMAGE));
            target.GetComponent<CharacterHealth>().ReceiveDamage(DamageType.PHYSICAL,damage);
            //Debug.Log("Attack");
        }
    }

    public enum Side
    {
        PLAYER, ENEMY
    }
}