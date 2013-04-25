using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeTravel
{
    public static class Collision
    {
        public static Coordinate test(Entity tester, Entity testedAgainst)
        {
            double d = (tester.getBoundingRadius() + testedAgainst.getBoundingRadius()) - 
                distance(tester.getRoomPosition(), testedAgainst.getRoomPosition());
            if (d > 0)
            {
                Coordinate temp = testedAgainst.getRoomPosition() - tester.getRoomPosition();
                double direction = Math.Atan2(temp.getY(), temp.getX());
                return new Coordinate(Math.Cos(direction) * d, Math.Sin(direction) * d);
            }
            else
            { return null; }
        }

        public static double distance(Coordinate c1, Coordinate c2)
        { return Math.Sqrt(Math.Pow(c2.getX() - c1.getX(), 2) + Math.Pow(c2.getY() - c1.getY(), 2)); }

        public static Coordinate test(Entity tester, Surface surface)
        {
            Coordinate sc1 = surface.getStartCoordinate() + surface.getRoomPosition();
            Coordinate sc2 = surface.getEndCoordinate() + surface.getRoomPosition();

            double slope = (sc2.getY() - sc1.getY()) / (sc2.getX() - sc1.getX());
            double yInt = sc1.getY() - slope * sc1.getX();

            double perpYInt = tester.getRoomPosition().getY() + tester.getRoomPosition().getX() / slope;
            double xIntersect = (perpYInt - yInt) / (slope + 1 / slope);
            double yIntersect = xIntersect * slope + yInt;

            if (slope == 0)
            {
                xIntersect = tester.getRoomPosition().getX();
                yIntersect = yInt;
            }

            if ((sc1.getX() <= xIntersect && sc2.getX() >= xIntersect ||
                sc1.getX() >= xIntersect && sc2.getX() <= xIntersect) &&
                (sc1.getY() <= yIntersect && sc2.getY() >= yIntersect ||
                sc1.getY() >= yIntersect && sc2.getY() <= yIntersect))
            {
                double d = tester.getBoundingRadius() - distance(tester.getRoomPosition(), new Coordinate(xIntersect, yIntersect));
                if (d > 0)
                {
                    Coordinate temp = new Coordinate(xIntersect, yIntersect) - tester.getRoomPosition();
                    double direction = Math.Atan2(temp.getY(), temp.getX());
                    return new Coordinate(Math.Cos(direction) * d, Math.Sin(direction) * d);
                }
            }

            double d1 = tester.getBoundingRadius() - distance(tester.getRoomPosition(), sc1);
            if (d1 > 0)
            {
                Coordinate temp = sc1 - tester.getRoomPosition();
                double direction = Math.Atan2(temp.getY(), temp.getX());
                return new Coordinate(Math.Cos(direction) * d1, Math.Sin(direction) * d1);
            }
            d1 = tester.getBoundingRadius() - distance(tester.getRoomPosition(), sc2);
            if (d1 > 0)
            {
                Coordinate temp = sc2 - tester.getRoomPosition();
                double direction = Math.Atan2(temp.getY(), temp.getX());
                return new Coordinate(Math.Cos(direction) * d1, Math.Sin(direction) * d1);
            }

            return null;
        }
    }
}
