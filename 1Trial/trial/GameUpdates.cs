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
            if (kstate.IsKeyDown(Keys.Left)) move.X -= player.Speed;
            if (kstate.IsKeyDown(Keys.Right)) move.X += player.Speed;
            if (kstate.IsKeyDown(Keys.Up)) move.Y -= player.Speed;
            if (kstate.IsKeyDown(Keys.Down)) move.Y += player.Speed;
            #endregion

            if (move != Vector2.Zero)
            {
                Vector2 newPosition = player.Position + move;
                newPosition = Vector2.Clamp(newPosition, Vector2.Zero, new Vector2((World1.MapSize / 2) - 1, (World1.MapSize / 2) - 1));

                // Try full move first
                bool canMoveFull =
                    World1.WorldMap[(int)Math.Floor(newPosition.X), (int)Math.Floor(newPosition.Y)] != 0 &&
                    World1.WorldMap[(int)Math.Floor(newPosition.X), (int)Math.Floor(newPosition.Y)] != 2 &&
                    World1.WorldMap[(int)Math.Ceiling(newPosition.X), (int)Math.Ceiling(newPosition.Y)] != 0 &&
                    World1.WorldMap[(int)Math.Ceiling(newPosition.X), (int)Math.Ceiling(newPosition.Y)] != 2;

                if (canMoveFull)
                {
                    player.Position = newPosition;
                }
                else
                {
                    // Try X only
                    Vector2 xMove = new Vector2(newPosition.X, player.Position.Y);
                    bool canMoveX =
                        World1.WorldMap[(int)Math.Floor(xMove.X), (int)Math.Floor(xMove.Y)] != 0 &&
                        World1.WorldMap[(int)Math.Floor(xMove.X), (int)Math.Floor(xMove.Y)] != 2 &&
                        World1.WorldMap[(int)Math.Ceiling(xMove.X), (int)Math.Ceiling(xMove.Y)] != 0 &&
                        World1.WorldMap[(int)Math.Ceiling(xMove.X), (int)Math.Ceiling(xMove.Y)] != 2;

                    if (canMoveX)
                    {
                        player.Position = xMove;
                    }
                    else
                    {
                        // Try Y only
                        Vector2 yMove = new Vector2(player.Position.X, newPosition.Y);
                        bool canMoveY =
                            World1.WorldMap[(int)Math.Floor(yMove.X), (int)Math.Floor(yMove.Y)] != 0 &&
                            World1.WorldMap[(int)Math.Floor(yMove.X), (int)Math.Floor(yMove.Y)] != 2 &&
                            World1.WorldMap[(int)Math.Ceiling(yMove.X), (int)Math.Ceiling(yMove.Y)] != 0 &&
                            World1.WorldMap[(int)Math.Ceiling(yMove.X), (int)Math.Ceiling(yMove.Y)] != 2;

                        if (canMoveY)
                        {
                            player.Position = yMove;
                        }
                    }
                }
            }

            var viewport = GraphicsDevice.Viewport;
            Vector2 screenCenter = new Vector2(viewport.Width, viewport.Height) / 2f;
            Vector2 playerPixelPos = player.Position * MainSetup.Scale;
            _cameraPosition = playerPixelPos - screenCenter;

            base.Update(gameTime);
        }
    }
}

