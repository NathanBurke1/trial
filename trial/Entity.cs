using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace trial
{
    public class Entity
    {
        public Vector2 Position { get; set; }
        public Texture2D Texture { get; set; }
        public float scale { get; set; } // Scale of the entity, used for rendering
        public string ID { get; set; } // Unique identifier for the entity - name in case of player.
        public int Level { get; set; } // Level of the entity, used for scaling difficulty

        public int MaxHealth { get; set; } // Maximum health of the entity
        public double Health { get; set; } // Current health of the entity
        public double Speed { get; set; } // Speed the entity moves at

        public List<Modifier> AppliedModifiers; //The effects that the entity is currently experiencing
        private bool isAlive {  get; set; }

        public void Draw() { }
        public void Remove() { }
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
