using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using static System.Formats.Asn1.AsnWriter;

namespace trial
{
    partial class Game1 : Game
    {
        protected override void Draw(GameTime gameTime)
        {
            float scale = MainSetup.Scale;
            bool AdminMode = MainSetup.AdminMode;
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            DisplayManager(AdminMode, gameTime, scale);

            _spriteBatch.End();
            base.Draw(gameTime);
            return;
        }
        private void DisplayManager(bool AdminMode, GameTime gameTime, float scale)
        {
            if (_gameState == GameState.MainMenu)
            {
                string[] lines = { "Red Oath", "Horde Survival", "Press 1 to Start new game", "Press 2 to continue game", "Press 3 to load game", "Press Escape to quit","","" };
                SpriteFont[] fonts = { titleFont, StandardTextFont, StandardTextFont, StandardTextFont, StandardTextFont, StandardTextFont, StandardTextFont, StandardTextFont};
                if (AdminMode) { lines[8] = "Press F1 for Admin view"; }
                DisplayMenus(lines, fonts, gameTime);
            }
            else if (_gameState == GameState.PlayingMenu)
            {
                string[] lines = { "Red Oath", " ", "Press 1 to select world", "Press 2 to view character", "Press 3 to quit" };
                SpriteFont[] fonts = { titleFont, StandardTextFont, StandardTextFont, StandardTextFont };
                if (AdminMode) { lines[3] = "Press F1 for Admin view"; }
                DisplayMenus(lines, fonts, gameTime);
            }
            else if (_gameState == GameState.AdminMenu)
            {
                string[] lines = { "Red Oath", "Admin Menu", "Press Enter to continue", "Press Escape to return to Main Menu" };
                SpriteFont[] fonts = { titleFont, StandardTextFont, StandardTextFont, StandardTextFont };
                DisplayMenus(lines, fonts, gameTime);
            }
            else if (_gameState == GameState.Playing)
            {
                Vector2 mapDrawPos = -_cameraPosition;

                _spriteBatch.Draw(
                    _mapTexture,
                    new Rectangle((int)mapDrawPos.X, (int)mapDrawPos.Y, (int)((World1.MapSize / 2) * scale), (int)((World1.MapSize / 2) * scale)),
                    Color.White
                );

                Vector2 playerScreenPos = (player.Position * scale) - _cameraPosition;
                _spriteBatch.Draw(
                    _mapTexture,
                    new Rectangle((int)playerScreenPos.X, (int)playerScreenPos.Y, (int)scale, (int)scale),
                    new Rectangle(0, 0, 1, 1),
                    Color.Red
                );
            }
        }
        private void DisplayMenus(string[] lines, SpriteFont[] fonts, GameTime gameTime)
        {
            int spacing = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                Vector2 size = fonts[i].MeasureString(lines[i]);
                Vector2 pos = new Vector2(
                    (GraphicsDevice.Viewport.Width - size.X) / 2,
                    (spacing)
                );
                spacing += (int)Math.Round(fonts[i].LineSpacing * 1.2);
                _spriteBatch.DrawString(fonts[i], lines[i], pos, Color.White);
            }

        }
    }
}