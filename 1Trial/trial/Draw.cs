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
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            DisplayManager( gameTime, scale);

            _spriteBatch.End();
            base.Draw(gameTime);
            return;
        }

        private void DisplayManager(GameTime gameTime, float scale)
        {
            if (_gameState == GameState.MainMenu)
            {
                string[] lines = { "GameName", "-- Horde Survival --"," ", "Press 1 to Start new game", "Press 2 to continue game", "Press 3 to load game", "Press Escape to quit" };
                SpriteFont[] fonts = {titleFont};
                DisplayMenus(lines, fonts, gameTime);
            }
            else if (_gameState == GameState.PlayingMenu)
            {
                string[] lines = { "GameName", " ", "Press 1 to select world", "Press 2 to view character", "Press 3 to quit" };
                SpriteFont[] fonts = {titleFont};
                DisplayMenus(lines, fonts, gameTime);
            }
            else if (_gameState == GameState.Playing)
            {
                Vector2 mapDrawPos = -_cameraPosition;
                Vector2 pos = new Vector2(0, 0);

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
                _spriteBatch.DrawString(StandardTextFont, $"{Math.Round(player.Position.X,2)}, {Math.Round(player.Position.Y, 2)}", pos, Color.White);
            }
        }

        private void DisplayMenus(string[] lines, SpriteFont[] fonts, GameTime gameTime)
        {
            SpriteFont[] usedfonts;
            if (fonts.Length != lines.Length)
            {
                usedfonts = new SpriteFont[lines.Length];

                for (int i = 0; i < lines.Length;i++)
                {
                    try
                    {
                        usedfonts[i] = fonts[i];
                    }
                    catch
                    {
                        usedfonts[i] = StandardTextFont;
                    }
                }
            }
            else
            {
                usedfonts = fonts;
            }
            int spacing = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                Vector2 size = usedfonts[i].MeasureString(lines[i]);
                Vector2 pos = new Vector2(
                    (GraphicsDevice.Viewport.Width - size.X) / 2,
                    (spacing)
                );
                spacing += usedfonts[i].LineSpacing;
                _spriteBatch.DrawString(usedfonts[i], lines[i], pos, Color.White);
            }

        }
    }
}