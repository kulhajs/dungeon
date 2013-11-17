#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace dungeon
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        TileMap tileMap;
        Camera camera;
        Player player;

        KeyboardState currentKeyboardState;
        KeyboardState oldKeyboardState;

        MouseState currentMouseState;
        MouseState oldMouseState;

        const int MAP_WIDTH = 128;

        const int MAP_HEIGHT = 128;

        const int MAP_SMOOTHNESS = 3;

        const int MAP_SEED = 6;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 480;
            graphics.ApplyChanges();

            this.IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            tileMap = new TileMap(MAP_SEED, MAP_WIDTH, MAP_HEIGHT);
            tileMap.Generate(MAP_SMOOTHNESS);
            tileMap.Initialize();

            camera = new Camera(graphics.GraphicsDevice.Viewport);

            player = new Player(new Vector2(96, 96));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            tileMap.LoadContent(this.Content);
            player.LoadContent(this.Content);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            currentKeyboardState = Keyboard.GetState();
            currentMouseState = Mouse.GetState();

            if (currentKeyboardState.IsKeyDown(Keys.F5) && !oldKeyboardState.IsKeyDown(Keys.F5))
            {
                SeedGenerator.Seed = MAP_SEED;
                tileMap.Regenerate(MAP_SMOOTHNESS, this.Content);
            }

            player.Update(gameTime, currentKeyboardState, oldKeyboardState, tileMap.Tiles, currentMouseState, oldMouseState, camera);
            camera.Update(player, gameTime);

            oldKeyboardState = currentKeyboardState;
            oldMouseState = currentMouseState;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend,
                null, null, null, null,
                camera.Transform);

            tileMap.DrawTiles(this.spriteBatch);

            player.Draw(this.spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
