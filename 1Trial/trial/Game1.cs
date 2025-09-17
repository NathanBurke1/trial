using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace trial
{
    public enum GameState
    {
        MainMenu,
        PlayingMenu,
        Playing
    }

    public class GameSettings
    {
        public int ScreenWidth;// Default screen width
        public int ScreenHeight; // Default screen height
        public bool Fullscreen; // Default fullscreen mode
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
        }
    }

    partial class Game1 : Game
    {
        #region Game Components

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private GameSettings MainSetup;
        private MapGenerator World1;

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
            

            MainSetup = new GameSettings(900, 600, false, 4f, 1024, 8, 0.5, 2);
            World1 = new MapGenerator(MainSetup);

            _graphics.PreferredBackBufferWidth = MainSetup.ScreenWidth;
            _graphics.PreferredBackBufferHeight = MainSetup.ScreenHeight;
            _graphics.IsFullScreen = MainSetup.Fullscreen;
            _graphics.ApplyChanges();
        }
    }
}