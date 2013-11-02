using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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

        public List<Tile> Tiles;

        public TileMap(int seed, int width, int height)
        {
            this.Width = width;
            this.Height = height;

            Tiles = new List<Tile>();

            SeedGenerator.Seed = seed;
            map = new int[Width, Height];

        }

        public void Regenerate(int smoothness, ContentManager theContentManager)
        {
            this.Generate(smoothness);
            this.Initialize();
            this.LoadContent(theContentManager);
        }

        public void Generate(int smoothness)
        {
            for (int i = 0; i < this.Width; i++)
                for (int j = 0; j < this.Height; j++)
                    map[i, j] = SeedGenerator.Random(2);


            for(int i = 0; i < smoothness; i++)
            {
                int[,] new_map = new int[this.Width, this.Height];

                for(int j = 0; j < this.Width; j++)
                    for(int k = 0; k < this.Height; k++)
                    {
                        Range x_range = new Range() { Low = Math.Max(0, j - 1), High = Math.Min(Width - 1, j + 1) };
                        Range y_range = new Range() { Low = Math.Max(0, k - 1), High = Math.Min(Height - 1, k + 1) };

                        int wallCount = 0;
                        for (int a = x_range.Low; a <= x_range.High; a++)
                            for (int b = y_range.Low; b <= y_range.High; b++)
                            {
                                if ((a == j) && (b == k)) continue;

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

            for (int i = 0; i < Width; i++)
                for(int j = 0; j < Height; j++)
                {
                    int cell = map[i, j];
                    if(cell == 0)
                    {
                        if (((i > 0 && (map[i - 1, j] == 1)) ||
                            ((j > 0) && map[i, j - 1] == 1)) ||
                            ((i < Width - 1) && (map[i + 1, j] == 1)) ||
                            ((j < Height - 1) && (map[i, j + 1] == 1)))
                            map[i, j] = 2;
                    }
                }
        }

        public void Initialize()
        {
            if (Tiles.Count > 0)
                Tiles.Clear();

            for(int i = 0; i < Width; i++)
                for(int j = 0; j < Height; j++)
                {
                    Tiles.Add(new Tile(new Vector2(i * 32 , j * 32), map[i, j]));
                }
        }

        public void LoadContent(ContentManager theContentManager)
        {
            foreach(Tile tile in Tiles)
            {
                tile.LoadContent(theContentManager);
            }
        }

        public void DrawTiles(SpriteBatch theSpriteBatch)
        {
            foreach(Tile tile in Tiles)
            {
                tile.Draw(theSpriteBatch);
            }
        }

    }

    class Range
    {
        public int Low { get; set; }

        public int High { get; set; }
    }
}
