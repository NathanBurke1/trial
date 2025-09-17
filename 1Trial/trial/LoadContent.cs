using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace trial
{
    partial class Game1 : Game
    {
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            titleFont = Content.Load<SpriteFont>("MenuTextTitles");
            StandardTextFont = Content.Load<SpriteFont>("MenuTextStandard");
        }
    }
}