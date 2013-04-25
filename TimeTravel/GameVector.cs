using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeTravel
{
    public class GameVector
    {
        private double _magnitude;
        public double getMagnitude()
        { return _magnitude; }

        private double _direction;
        public double getDirection()
        { return _direction; }

        public GameVector(double magnitude, double direction)
        {
            if (magnitude < 0)
            {
                _magnitude = -magnitude;
                direction += Math.PI;
                if (direction >= Math.PI)
                { direction -= 2 * Math.PI; }
                _direction = direction;
            }
            else
            {
                _magnitude = magnitude;
                _direction = direction;
            }
        }

        public double getXLength()
        { return _magnitude * Math.Cos(_direction); }
        public double getYLength()
        { return _magnitude * Math.Sin(_direction); }

        public static GameVector makeFromCoordinate(Coordinate coordinate)
        {
            double magnitude = Math.Sqrt(coordinate.getX() * coordinate.getX() + coordinate.getY() * coordinate.getY());
            double direction = Math.Atan2(coordinate.getY(), coordinate.getX());
            return new GameVector(magnitude, direction);
        }

        public static GameVector operator +(GameVector v1, GameVector v2)
        {
            double x3 = v1.getXLength() + v2.getXLength();
            double y3 = v1.getYLength() + v2.getYLength();

            return makeFromCoordinate(new Coordinate(x3, y3));
        }
    }
}
