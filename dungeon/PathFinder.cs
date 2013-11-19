using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dungeon
{
    class PathFinder
    {
        const int HORIZONTAL_COST = 10;
        const int DIAGONAL_COST = 14;

        private static int Heuristic(Tile tile, Tile destinationTile)
        {
            Vector2 tilePosition = tile.Position;

            int steps = 0;

            if(tilePosition.X < destinationTile.X)
            {
                while(tilePosition.X < destinationTile.X)
                {
                    tilePosition.X += TileMap.TILE_SIZE;
                    steps++;
                }
            }
            else if(tilePosition.X > destinationTile.X)
            {
                while(tilePosition.X > destinationTile.X)
                {
                    tilePosition.X -= TileMap.TILE_SIZE;
                    steps++;
                }
            }


            if (tilePosition.Y < destinationTile.Y)
            {
                while (tilePosition.Y < destinationTile.Y)
                {
                    tilePosition.Y += TileMap.TILE_SIZE;
                    steps++;
                }
            }
            else if (tilePosition.Y > destinationTile.Y)
            {
                while (tilePosition.Y > destinationTile.Y)
                {
                    tilePosition.Y -= TileMap.TILE_SIZE;
                    steps++;
                }
            }

            return steps * 10;
        }

        public static List<Tile> FindPath(Tile start, Tile destination, List<Tile> tileMap)
        {
            List<Tile> walkableTiles = tileMap;
            List<Tile> neighbours = new List<Tile>();
            Tile currentTile;

            List<Tile> openTiles = new List<Tile>();
            List<Tile> closedTiles = new List<Tile>();

            start.G = 0;
            openTiles.Add(start);

            while(openTiles.Count > 0)
            {
                openTiles = openTiles.OrderBy(tile => tile.F).ToList();
                currentTile = openTiles[0];

                closedTiles.Add(currentTile);
                openTiles.Remove(currentTile);

                if(currentTile.IsNextTo(destination))
                {
                    destination.ParentTile = currentTile;
                    return GetPath(destination, start, destination);
                }

                neighbours = walkableTiles.Where(tile => tile.IsNextTo(currentTile) && !closedTiles.Contains(tile)).ToList();
                foreach(Tile tile in neighbours)
                {
                    if(openTiles.Contains(tile))
                    {
                        int g = currentTile.G + tile.X == currentTile.X || tile.Y == currentTile.Y ? HORIZONTAL_COST : DIAGONAL_COST;
                        if (g < tile.G) tile.ParentTile = currentTile;
                        else continue;
                    }
                    tile.H = Heuristic(tile, destination);
                    tile.G = currentTile.G + tile.X == currentTile.X || tile.Y == currentTile.Y ? HORIZONTAL_COST : DIAGONAL_COST;
                    tile.F = tile.G + tile.H;

                    tile.ParentTile = currentTile;

                    openTiles.Add(tile);
                }
            }

            return new List<Tile>();
        }

        private static List<Tile> GetPath (Tile currentTile, Tile start, Tile destination)
        {
            List<Tile> path = new List<Tile>();
            path.Add(currentTile);

            while(currentTile.Position != start.Position)
            {
                currentTile.Color = Color.Red;
                path.Add(currentTile.ParentTile);
                currentTile = currentTile.ParentTile;
            }

            path.Reverse();
            path.Add(destination);

            return path;
        }
    }
}
