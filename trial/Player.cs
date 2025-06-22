using Microsoft.Xna.Framework;
using System.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trial
{
    public class Player : Entity
    {
        public Vector2 Position { get; set; }
        public int MaxMana { get; }
        public double Mana { get; set; } // Current mana of the player
        public int Experience { get; set; } // Experience points of the player
        private double ManaRegenRate { get; set; } // Rate at which mana regenerates per second

        public Player(string name, int level, int maxHealth, double health, int damage, double speed, Vector2 position, int maxMana, double mana, double manaRegenRate)
        {
            ID = name;
            Level = level;
            MaxHealth = maxHealth;
            Health = health;
            Damage = damage;
            Speed = speed;
            Position = position;
            MaxMana = maxMana;
            Mana = mana; // Start with full mana
            ManaRegenRate = manaRegenRate;
        }
        public Player(string name)
        {
            ID = name;
            Level = 1; // Default level
            MaxHealth = 10; // Default maximum health
            Health = MaxHealth; // Start with full health
            Damage = 1; // Default damage
            Speed = 2.0; // Default speed
            Position = new Vector2(0, 0); // Starting position
            MaxMana = 10; // Default maximum mana
            Mana = MaxMana; // Start with full mana
            ManaRegenRate = 0.1; // Default mana regeneration rate per second
        }
    }
}


