using System;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace SFMLMonThirs
{
    class MyGame : Drawable
    {
        Zmeika _player;
        Eda _food;
        int _score = 0;
        int screenWidth = 800, screenHeight = 600;
        Text _scoreText;
        Color _colBackgrnd =  new Color(0, 0, 0);


        public MyGame()
        {
            _player = new Zmeika(20, new Vector2f(screenWidth / 2, screenHeight / 2));
            _food = new Eda(screenWidth, screenHeight, 20);
            _scoreText = new Text()
            {
                Position = new Vector2f(10, 10),
                FillColor = Color.White,
                CharacterSize = 20,
                DisplayedString = "SCORE: 0",
                Font = new Font("ARIAL.TTF")
            };
        }

        internal void OnKP(object sender, KeyEventArgs e)
        {
            switch (e.Code)
            {
                case Keyboard.Key.Up:
                    _player.Turn(Dir.UP); 
                    break;
                case Keyboard.Key.Right:
                    _player.Turn(Dir.RIGHT);
                    break;
                case Keyboard.Key.Left:
                    _player.Turn(Dir.LEFT);
                    break;
                case Keyboard.Key.Down:
                    _player.Turn(Dir.DOWN);
                    break;
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Clear(_colBackgrnd);
            target.Draw(_player);
            target.Draw(_food);
            target.Draw(_scoreText);
        }

        public void Update()
        {
            UpdateScore();
            _player.Move((int)screenWidth, (int)screenHeight);
            if (_food.AmAm(_player._head))
            {
                _player.Grow();
                _food.Spawn(screenWidth, screenHeight);
                _colBackgrnd = _player.targetColor;

            }
            if ( _player.AmAm() )
            {
                _player.SetColor(_colBackgrnd);
            }
        }

        private void UpdateScore()
        {
            _score = _player.Score;
            _scoreText.DisplayedString = $"SCORE: {_score}";
        }

        internal void OnMM(object sender, MouseMoveEventArgs e)
        {
            byte red = (byte)((float)e.X / screenWidth * 255);
            byte blue = (byte)((float)e.Y / screenHeight * 255); 
            _colBackgrnd = new Color(red, 0, blue);

        }
    }
}
