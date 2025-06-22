using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace trial
{
    public enum GameState
    {
        MainMenu,
        AdminMenu,
        PlayingMenu,
        Playing
    }

    public class GameSettings
    {
        public int ScreenWidth;// Default screen width
        public int ScreenHeight; // Default screen height
        public bool Fullscreen; // Default fullscreen mode
        public bool AdminMode; // Set to true for access to admin features
        public float Scale; // Scale factor for the game, used for rendering
        public int MapSize; // Default map size
        public int NumOctaves; // Number of octaves for noise generation
        public double Persistence; // Persistence for noise generation
        public double Lacunarity; // Lacunarity for noise generation
        public GameSettings(int screenWidth, int screenHeight , bool fullscreen, float scale, int mapSize, int numOctaves, double persistence, double lacunarity)
        {
            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;
            Fullscreen = fullscreen;
            Scale = scale; 
            MapSize = mapSize;
            NumOctaves = numOctaves;
            Persistence = persistence;
            Lacunarity = lacunarity;
            AdminMode = false; // Default to false, can be set to true for admin features
        }
    }

    partial class Game1 : Game
    {
        #region Game Components

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private GameSettings MainSetup;
        private WorldHeightmapGenerator World1;

        private Texture2D _mapTexture;

        private Player player;
        private Vector2 _cameraPosition;

        private GameState _gameState = GameState.MainMenu;
        private SpriteFont titleFont;
        private SpriteFont StandardTextFont;

        #endregion
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;

            MainSetup = new GameSettings(800, 600, false, 2.0f, 512, 32, 0.5, 2);
            World1 = new WorldHeightmapGenerator(MainSetup);
        }
    }
}