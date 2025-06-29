using System;
namespace trial
{
    public class WorldHeightmapGenerator
    {
        public int MapSize;
        private int[] _p; // Permutation array for Perlin noise
        private Random _random;
        public byte[,] WorldMap;  //0 = water, 1 = flatland, 2 = mountain, 3 = path. Other numbers refer to specific decorations not yet implemented

        public WorldHeightmapGenerator(GameSettings MainSetup)
        {
            this.MapSize = MainSetup.MapSize; // Default map size, can be adjusted
            _random = new Random();
            InitializePermutations();
        }

        private void InitializePermutations()
        {
            _p = new int[2 * MapSize]; //Double the width of the map
            int[] permutation = new int[MapSize];
            for (int i = 0; i < MapSize; i++)
            {
                permutation[i] = i;
            }

            // Shuffles
            for (int i = 0; i < MapSize; i++)
            {
                int swapIndex = _random.Next(MapSize);
                int temp = permutation[i];
                permutation[i] = permutation[swapIndex];
                permutation[swapIndex] = temp;
            }

            // Duplicates
            for (int i = 0; i < MapSize; i++)
            {
                _p[i] = permutation[i];
                _p[i + MapSize] = permutation[i];
            }
        }

        private double Fade(double t)
        {
            return t * t * t * (t * (t * 6 - 15) + 10);
        }

        private double Lerp(double t, double a, double b)
        {
            return a + t * (b - a);
        }

        private double Grad(int hash, double x, double y, double z)
        {
            int h = hash & 15;
            double u = h < 8 ? x : y;
            double v = h < 4 ? y : h == 12 || h == 14 ? x : z;
            return ((h & 1) == 0 ? u : -u) + ((h & 2) == 0 ? v : -v);
        }

        // PerlinNoise function (2D for heightmap)
        // Input x, y should typically be in a [0, 1] range for scaling,
        // but the implementation handles internal scaling based on the "grid".
        internal double PerlinNoise(double x, double y)
        {
            int X = (int)Math.Floor(x) & (MapSize - 1);
            int Y = (int)Math.Floor(y) & (MapSize - 1);

            x -= Math.Floor(x);
            y -= Math.Floor(y);

            double u = Fade(x);
            double v = Fade(y);

            int A = _p[X] + Y;
            int AA = _p[A];
            int AB = _p[A + 1];
            int B = _p[X + 1] + Y;
            int BA = _p[B];
            int BB = _p[B + 1];

            // Smoothly interpolate between the 8 corners (simplified to 4 for 2D)
            return Lerp(v, Lerp(u, Grad(_p[AA], x, y, 0),
                                   Grad(_p[BA], x - 1, y, 0)),
                           Lerp(u, Grad(_p[AB], x, y - 1, 0),
                                   Grad(_p[BB], x - 1, y - 1, 0)));

        }

        internal double[,] GenerateWorldHeightmap(int numOctaves, double persistence, double lacunarity)
        {
            double[,] heightmap = new double[MapSize, MapSize];

            // Add random offsets for more variety
            double offsetX = _random.NextDouble() * 10000;
            double offsetY = _random.NextDouble() * 10000;

            for (int x = 0; x < MapSize / 2; x++)
            {
                for (int y = 0; y < MapSize / 2; y++)
                {
                    double baseNoiseValue = 0;
                    double amplitude = 1.0;
                    double frequency = 1.0;

                    for (int i = 0; i < numOctaves; i++)
                    {
                        double sampleX = ((double)x / (MapSize / 2) * frequency) + offsetX;
                        double sampleY = ((double)y / (MapSize / 2) * frequency) + offsetY;

                        baseNoiseValue += PerlinNoise(sampleX, sampleY) * amplitude;

                        amplitude *= persistence;
                        frequency *= lacunarity;
                    }

                    heightmap[x, y] = baseNoiseValue;
                }
            }

            double minVal = FindMin(heightmap);
            double maxVal = FindMax(heightmap);

            if (Math.Abs(maxVal - minVal) < 0.001)
            {
                for (int x = 0; x < MapSize / 2; x++)
                {
                    for (int y = 0; y < MapSize / 2; y++)
                    {
                        heightmap[x, y] = 0.5;
                    }
                }
            }
            else
            {
                for (int x = 0; x < MapSize / 2; x++)
                {
                    for (int y = 0; y < MapSize / 2; y++)
                    {
                        heightmap[x, y] = (heightmap[x, y] - minVal) / (maxVal - minVal);
                    }
                }
            }
            return heightmap;
        }

        // Helper to find the minimum value in the heightmap
        private double FindMin(double[,] heightmap)
        {
            double min = double.MaxValue;
            foreach (double val in heightmap)
            {
                if (val < min)
                    min = val;
            }
            return min;
        }

        // Helper to find the maximum value in the heightmap
        private double FindMax(double[,] heightmap)
        {
            double max = double.MinValue;
            foreach (double val in heightmap)
            {
                if (val > max)
                    max = val;
            }
            return max;
        }

        public void GenerateMap(int numOctaves, double persistence, double lacunarity)
        {
            double[,] heightmap = GenerateWorldHeightmap(numOctaves, persistence, lacunarity);
            WorldMap = new byte[MapSize / 2, MapSize / 2];
            for (int x = 0; x < MapSize / 2; x++)
            {
                for (int y = 0; y < MapSize / 2; y++)
                {
                    // Classify the heightmap values into different terrain types
                    if (heightmap[x, y] < 0.2)
                    {
                        WorldMap[x, y] = 0; // Water
                    }
                    else if (heightmap[x, y] > 0.8)
                    {
                        WorldMap[x, y] = 2; // Mountain
                    }
                    else
                    {
                        WorldMap[x, y] = 1; // Flatland
                    }
                }
            }
        }
    }
}


