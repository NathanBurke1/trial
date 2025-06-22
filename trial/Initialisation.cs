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

            // Initialize player at center of map (in tile coordinates)
            player = new Player("name");
            player.Position = new Vector2(World1.MapSize / 4f, World1.MapSize / 4f);

            // Create a texture from the generated map
            _mapTexture = new Texture2D(GraphicsDevice, World1.MapSize / 2, World1.MapSize / 2);

            Color[] colorData = new Color[World1.MapSize / 2 * World1.MapSize / 2];
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

        protected override void Initialize()
        {
            // Do not generate map or player here
            base.Initialize();
        }
    }
}

