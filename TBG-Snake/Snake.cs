using System;
using System.Collections.Generic;
using System.Text;

namespace TBG_Snake
{
    class Snake
    {
        public int x, y;
        public Direction dir = 0;
        public Tail[] tail = new Tail[100];
        public int tailNum = 0;
    }

    struct Tail
    {
        public int x, y;
    }

    enum Direction
    {
        STOP = 0, UP, DOWN, LEFT, RIGHT
    }
}
