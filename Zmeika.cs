using System;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;
using SFML.System;

namespace SFMLMonThirs
{
    enum Dir { UP, DOWN, LEFT, RIGHT };

    class Zmeika : Drawable
    {
        public RectangleShape _head;
        Dir _headDir = Dir.UP;
        Vector2f[] spd = new Vector2f[4];
        public Color targetColor = Color.Black;
        List<RectangleShape> _body = new List<RectangleShape>();
        public int Score
        {
            get { return _body.Count * 100; }
        }

        public Zmeika(int size, Vector2f startPos)
        {
            _head = new RectangleShape()
            {
                Position = startPos,
                Size = new Vector2f(size, size)
            };



            for (int i = 1; i <= 5; i++)
            {
                _body.Add(
                    new RectangleShape()
                    {
                        Position = _head.Position + i * new Vector2f(0, size),
                        Size = new Vector2f(size, size)
                    }
                );
            }

            spd[(int)Dir.UP] = new Vector2f(0, -size);
            spd[(int)Dir.DOWN] = new Vector2f(0, size);
            spd[(int)Dir.LEFT] = new Vector2f(-size, 0);
            spd[(int)Dir.RIGHT] = new Vector2f(size, 0);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            foreach (RectangleShape rs in _body)
                target.Draw(rs);
            target.Draw(_head);
        }

        public void Move(int width, int height)
        {
            for (int i = _body.Count - 1; i > 0; i--)
                _body[i].Position = _body[i - 1].Position;

            _body[0].Position = _head.Position;

            _head.Position += spd[(int)_headDir];
            if (_head.Position.Y >= height)
                _head.Position = new Vector2f(_head.Position.X, 0);
            if (_head.Position.Y < 0)
                _head.Position = new Vector2f(_head.Position.X, height - _head.Size.Y);
            if (_head.Position.X >= width)
                _head.Position = new Vector2f(0, _head.Position.Y);
            if (_head.Position.X < 0)
                _head.Position = new Vector2f(width - _head.Size.X, _head.Position.Y);
        }

        internal void Grow()
        {
            _body.Add(new RectangleShape(_body[_body.Count - 1]));

            byte colorStepR = (byte)((255 - targetColor.R) / _body.Count);
            byte colorStepG = (byte)((255 - targetColor.G) / _body.Count);
            byte colorStepB = (byte)((255 - targetColor.B) / _body.Count);

            for (int i = 0; i < _body.Count; i++)
            {
                _body[i].FillColor = new Color(
                    (byte)(255 - (colorStepR * i)),
                    (byte)(255 - (colorStepG * i)),
                    (byte)(255 - (colorStepB * i))
                    );
            }

        }

        public void Turn(Dir newDir)
        {
            _headDir = newDir;
        }

        public bool AmAm()
        {
            for (int i = _body.Count - 1; i >= 0; i--)
                if (_head.Position == _body[i].Position)
                {
                    _body.RemoveRange(i, _body.Count - i);
                    return true;
                }

            return false;
        }

        public void SetColor(Color colBackgrnd)
        {
            targetColor = colBackgrnd;
        }
    }

}
