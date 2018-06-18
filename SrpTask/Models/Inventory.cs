using System;
using System.Collections.Generic;
using System.Text;

namespace SrpTask.Models
{
    public class Inventory
    {
        public List<Item> ItemList { get; set; }

        public int Weight { get; set; }

        public int Armour { get; set; }

        public int CarryingCapacity { get; set; }
    }
}
