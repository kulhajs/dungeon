using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dungeon
{
    class Tile : Sprite
    {
        public Tile ParentTile { get; set; }

        public int G { get; set; }

        public int H { get; set; }

        public int F { get; set; }

        public int TileType { get; private set; }

        public bool VisitedEh { get; set; }

        public Rectangle TileRectangle { get { return new Rectangle((int)this.X, (int)this.Y, TileMap.TILE_SIZE, TileMap.TILE_SIZE); } }

        public Tile(Vector2 position, int tileType)
        {
            this.Position = position;
            this.TileType = tileType;
            this.Color = Color.Black;
            this.VisitedEh = false;
        }
        public void LoadContent(ContentManager theContentManager)
        {
            base.LoadContent(theContentManager, "tile" + TileType.ToString());
        }

        public void Draw(SpriteBatch theSpriteBatch)
        {
            base.Draw(theSpriteBatch, Vector2.Zero, this.Position, this.Color, 0.0f);
        }

        public float DistanceTo(Vector2 point)
        {
            return (point - this.Position).Length();
        }

        public bool IsNextTo(Tile tile)
        {
            if (((this.Position.X + TileMap.TILE_SIZE == tile.Position.X) || (this.Position.X - TileMap.TILE_SIZE == tile.Position.X) || (this.Position.X == tile.Position.X))
                && ((this.Position.Y + TileMap.TILE_SIZE == tile.Position.Y) || (this.Position.Y - TileMap.TILE_SIZE == tile.Position.Y) || (this.Position.Y == tile.Position.Y)))
                return true;

            return false;
        }
    }
}
