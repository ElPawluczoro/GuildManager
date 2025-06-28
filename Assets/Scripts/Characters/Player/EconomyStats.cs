using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Characters.Player
{
    public class EconomyStats : MonoBehaviour
    {
        [SerializeField] private UInt16 hireCost;
        [SerializeField] private UInt16 salary;

        public UInt16 HireCost { get { return hireCost; } }
        public UInt16 Salary { get { return salary; } }

        public void IncreaseSalary(UInt16 value)
        {
            salary += value;
        }

        public void DecreaseSalary(UInt16 value)
        {
            salary -= value;
        }
    }
}
