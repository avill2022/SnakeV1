using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake
{
    class Cuadro
    {
        private int x;
        public int X { get { return x; } set { x = value; } }
        private int y;
        public int Y { get { return y; } set { y = value; } }

        public Cuadro()
        {
            x = 0;
            y = 0;
        }

    }
}
