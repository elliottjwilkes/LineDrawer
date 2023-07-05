using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsApp1
{
    public class Line
    {
        private (int x, int y) _start;
        private (int x, int y) _end;
        private Color _colour;

        public Line((int x,int y) start, (int x, int y) end, Color colour)
        {
            _start = start;
            _end = end;
            _colour = colour;
        }

        public (int x, int y) Start
        {
            get { return _start; }
            set { _start = value; }
        }

        public (int x, int y) End
        {
            get { return _end; }
            set { _end = value; }
        }

        public Color Colour
        {
            get { return _colour; }
            set { _colour = value; }
        }

        public override string ToString()
        {
             return "Start Point: (" + _start.x + ", " + _start.y + "), End Point: (" + _end.x + ", " + _end.y + ")";
        }
    }
}
