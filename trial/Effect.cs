using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.Mime.MediaTypeNames;

namespace trial
{
    public class Effect
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public byte Type { get; set; }

        public void Split(int index, List<Enemy> enemies)
        {
            if (enemies[index].MaxHealth < 4)
            {
                return; // Cannot split if health is too low
            }
            Enemy enemy = enemies[index];
            enemy.ID = enemy.ID + "_split_";
            enemy.Level = (int)Math.Round((decimal)(enemy.Level/4));
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
        }
    }
}
