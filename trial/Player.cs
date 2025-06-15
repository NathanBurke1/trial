using Microsoft.Xna.Framework;
using System.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trial
{
    public class Player
    {
        public Vector2 Position { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public int Level { get; set; }
        public int MaxHealth { get; set; }
        public double Health { get; set; }
    }
}


