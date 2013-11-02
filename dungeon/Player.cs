using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dungeon
{
    enum MovingDirection
    {
        Down = 0, 
        Up = 5,
        Right = 10,
        Left = 15
    };

    class Player : Sprite
    {
        const string ASSETNAME = "darksoldiersheetupdate_0";

        const int TEXTURE_SIZE = 64;

        int currentFrame = 0;

        int frameTime = 8;

        const float VELOCITY = 100f;

        public Vector2 Direction = Vector2.Zero;

        Vector2 MOVE_DOWN = new Vector2(0, 1);

        Vector2 MOVE_UP = new Vector2(0, -1);

        Vector2 MOVE_LEFT = new Vector2(-1, 0);

        Vector2 MOVE_RIGHT = new Vector2(1, 0);

        MovingDirection currentMovingDirection = MovingDirection.Down;

        MovingDirection oldMovingDirection;

        private Rectangle[] sources = new Rectangle[] {
            //MOVE DOWN
            new Rectangle(0, 0, TEXTURE_SIZE, TEXTURE_SIZE),
            new Rectangle(TEXTURE_SIZE, 0, TEXTURE_SIZE, TEXTURE_SIZE),
            new Rectangle(TEXTURE_SIZE * 2, 0, TEXTURE_SIZE, TEXTURE_SIZE),
            new Rectangle(TEXTURE_SIZE * 3, 0, TEXTURE_SIZE, TEXTURE_SIZE), 
            new Rectangle(TEXTURE_SIZE * 4, 0, TEXTURE_SIZE, TEXTURE_SIZE),
            //MOVE UP
            new Rectangle(0, TEXTURE_SIZE, TEXTURE_SIZE, TEXTURE_SIZE),
            new Rectangle(TEXTURE_SIZE, TEXTURE_SIZE, TEXTURE_SIZE, TEXTURE_SIZE),
            new Rectangle(TEXTURE_SIZE * 2, TEXTURE_SIZE, TEXTURE_SIZE, TEXTURE_SIZE),
            new Rectangle(TEXTURE_SIZE * 3, TEXTURE_SIZE, TEXTURE_SIZE, TEXTURE_SIZE),
            new Rectangle(TEXTURE_SIZE * 4, TEXTURE_SIZE, TEXTURE_SIZE, TEXTURE_SIZE),
            //MOVE LEFT
            new Rectangle(0, TEXTURE_SIZE * 2, TEXTURE_SIZE, TEXTURE_SIZE),
            new Rectangle(TEXTURE_SIZE, TEXTURE_SIZE * 2, TEXTURE_SIZE, TEXTURE_SIZE),
            new Rectangle(TEXTURE_SIZE * 2, TEXTURE_SIZE * 2, TEXTURE_SIZE, TEXTURE_SIZE),
            new Rectangle(TEXTURE_SIZE * 3, TEXTURE_SIZE * 2, TEXTURE_SIZE, TEXTURE_SIZE),
            new Rectangle(TEXTURE_SIZE * 4, TEXTURE_SIZE * 2, TEXTURE_SIZE, TEXTURE_SIZE),
            //MOVE RIGHT
            new Rectangle(0, TEXTURE_SIZE * 3, TEXTURE_SIZE, TEXTURE_SIZE),
            new Rectangle(TEXTURE_SIZE, TEXTURE_SIZE * 3, TEXTURE_SIZE, TEXTURE_SIZE),
            new Rectangle(TEXTURE_SIZE * 2, TEXTURE_SIZE * 3, TEXTURE_SIZE, TEXTURE_SIZE),
            new Rectangle(TEXTURE_SIZE * 3, TEXTURE_SIZE * 3, TEXTURE_SIZE, TEXTURE_SIZE),
            new Rectangle(TEXTURE_SIZE * 4, TEXTURE_SIZE * 3, TEXTURE_SIZE, TEXTURE_SIZE)
        };

        public Player(Vector2 position)
        {
            this.Position = position;
        }

        public void LoadContent(ContentManager theContentManager)
        {
            base.LoadContent(theContentManager, ASSETNAME);
            this.Source = sources[0];
        }

        public void Update(GameTime theGameTime, KeyboardState currentKeyboardState, KeyboardState oldKeyboardState)
        {
            this.Direction = Vector2.Zero;

            if (currentKeyboardState.IsKeyDown(Keys.Down))
            {
                this.Direction = MOVE_DOWN;
                currentMovingDirection = MovingDirection.Down;
            }
            else if (currentKeyboardState.IsKeyDown(Keys.Up))
            {
                this.Direction = MOVE_UP;
                currentMovingDirection = MovingDirection.Up;
            }

            else if (currentKeyboardState.IsKeyDown(Keys.Left))
            {
                this.Direction = MOVE_LEFT;
                currentMovingDirection = MovingDirection.Left;
            }
            else if (currentKeyboardState.IsKeyDown(Keys.Right))
            {
                this.Direction = MOVE_RIGHT;
                currentMovingDirection = MovingDirection.Right;
            }

            if (Direction != Vector2.Zero)
            {
                Direction.Normalize();
                this.Animate();
            }

            this.Position += Direction * VELOCITY * (float)theGameTime.ElapsedGameTime.TotalSeconds;


            oldMovingDirection = currentMovingDirection;
        }


        public void Animate()
        {
            if(oldMovingDirection != currentMovingDirection)
            {
                currentFrame = 0;
            }

            this.Source = sources[(int)currentMovingDirection + currentFrame];

            if(frameTime < 0)
            {
                if (currentFrame < 4)
                    currentFrame++;
                else
                    currentFrame = 0;

                frameTime = 6;
            }

            frameTime--;
        }

        public void Draw(SpriteBatch theSpriteBatch)
        {
            base.Draw(theSpriteBatch, Vector2.Zero, this.Position, Color.White, 0.0f);
        }
    }
}
