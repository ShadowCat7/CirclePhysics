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

        private Coordinate _center;
        public Coordinate getCenter()
        { return _center; }

        public BoundingCircle(int radius, Coordinate center)
        {
            _radius = radius;
            _center = center;
        }
    }
}
