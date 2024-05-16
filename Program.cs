using System;
using SFML.Window;
using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;

namespace SFMLMonThirs
{
    class Program
    {
        static void Main(string[] args)
        {
            VideoMode vm = new VideoMode(800, 600);
            RenderWindow rw = new RenderWindow(vm, "SFMLTEST");
            MyGame mg = new MyGame();
            rw.SetFramerateLimit(12);
            rw.Closed += (sender, e) => { rw.Close(); };
            rw.KeyPressed += mg.OnKP;
            rw.MouseMoved += mg.OnMM;
            while (rw.IsOpen)
            {
                rw.DispatchEvents();
                mg.Update();
                rw.Draw(mg);
                rw.Display();
            }
        }
    }
}
