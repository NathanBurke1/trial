using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;


namespace trial
{
    partial class Game1 : Game
    {
        protected override void Update(GameTime gameTime)
        {
            var kstate = Keyboard.GetState();

            // Always allow exit with gamepad back or Alt+F4
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            if (_gameState == GameState.MainMenu)
            {
                if (kstate.IsKeyDown(Keys.Enter))
                {
                    StartGame();
                    _gameState = GameState.Playing;
                }
                base.Update(gameTime);
                return;
            }

            // If in Playing state, allow ESC to return to menu
            if (_gameState == GameState.Playing && kstate.IsKeyDown(Keys.Escape))
            {
                _gameState = GameState.MainMenu;
                base.Update(gameTime);
                return;
            }

            #region Gamepad Input
            Vector2 move = Vector2.Zero;
            if (kstate.IsKeyDown(Keys.Left)) move.X -= 10;
            if (kstate.IsKeyDown(Keys.Right)) move.X += 10;
            if (kstate.IsKeyDown(Keys.Up)) move.Y -= 10;
            if (kstate.IsKeyDown(Keys.Down)) move.Y += 10;
            #endregion

            if (move != Vector2.Zero)
            {
                player.Position += Vector2.Normalize(move);
                player.Position = Vector2.Clamp(player.Position, Vector2.Zero, new Vector2((World1.MapSize / 2) - 1, (World1.MapSize / 2) - 1));
            }

            var viewport = GraphicsDevice.Viewport;
            Vector2 screenCenter = new Vector2(viewport.Width, viewport.Height) / 2f;
            Vector2 playerPixelPos = player.Position * MainSetup.Scale;
            _cameraPosition = playerPixelPos - screenCenter;

            base.Update(gameTime);
        }
    }
}