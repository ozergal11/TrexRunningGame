using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRexRunnigGame.Graphics
{
    public class Sprite
    {
        public Texture2D Texture { get; private set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public Sprite(Texture2D texture, int x, int y, int Width, int Height)
        {
            this.Texture = texture;

            this.X = x;

            this.Y = y;

            this.Width = Width;

            this.Height = Height;

        }

        public void Drew(SpriteBatch spriteBatch, Vector2 coordinates)
        {
            spriteBatch.Draw(Texture , coordinates , new Rectangle(X,Y,Width,Height), Color.White);

        }

    }
}
