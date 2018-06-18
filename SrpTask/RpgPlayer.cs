using SrpTask.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace SrpTask
{
    public class RpgPlayer
    {
        public const int MaximumCarryingCapacity = 1000;

        private readonly IGameEngine _gameEngine;

        private readonly IInventoryService _inventoryService;

        public int Health { get; set; }

        public int MaxHealth { get; set; }

        public int Armour { get; private set; }

        public List<Item> Inventory;

        /// <summary>
        /// How much the player can carry in kilograms
        /// </summary>
        public int CarryingCapacity { get; private set; }

        public RpgPlayer(IGameEngine gameEngine, IInventoryService inventoryService)
        {
            _gameEngine = gameEngine;
            _inventoryService = inventoryService;
            Inventory = new List<Item>();
            CarryingCapacity = MaximumCarryingCapacity;
        }

        public void UseItem(Item item)
        {
            if (item.Name == "Stink Bomb")
            {
                var enemies = _gameEngine.GetEnemiesNear(this);

                foreach (var enemy in enemies)
                {
                    enemy.TakeDamage(100);
                }
            }
        }

        public bool PickUpItem(Item item)
        {
            var weight = _inventoryService.GetTotalWeight(Inventory);
            if (weight + item.Weight > CarryingCapacity)
                return false;

            if (item.Unique && _inventoryService.Exists(item, Inventory))
                return false;

            // Don't pick up items that give health, just consume them.
            if (item.Heal > 0)
            {
                Health += item.Heal;

                if (Health > MaxHealth)
                    Health = MaxHealth;

                if (item.Heal > 500)
                {
                    _gameEngine.PlaySpecialEffect("green_swirly");
                }

                return true;
            }

            if (item.Rare)
                _gameEngine.PlaySpecialEffect("cool_swirly_particles");

            if (item.Rare && item.Unique)
                _gameEngine.PlaySpecialEffect("blue_swirly");

            Inventory.Add(item);

            Armour = _inventoryService.GetTotalArmour(Inventory);

            return true;
        }

        public void TakeDamage(int damage)
        {
            int inventoryWeight = _inventoryService.GetTotalWeight(Inventory);
            int damageToDeduct = 0;

            if (damage < Armour)
            {
                _gameEngine.PlaySpecialEffect("parry");
                return;
            }

            if (inventoryWeight < (CarryingCapacity / 2))
            {
                damageToDeduct = (int)(damage * 0.25);
            }

            var damageToDeal = damage - Armour - damageToDeduct;
            Health -= damageToDeal;
            
            _gameEngine.PlaySpecialEffect("lots_of_gore");
        }
    }
}