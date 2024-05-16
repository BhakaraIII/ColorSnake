using System;
using System.Runtime.InteropServices;
using SFML.Graphics;
using SFML.System;

namespace SFMLMonThirs
{
    class Eda : Drawable
    {
        RectangleShape rs;
        Random r;
        public Color FoodColor
        {
            get
            {
                return rs.FillColor;
            }
        }

        public Eda(int width, int height, int scale)
        {
            r = new Random();
            rs = new RectangleShape()
            {
                Size = new Vector2f(scale, scale),
                FillColor = Color.Red
            };
            Spawn(width, height);

        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(rs);
        }

        public void Spawn(int w, int h)
        {
            int xMAX = w / (int)rs.Size.X;
            int yMAX = h / (int)rs.Size.Y;

            rs.Position = new Vector2f(
                r.Next(0, xMAX) * rs.Size.X,
                r.Next(0, yMAX) * rs.Size.Y
                    );

            int colorNum = r.Next(0, 8);
            rs.FillColor = colorNum switch
            {
                0 => Color.Red,
                1 => Color.Green,
                2 => Color.Blue,
                3 => Color.Magenta,
                4 => Color.Yellow,
                5 => Color.Cyan,
                6 => Color.Black,
                7 => Color.White
            };
        }

        public bool AmAm(RectangleShape rsOut)
        {
            return rs.GetGlobalBounds().Intersects(rsOut.GetGlobalBounds());
        }
    }

}
