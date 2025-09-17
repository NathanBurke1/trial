using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace trial
{
    public class Enemy : Entity
    {
        public double Damage { get; set; } // Damage the enemy deals
        public bool isAlive { get; set; }
        public Enemy(Vector2 position, Texture2D texture, string id, int level, int maxHealth, float speed, List<Modifier> appliedModifiers, double damage, float scale)
        {
            Position = position;
            Texture = texture;
            ID = id;
            Level = level;
            MaxHealth = maxHealth;
            Health = maxHealth;
            Speed = speed;
            AppliedModifiers = appliedModifiers;
            Damage = damage;
            scale = scale; // Default scale for the enemy
        }
        public Enemy(Vector2 position, Texture2D texture, string id, int level, int maxHealth, float speed, double damage)
        {
            Position = position;
            Texture = texture;
            ID = id;
            Level = level;
            MaxHealth = maxHealth;
            Health = maxHealth;
            Speed = speed;
            AppliedModifiers = new List<Modifier>();
            Damage = damage;
            scale = 1f;
        }
    }

}
