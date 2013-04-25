using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeTravel
{
    public class BoundingCircle
    {
        private int _radius;
        public int getRadius()
        { return _radius; }

        public BoundingCircle(int radius)
        { _radius = radius; }
    }
}
