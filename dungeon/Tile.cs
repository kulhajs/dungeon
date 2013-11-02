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
        public int TileType { get; private set; }

        public Tile(Vector2 position, int tileType)
        {
            this.Position = position;
            this.TileType = tileType;
        }

        public void LoadContent(ContentManager theContentManager)
        {
            base.LoadContent(theContentManager, "tile" + TileType.ToString());
        }

        public void Draw(SpriteBatch theSpriteBatch)
        {
            base.Draw(theSpriteBatch, Vector2.Zero, this.Position, Color.White, 0.0f);
        }
    }
}
