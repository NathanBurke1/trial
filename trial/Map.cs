using System;

public class WorldHeightmapGenerator
{
    // --- Perlin Noise Implementation (Simplified, based on common examples) ---
    // This is a basic Perlin Noise implementation. For production games/apps,
    // you might want to use a more robust library like FastNoiseLite or Noise.NET.

    private int[] _p; // Permutation array for Perlin noise
    private Random _random;
    public byte[,] WorldMap;  //0 = water, 1 = flatland, 2 = mountain, 3 = path. Other numbers refer to specific decorations not yet implemented

    public WorldHeightmapGenerator(int seed)
    {
        _random = new Random(seed);
        InitializePermutations();
    }

    private void InitializePermutations()
    {
        _p = new int[512]; // Needs to be 512 for convenience (256 doubled)
        int[] permutation = new int[256];
        for (int i = 0; i < 256; i++)
        {
            permutation[i] = i;
        }

        // Shuffle the permutation array
        for (int i = 0; i < 256; i++)
        {
            int swapIndex = _random.Next(256);
            int temp = permutation[i];
            permutation[i] = permutation[swapIndex];
            permutation[swapIndex] = temp;
        }

        // Duplicate the permutation array to avoid modulo operations
        for (int i = 0; i < 256; i++)
        {
            _p[i] = permutation[i];
            _p[i + 256] = permutation[i];
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
        int X = (int)Math.Floor(x) & 255;
        int Y = (int)Math.Floor(y) & 255;

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

    // --- End Perlin Noise Implementation ---


    // Main function to generate the heightmap
    internal double[,] GenerateWorldHeightmap(int width, int height, int numOctaves, double persistence, double lacunarity)
    {
        double[,] heightmap = new double[width, height];

        // Base Noise Generation
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                double baseNoiseValue = 0;
                double amplitude = 1.0;
                double frequency = 1.0; // Starting frequency relative to the map size

                for (int i = 0; i < numOctaves; i++)
                {
                    // Scale x, y for Perlin noise function.
                    // Multiplying by 'frequency' makes the noise "zoom in" or "zoom out".
                    // Dividing by 'width' and 'height' normalizes the coordinates to a [0, 1] range internally for Perlin.
                    double sampleX = (double)x / width * frequency;
                    double sampleY = (double)y / height * frequency;

                    baseNoiseValue += PerlinNoise(sampleX, sampleY) * amplitude;

                    amplitude *= persistence; // Reduces amplitude for higher frequencies (less impact)
                    frequency *= lacunarity;  // Increases frequency for higher detail (more waves)
                }

                heightmap[x, y] = baseNoiseValue;
            }
        }

        // Normalize to 0-1 range
        double minVal = FindMin(heightmap);
        double maxVal = FindMax(heightmap);

        // Avoid division by zero if all values are the same (e.g., flat map)
        if (Math.Abs(maxVal - minVal) < 0.000001) // Small epsilon for float comparison
        {
            // If all values are the same, normalize to 0.5 or 0, whatever makes sense for a flat map
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    heightmap[x, y] = 0.5; // Or 0.0, depending on desired flat map height
                }
            }
        }
        else
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
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

    public void GenerateMap(int width, int height, int numOctaves, double persistence, double lacunarity)
    {
        double[,] heightmap = GenerateWorldHeightmap(width, height, numOctaves, persistence, lacunarity);
        WorldMap = new byte[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Classify the heightmap values into different terrain types
                if (heightmap[x, y] < 0.2)
                {
                    WorldMap[x, y] = 0; // Water
                }
                else if (heightmap[x, y] < 0.5)
                {
                    WorldMap[x, y] = 1; // Flatland
                }
                else if (heightmap[x, y] < 0.8)
                {
                    WorldMap[x, y] = 2; // Mountain
                }
            }
        }
    }
}


