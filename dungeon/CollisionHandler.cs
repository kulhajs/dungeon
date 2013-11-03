using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dungeon
{
    static class CollisionHandler
    {
        public static bool IsLeftCollision(Rectangle entityRectangle, List<Tile> tiles)
        {
            Rectangle tileRectangle;
            foreach (Tile tile in tiles)
                if (tile.TileType != 1)
                {
                    tileRectangle = new Rectangle((int)tile.X, (int)tile.Y, TileMap.TILE_SIZE, TileMap.TILE_SIZE);
                    if (entityRectangle.Intersects(tileRectangle) && entityRectangle.Left < tileRectangle.Right && entityRectangle.Right > tileRectangle.Right)
                        return true;
                }
            return false;
        }

        public static bool IsRightCollision(Rectangle entityRectangle, List<Tile> tiles)
        {
            Rectangle tileRectangle;
            foreach(Tile tile in tiles)
                if(tile.TileType != 1)
                {
                    tileRectangle = new Rectangle((int)tile.X, (int)tile.Y, TileMap.TILE_SIZE, TileMap.TILE_SIZE);
                    if (entityRectangle.Intersects(tileRectangle) && entityRectangle.Right > tileRectangle.Left && entityRectangle.Left < tileRectangle.Left)
                        return true;
                }
            return false;
        }

        public static bool IsTopCollision(Rectangle entityRectangle, List<Tile> tiles)
        {
            Rectangle tileRectangle;
            foreach(Tile tile in tiles)
                if(tile.TileType != 1)
                {
                    tileRectangle = new Rectangle((int)tile.X, (int)tile.Y, TileMap.TILE_SIZE, TileMap.TILE_SIZE);
                    if (entityRectangle.Intersects(tileRectangle) && entityRectangle.Top < tileRectangle.Bottom && entityRectangle.Bottom > tileRectangle.Bottom)
                        return true;
                }
            return false;
        }

        public static bool IsBottomCollision(Rectangle entityRectangle, List<Tile> tiles)
        {
            Rectangle tileRectangle;
            foreach(Tile tile in tiles)
                if(tile.TileType != 1)
                {
                    tileRectangle = new Rectangle((int)tile.X, (int)tile.Y, 32, 32);
                    if (entityRectangle.Intersects(tileRectangle) && entityRectangle.Bottom > tileRectangle.Top && entityRectangle.Top < tileRectangle.Top)
                        return true;
                }
            return false;
        }
    }
}
