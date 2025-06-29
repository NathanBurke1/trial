using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace trial
{
    public class Effect
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public byte Type { get; set; }

        public List<Enemy> Split(int index, List<Enemy> enemies)
        {
            if (enemies[index].MaxHealth < 4)
            {
                return enemies; // Cannot split if health is too low
            }
            Enemy enemy = enemies[index];
            enemy.ID = enemy.ID + "_split_";
            enemy.Level = (int)Math.Round((decimal)(enemy.Level / 4));
            enemy.MaxHealth = (int)Math.Round((decimal)(enemy.MaxHealth / 4));
            enemy.Health = enemy.MaxHealth;
            enemy.Speed = enemy.Speed / 2;
            enemy.AppliedModifiers = new List<Modifier>(enemy.AppliedModifiers);
            enemy.scale = 0.3f;
            enemy.Damage = enemy.Damage / 4;
            Vector2 pos = enemy.Position;
            pos.X -= 6;
            for (int i = 0; i < 3; i++)
            {
                pos.X += 3;
                enemies.Add(new Enemy(pos, enemy.Texture, enemy.ID + i.ToString(), enemy.Level, enemy.MaxHealth, enemy.Speed, enemy.AppliedModifiers, enemy.Damage, enemy.scale));
            }
            return enemies;
        }
        public void Explode(int index, List<Enemy> enemies)
        {
            // Implement explosion logic here
            // Create projectile object with 2pi spread and radius of 5
            // Damage is different based on entity type so should create 2 different projectiles
        }
        public List<Enemy> Pull(int index, List<Enemy> enemies)
        {
            Vector2 pullPosition = enemies[index].Position;
            foreach (Enemy enemy in enemies)
            {
                if (Vector2.Distance(enemy.Position, pullPosition) < 5f && enemy != enemies[index])
                {
                    //while (pullPosition) // Keep pulling until the enemy is at the pull position (add logic to pull at set speed)
                    //enemy.Position = pullPosition; // Pull the enemy to the centre of the pull
                }
            }
            return null; // Return null for now, as the pull logic is not fully implemented
        }
    }
}
