using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trial
{
    public class Entity
    {
        public string ID { get; set; } // Unique identifier for the entity - Overridden in case of player.
        public int Level { get; set; } // Level of the entity, used for scaling difficulty

        public int MaxHealth { get; set; } // Maximum health of the entity
        public double Health { get; set; } // Current health of the entity
        public int Damage { get; set; } // Default damage of the entity
        public double Speed { get; set; } // Speed the entity moves at

        public void TakeDamage(double damageAmount) // Method to apply damage to the entity
        {
            Health -= damageAmount;
            if (Health < 0)
            {
                Health = 0; // Ensure health does not go below zero
            }
            Health = Math.Round(Health, 2); // Round health to two decimal places
        }
        public void Heal(double healAmount) // Method to heal the entity
        {
            Health += healAmount;
            if (Health > MaxHealth)
            {
                Health = MaxHealth; // Ensure health does not exceed maximum health
            }
            Health = Math.Round(Health, 2); // Round health to two decimal places
        }
    }
}
