using SrpTask.Contracts;
using SrpTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrpTask
{
    public class InventoryService : IInventoryService
    {
        public bool ExistsInInventory(Item item, List<Item> inventory)
        {
            return inventory.Any(x => x.Id == item.Id);
        }

        public bool ItemCanBePickedUp(Item item, Inventory inventory)
        {
            var response = true;

            if (inventory.Weight + item.Weight > inventory.CarryingCapacity)
                response = false;

            if (item.Unique && this.ExistsInInventory(item, inventory.ItemList))
                response = false;

            if (item.Heal > 0)
                response = false;

            return response;
        }
    }
}
