using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



namespace dungeon
{
    class Camera
    {
        public Matrix Transform;
        public Vector2 Origin;
        Viewport view;

        float convergenceSpeed = 2.0f;
        float lookPlayer = 1.5f;
        float lookAhead = 0.2f;

        public Camera(Viewport view)
        {
            this.view = view;
            this.Origin = new Vector2(view.Width / 2, view.Height / 2);
        }

        public void Update(Player player, GameTime theGameTime)
        {
            //if (player.X < 368)
            //    this.Origin.X = 0;
            //else
                //this.Origin.X = player.X - 368;
            this.Origin.X  += convergenceSpeed * (lookPlayer * ((player.Position.X - 368) - this.Origin.X) + lookAhead * player.Direction.X) * (float)theGameTime.ElapsedGameTime.TotalSeconds;

            //if (player.Y < 208)
            //    this.Origin.Y = 0;
            //else
                //this.Origin.Y = player.Y - 208;
            this.Origin.Y += convergenceSpeed * (lookPlayer * ((player.Position.Y - 208) - this.Origin.Y) + lookAhead * player.Direction.Y) * (float)theGameTime.ElapsedGameTime.TotalSeconds;

            this.Transform = Matrix.CreateTranslation(new Vector3(-Origin.X, -Origin.Y, 0));
        }
    }
}
