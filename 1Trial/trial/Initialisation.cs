using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace trial
{
    partial class Game1 : Game
    {
        protected void StartGame()
        {
            // Generate the map data
            World1.GenerateMap(MainSetup.NumOctaves, MainSetup.Persistence, MainSetup.Lacunarity);

            // Find a flatland tile (1) near the center if the center is not flatland
            int mapHalf = World1.MapSize / 4;
            int mapLimit = World1.MapSize / 2;
            Vector2 startPos = new Vector2(mapHalf, mapHalf);

            // Helper to check if a tile is flatland
            bool IsFlatland(int x, int y) =>
                x >= 0 && y >= 0 && x < mapLimit && y < mapLimit && World1.WorldMap[x, y] == 1;

            int foundX = (int)startPos.X;
            int foundY = (int)startPos.Y;

            if (!IsFlatland(foundX, foundY))
            {
                // Spiral search outwards from center
                int[] dx = { 1, 0, -1, 0 };
                int[] dy = { 0, 1, 0, -1 };
                int step = 1;
                int x = foundX, y = foundY;
                bool found = false;

                while (step < mapLimit && !found)
                {
                    for (int dir = 0; dir < 4 && !found; dir++)
                    {
                        for (int i = 0; i < step; i++)
                        {
                            x += dx[dir];
                            y += dy[dir];
                            if (IsFlatland(x, y))
                            {
                                foundX = x;
                                foundY = y;
                                found = true;
                                break;
                            }
                        }
                        // Increase step after two directions (right and down)
                        if (dir == 1 || dir == 3) step++;
                    }
                }
            }

            player = new Player();
            player.Position = new Vector2(foundX, foundY);

            // Create a texture from the generated map
            _mapTexture = new Texture2D(GraphicsDevice, World1.MapSize / 2, World1.MapSize / 2);

            Color[] colorData = new Color[(World1.MapSize * World1.MapSize) / 4];
            for (int y = 0; y < World1.MapSize / 2; y++)
            {
                for (int x = 0; x < World1.MapSize / 2; x++)
                {
                    byte tile = World1.WorldMap[x, y];
                    Color c = tile switch
                    {
                        0 => Color.Blue,        // Water
                        1 => Color.ForestGreen, // Flatland
                        2 => Color.Gray,        // Mountain
                        3 => Color.SandyBrown,  // Path (not generated yet)
                        _ => Color.Black        // Unknown/decoration
                    };
                    colorData[y * (World1.MapSize / 2) + x] = c;
                }
            }
            _mapTexture.SetData(colorData);
        }

        protected override void Initialise()
        {
            // Leave like this
            base.Initialize();
        }
    }
}

