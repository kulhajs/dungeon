using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dungeon
{
    class TileMap
    {
        private int[,] map;

        public int Width { get; private set; }

        public int Height { get; private set; }

        public TileMap(int seed, int width, int height)
        {
            this.Width = width;
            this.Height = height;

            SeedGenerator.Seed = seed;
            map = new int[Width, Height];

        }

        public void Generate(int smoothness)
        {
            for (int i = 0; i < this.Width; i++)
                for (int j = 0; j < this.Height; j++)
                    map[i, j] = SeedGenerator.Random(0, 2);


            for(int i = 0; i < smoothness; i++)
            {
                int[,] new_map = new int[this.Width, this.Height];

                for(int j = 0; j < this.Width; j++)
                    for(int k = 0; k < this.Height; k++)
                    {
                        Range x_range = new Range() { Low = Math.Max(0, j - 1), High = Math.Min(Width - 1, j + 1) };
                        Range y_range = new Range() { Low = Math.Max(0, k - 1), High = Math.Min(Height - 1, k + 1) };

                        int wallCount = 0;
                        for (int a = x_range.Low; a < x_range.High; a++)
                            for (int b = y_range.Low; b < y_range.High; b++)
                            {
                                if ((a == j) || (b == k)) continue;

                                wallCount += 1 - map[a, b];
                            }

                        if (((map[j, k] == 0) && (wallCount >= 4)) ||
                            ((map[j, k] == 1) && (wallCount >= 5)) ||
                            ((j == 0) || (k == 0) || (j == Width - 1) || (k == Height - 1)))
                        {
                            new_map[j, k] = 0;
                        }
                        else
                        {
                            new_map[j, k] = 1;
                        }
                    }

                map = new_map;
            }
        }

    }

    class Range
    {
        public int Low { get; set; }

        public int High { get; set; }
    }
}
