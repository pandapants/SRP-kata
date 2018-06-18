﻿using System;
using System.Collections.Generic;
using System.Text;
using SrpTask.Entities;

namespace SrpTask.Contracts
{
    public interface IInventoryService
    {
        bool ExistsInInventory(Item item, List<Item> inventory);
        bool ItemCanBePickedUp(Item item, Inventory inventory);
    }
}
