using SrpTask.Contracts;
using SrpTask.Models;
using System.Collections.Generic;
using System.Linq;

namespace SrpTask
{
    public class RpgPlayer
    {
        public const int MaximumCarryingCapacity = 1000;

        private readonly IGameEngine _gameEngine;

        private readonly IInventoryService _inventoryService;

        private readonly IDamageCalculator _damageCalculator;

        public int Health { get; set; }

        public int MaxHealth { get; set; }

        //public int Armour { get; private set; }

        public Inventory Inventory;

        ///// <summary>
        ///// How much the player can carry in kilograms
        ///// </summary>
        //public int CarryingCapacity { get; private set; }

        public RpgPlayer(IGameEngine gameEngine, IInventoryService inventoryService, IDamageCalculator damageCalculator)
        {
            _gameEngine = gameEngine;
            _inventoryService = inventoryService;
            _damageCalculator = damageCalculator;

            Inventory = new Inventory()
            {
                ItemList = new List<Item>(),
                CarryingCapacity = MaximumCarryingCapacity,
                Armour = 0,
                Weight = 0
            };
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

        public void PickUpItem(Item item)
        {
            if(_inventoryService.ItemCanBePickedUp(item, Inventory))
            {
                Inventory.ItemList.Add(item);
                Inventory.Weight += item.Weight;
                Inventory.Armour += item.Armour;
            }

            if (item.Rare)
                _gameEngine.PlaySpecialEffect("cool_swirly_particles");

            if (item.Rare && item.Unique)
                _gameEngine.PlaySpecialEffect("blue_swirly");

            if (item.Heal > 0)
            {
                Health += item.Heal;

                if (Health > MaxHealth)
                    Health = MaxHealth;

                if (item.Heal > 500)
                {
                    _gameEngine.PlaySpecialEffect("green_swirly");
                }
            }
        }

        public void TakeDamage(int damage)
        {
            if (_damageCalculator.ArmourIsSufficient(damage, Inventory.Armour))
            {
                _gameEngine.PlaySpecialEffect("parry");
            }
            else
            {
                var damageReduction = _damageCalculator.GetTotalDamageReduction(damage, Inventory));
                Health -= (damage - damageReduction);

                _gameEngine.PlaySpecialEffect("lots_of_gore");
            }
        }
    }
}