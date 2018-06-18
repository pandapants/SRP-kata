using SrpTask.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrpTask
{
    public class InventoryService : IInventoryService
    {
        public bool Exists(Item item, List<Item> inventory)
        {
            return inventory.Any(x => x.Id == item.Id);
        }

        public int GetTotalWeight(List<Item> inventory)
        {
            return inventory.Sum(x => x.Weight);
        }

        public int GetTotalArmour(List<Item> inventory)
        {
            return inventory.Sum(x => x.Armour);
        }
    }
}
