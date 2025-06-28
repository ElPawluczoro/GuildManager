using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Characters.Player
{
    public class PointsContainer
    {
        private byte passivePoints;
        private byte skillPoints;

        public byte PassivePoints { get { return passivePoints; } }
        public byte SkillPoints { get {return skillPoints; } }

        public void AddPassivePoint()
        {
            passivePoints++;
        }

        public void RemovePassivePoint() 
        {
            passivePoints--; 
        }

        public void AddSkillPoint()
        {
            skillPoints++;
        }

        public void RemoveSkillPoint()
        {
            skillPoints--;
        }
    }
}
