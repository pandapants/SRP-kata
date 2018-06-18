using SrpTask.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SrpTask.Contracts
{
    public interface IDamageCalculator
    {
        bool ArmourIsSufficient(int initialDamange, int armour);

        int GetLightInventoryDamageReduction(Inventory inventory, int damage);

        int GetTotalDamageReduction(int damage, Inventory inventory);
    }
}
