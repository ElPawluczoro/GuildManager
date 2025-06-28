using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Characters.Player
{
    public class CharacterLevel : MonoBehaviour
    {
        public const byte maxLevel = 10;
        public static readonly UInt16[] xpForLevel = 
            {
                100, 150, 250, 400, 600,
                750, 1050, 1400, 1800, 2500
            };

        private byte currentLevel;
        private UInt16 maxXp;
        private UInt16 currentXp;

        private PointsContainer points;

        public byte CurrentLevel { get { return currentLevel; } }
        public UInt16 MaxXp { get { return maxXp; } }
        public UInt16 CurrentXp { get { return currentXp; } }

        private void Start()
        {
            points = GetComponent<PointsContainer>();
        }

        public void AddXP(UInt16 amount)
        {
            if (currentLevel >= maxLevel) return;

            currentXp += amount;

            if(currentXp >= maxXp)
            {
                currentXp -= maxXp;
                currentLevel++;
                if (currentLevel == maxLevel) currentXp = maxXp;
                else maxXp = xpForLevel[currentLevel - 1];
                OnLevelUp();
            }
        }

        public void OnLevelUp()
        {
            points.AddPassivePoint();
            points.AddSkillPoint();
        }


    }
}
