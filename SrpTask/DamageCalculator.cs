using SrpTask.Contracts;
using SrpTask.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SrpTask
{
    public class DamageCalculator : IDamageCalculator
    {
        public bool ArmourIsSufficient(int initialDamange, int armour)
        {
            return armour > initialDamange;
        }

        public int GetLightInventoryDamageReduction(Inventory inventory, int initialDamage)
        {
            int damageReduction = 0;

            if (inventory.Weight < (inventory.CarryingCapacity / 2))
            {
                damageReduction = (int)(initialDamage * 0.25);
            }

            return damageReduction;
        }

        public int GetTotalDamageReduction(int initialDamage, Inventory inventory)
        {
            int damageReduction = 0;

            damageReduction += inventory.Armour;
            damageReduction += this.GetLightInventoryDamageReduction(inventory, initialDamage);

            return damageReduction;
        }
    }
}
