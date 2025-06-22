using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace trial
{
    public class Player : Entity
    {
        public int MaxMana { get; } // Maximum mana of the player
        public double Mana { get; set; } // Current mana of the player
        private double ManaRate { get; set; } // Rate at which mana regenerates per second
        public int Experience { get; set; } // Experience points of the player

        public Player(Vector2 position, Texture2D texture, string id, int level, int maxHealth, double health, double speed, List<Modifier> appliedModifiers, int maxMana, double mana,double manaRate, int experience)
        {
            Position = position;
            Texture = texture;
            ID = id;
            Level = level;
            MaxHealth = maxHealth;
            Health = health;
            Speed = speed;
            AppliedModifiers = appliedModifiers;
            MaxMana = maxMana;
            Mana = mana;
            ManaRate = manaRate;
            Experience = experience;
            scale = 1; // Default scale
        }
        public Player(string name)
        {
            ID = name;
            Level = 1; // Default level
            MaxHealth = 10; // Default maximum health
            Health = MaxHealth; // Start with full health
            Speed = 2.0; // Default speed
            Position = new Vector2(0, 0); // Starting position
            MaxMana = 10; // Default maximum mana
            Mana = MaxMana; // Start with full mana
            ManaRate = 0.1; // Default mana regeneration rate per second
            Experience = 0; // Start with 0 experience
            scale = 1; // Default scale
            AppliedModifiers = new List<Modifier>(); // Initialize the list of modifiers
        }
        private bool CanLevelUp()
        {
            if (Experience >= 100 + (50 * (Level - 1))) {  return true; } return false;
        }

        public void LevelUp()
        {
            while (CanLevelUp())
            {
                Level += 1;
                Experience -= (100 + (50 * (Level - 1)));
                //Add leveling up effects here like improving stats and giving skillpoints
            }
        }

    }
}


