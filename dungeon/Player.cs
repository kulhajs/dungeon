﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace dungeon
{
    enum MovingDirection
    {
        Left = 0, 
        LeftUp = 4,
        Up = 8,
        RightUp = 12,
        Right = 16, 
        RightDown = 20,
        Down = 24,
        LeftDown = 28
    };

    class Player : Sprite
    {
        const string ASSETNAME = "ogre";

        const int TEXTURE_SIZE = 64;

        int currentFrame = 0;

        int frameTime = 6;

        const float VELOCITY = 60f;

        public Vector2 Direction = Vector2.Zero;

        public Rectangle PlayerRectangle { get { return new Rectangle((int)this.X + 2, (int)this.Y + 48, 28, 14); } }

        public Rectangle PlayerRectangle2 { get { return new Rectangle((int)this.X + 30, (int)this.Y + 62, 1, 1); } }

        MovingDirection currentMovingDirection = MovingDirection.Down;

        MovingDirection oldMovingDirection;

        List<Tile> path = new List<Tile>();

        private Rectangle[] sources = new Rectangle[] {
            //MOVE DOWN
            new Rectangle(0, 0, TEXTURE_SIZE, TEXTURE_SIZE),
            new Rectangle(TEXTURE_SIZE, 0, TEXTURE_SIZE, TEXTURE_SIZE),
            new Rectangle(TEXTURE_SIZE * 2, 0, TEXTURE_SIZE, TEXTURE_SIZE),
            new Rectangle(TEXTURE_SIZE * 3, 0, TEXTURE_SIZE, TEXTURE_SIZE), 
            //MOVE UP
            new Rectangle(0, TEXTURE_SIZE, TEXTURE_SIZE, TEXTURE_SIZE),
            new Rectangle(TEXTURE_SIZE, TEXTURE_SIZE, TEXTURE_SIZE, TEXTURE_SIZE),
            new Rectangle(TEXTURE_SIZE * 2, TEXTURE_SIZE, TEXTURE_SIZE, TEXTURE_SIZE),
            new Rectangle(TEXTURE_SIZE * 3, TEXTURE_SIZE, TEXTURE_SIZE, TEXTURE_SIZE),
            //MOVE LEFT
            new Rectangle(0, TEXTURE_SIZE * 2, TEXTURE_SIZE, TEXTURE_SIZE),
            new Rectangle(TEXTURE_SIZE, TEXTURE_SIZE * 2, TEXTURE_SIZE, TEXTURE_SIZE),
            new Rectangle(TEXTURE_SIZE * 2, TEXTURE_SIZE * 2, TEXTURE_SIZE, TEXTURE_SIZE),
            new Rectangle(TEXTURE_SIZE * 3, TEXTURE_SIZE * 2, TEXTURE_SIZE, TEXTURE_SIZE),
            //MOVE RIGHT
            new Rectangle(0, TEXTURE_SIZE * 3, TEXTURE_SIZE, TEXTURE_SIZE),
            new Rectangle(TEXTURE_SIZE, TEXTURE_SIZE * 3, TEXTURE_SIZE, TEXTURE_SIZE),
            new Rectangle(TEXTURE_SIZE * 2, TEXTURE_SIZE * 3, TEXTURE_SIZE, TEXTURE_SIZE),
            new Rectangle(TEXTURE_SIZE * 3, TEXTURE_SIZE * 3, TEXTURE_SIZE, TEXTURE_SIZE),
            //MOVE DOWN
            new Rectangle(0, TEXTURE_SIZE * 4, TEXTURE_SIZE, TEXTURE_SIZE),
            new Rectangle(TEXTURE_SIZE, TEXTURE_SIZE * 4, TEXTURE_SIZE, TEXTURE_SIZE),
            new Rectangle(TEXTURE_SIZE * 2, TEXTURE_SIZE * 4, TEXTURE_SIZE, TEXTURE_SIZE),
            new Rectangle(TEXTURE_SIZE * 3, TEXTURE_SIZE * 4, TEXTURE_SIZE, TEXTURE_SIZE),
            //MOVE UP
            new Rectangle(0, TEXTURE_SIZE * 5, TEXTURE_SIZE, TEXTURE_SIZE),
            new Rectangle(TEXTURE_SIZE, TEXTURE_SIZE * 5, TEXTURE_SIZE, TEXTURE_SIZE),
            new Rectangle(TEXTURE_SIZE * 2, TEXTURE_SIZE * 5, TEXTURE_SIZE, TEXTURE_SIZE),
            new Rectangle(TEXTURE_SIZE * 3, TEXTURE_SIZE * 5, TEXTURE_SIZE, TEXTURE_SIZE),
            //MOVE LEFT
            new Rectangle(0, TEXTURE_SIZE * 6, TEXTURE_SIZE, TEXTURE_SIZE),
            new Rectangle(TEXTURE_SIZE, TEXTURE_SIZE * 6, TEXTURE_SIZE, TEXTURE_SIZE),
            new Rectangle(TEXTURE_SIZE * 2, TEXTURE_SIZE * 6, TEXTURE_SIZE, TEXTURE_SIZE),
            new Rectangle(TEXTURE_SIZE * 3, TEXTURE_SIZE * 6, TEXTURE_SIZE, TEXTURE_SIZE),
            //MOVE RIGHT
            new Rectangle(0, TEXTURE_SIZE * 7, TEXTURE_SIZE, TEXTURE_SIZE),
            new Rectangle(TEXTURE_SIZE, TEXTURE_SIZE * 7, TEXTURE_SIZE, TEXTURE_SIZE),
            new Rectangle(TEXTURE_SIZE * 2, TEXTURE_SIZE * 7, TEXTURE_SIZE, TEXTURE_SIZE),
            new Rectangle(TEXTURE_SIZE * 3, TEXTURE_SIZE * 7, TEXTURE_SIZE, TEXTURE_SIZE),
        };

        public Player(Vector2 position)
        {
            this.Position = position;
            this.Color = Color.White;
        }

        public void LoadContent(ContentManager theContentManager)
        {
            base.LoadContent(theContentManager, ASSETNAME);
            this.Source = sources[0];
        }

        public void Update(GameTime theGameTime, KeyboardState currentKeyboardState, KeyboardState oldKeyboardState, 
                           List<Tile> tiles, MouseState mouseState, MouseState oldMouseState, Camera camera)
        {
            Vector2 destination = Vector2.Zero;

            if(mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton != ButtonState.Pressed)
            {
                 destination = new Vector2(mouseState.X + camera.Origin.X, mouseState.Y + camera.Origin.Y);
            }

            if (destination != Vector2.Zero)
            {
                Tile start = tiles.Where(tile => tile.TileRectangle.Intersects(PlayerRectangle2)).ToList()[0];
                Tile des = tiles.Where(tile => tile.TileRectangle.Intersects(new Rectangle((int)destination.X, (int)destination.Y, 2, 2))).ToList()[0];

                if (des.TileType != 1) return;

                path = PathFinder.FindPath(start, des, tiles.Where(tile => tile.TileType == 1).ToList());
            }

            if(path.Count > 0)
            {
                UpdateMovement(path, theGameTime);
            }

            oldMovingDirection = currentMovingDirection;
        }

        private void UpdateMovement(List<Tile> path, GameTime theGameTime)
        {
            Direction = Vector2.Zero;

            if(PlayerRectangle2.Intersects(path[0].TileRectangle))
            {
                path.RemoveAt(0);
            }
  
            if(path.Count > 0)
            {
                UpdateDirection();

                this.Position += Direction * VELOCITY * (float)theGameTime.ElapsedGameTime.TotalSeconds;

                this.Animate();
            }

        }

        private void UpdateDirection()
        {
            Direction = new Vector2(path[0].X + TileMap.TILE_SIZE / 2, path[0].Y + TileMap.TILE_SIZE / 2) - new Vector2(PlayerRectangle2.X, PlayerRectangle2.Y);
            Direction.Normalize();

            if (FAbs(Direction.X) - FAbs(Direction.Y) > 0.25f)
            {
                if (Direction.X < 0)
                    currentMovingDirection = MovingDirection.Left;
                else
                    currentMovingDirection = MovingDirection.Right;
            }
            else if (FAbs(Direction.X) - FAbs(Direction.Y) < -0.25f)
            {
                if (Direction.Y < 0)
                    currentMovingDirection = MovingDirection.Up;
                else
                    currentMovingDirection = MovingDirection.Down;
            }
            else
            {
                if (Direction.X < 0 && Direction.Y < 0) currentMovingDirection = MovingDirection.LeftUp;
                else if (Direction.X > 0 && Direction.Y < 0) currentMovingDirection = MovingDirection.RightUp;
                else if (Direction.X < 0 && Direction.Y > 0) currentMovingDirection = MovingDirection.LeftDown;
                else if (Direction.X > 0 && Direction.Y > 0) currentMovingDirection = MovingDirection.RightDown;
            }
        }

        private void Animate()
        {
            if(oldMovingDirection != currentMovingDirection)
            {
                currentFrame = 0;
            }

            this.Source = sources[(int)currentMovingDirection + currentFrame];

            if(frameTime < 0)
            {
                if (currentFrame < 3)
                    currentFrame++;
                else
                    currentFrame = 0;

                frameTime = 6;
            }

            frameTime--;
        }

        public void Draw(SpriteBatch theSpriteBatch)
        {
            base.Draw(theSpriteBatch, Vector2.Zero, this.Position, this.Color, 0.0f);
        }
    }
}
