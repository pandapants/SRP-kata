using System;
using System.Collections.Generic;
using System.Text;

namespace SrpTask.Contracts
{
    public interface IInventoryService
    {
        bool Exists(Item item, List<Item> inventory);

        int GetTotalWeight(List<Item> inventory);

        int GetTotalArmour(List<Item> inventory);
    }
}
