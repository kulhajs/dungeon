using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



namespace dungeon
{
    class Camera
    {
        public Matrix Transform;
        public Vector2 Origin;
        Viewport view;

        public Camera(Viewport view)
        {
            this.view = view;
            this.Origin = new Vector2(view.Width / 2, view.Height / 2);
        }

        public void Update(Player player)
        {
            if (player.X < 368)
                this.Origin.X = 0;
            else
                this.Origin.X = player.X - 368;

            if (player.Y < 208)
                this.Origin.Y = 0;
            else
                this.Origin.Y = player.Y - 208;

            this.Transform = Matrix.CreateTranslation(new Vector3(-Origin.X, -Origin.Y, 0));
        }
    }
}
