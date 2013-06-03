using CirclePhysics.Entity;
using System;
using System.Collections.Generic;

namespace CirclePhysics.Physics
{
	public static class Collision
	{
		public static bool test(StaticEntity entity1, StaticEntity entity2)
		{
			return (findCollision(entity1, entity2) != null);
		}

		public static Coordinate findColliding(StaticEntity tester, StaticEntity testedAgainst)
		{
			return findCollision(tester, testedAgainst);
		}

		public static bool test(StaticEntity entity, Surface surface)
		{
			return (findCollision(entity, surface) != null);
		}

		public static Coordinate findColliding(StaticEntity tester, Surface testedAgainst)
		{
			return findCollision(tester, testedAgainst);
		}

		private static Coordinate findCollision(StaticEntity tester, StaticEntity testedAgainst)
		{
			Coordinate temp = testCircles(new BoundingCircle(tester.BigBoundingCircle.Radius,
				tester.BigBoundingCircle.Center + tester.RoomPosition),
				new BoundingCircle(testedAgainst.BigBoundingCircle.Radius,
				testedAgainst.BigBoundingCircle.Center + testedAgainst.RoomPosition));

			if (temp != null)
			{
				if (tester.BoundingCircles == null && testedAgainst.BoundingCircles == null)
				{ return temp; }
				else
				{
					List<Coordinate> coordinates = new List<Coordinate>();

					if (tester.BoundingCircles == null)
					{
						for (int i = 0; i < testedAgainst.BoundingCircles.Length; ++i)
						{
							temp = testCircles(new BoundingCircle(tester.BigBoundingCircle.Radius,
								tester.BigBoundingCircle.Center + tester.RoomPosition),
								new BoundingCircle(testedAgainst.BoundingCircles[i].Radius,
								testedAgainst.BoundingCircles[i].Center + testedAgainst.RoomPosition));
							if (temp != null)
							{ coordinates.Add(temp); }
						}
					}
					else if (testedAgainst.BoundingCircles == null)
					{
						for (int i = 0; i < tester.BoundingCircles.Length; ++i)
						{
							temp = testCircles(new BoundingCircle(tester.BoundingCircles[i].Radius,
								tester.BoundingCircles[i].Center + tester.RoomPosition),
								new BoundingCircle(testedAgainst.BigBoundingCircle.Radius,
								testedAgainst.BigBoundingCircle.Center + testedAgainst.RoomPosition));
							if (temp != null)
							{ coordinates.Add(temp); }
						}
					}
					else
					{
						for (int i = 0; i < tester.BoundingCircles.Length; ++i)
						{
							for (int j = 0; j < testedAgainst.BoundingCircles.Length; ++j)
							{
								temp = testCircles(new BoundingCircle(tester.BoundingCircles[i].Radius,
									tester.BoundingCircles[i].Center + tester.RoomPosition),
									new BoundingCircle(testedAgainst.BoundingCircles[j].Radius,
									testedAgainst.BoundingCircles[j].Center + testedAgainst.RoomPosition));
								if (temp != null)
								{ coordinates.Add(temp); }
							}
						}
					}

					if (coordinates.Count == 0)
					{ return null; }
					else if (coordinates.Count == 1)
					{ return coordinates[0]; }
					else
					{
						double bestX = 0;
						double bestY = 0;
						for (int i = 0; i < coordinates.Count; ++i)
						{
							if (Math.Abs(bestX) < Math.Abs(coordinates[i].getX()))
							{ bestX = coordinates[i].getX(); }
							if (Math.Abs(bestY) < Math.Abs(coordinates[i].getY()))
							{ bestY = coordinates[i].getY(); }
						}

						return new Coordinate(bestX, bestY);
					}
				}
			}
			else
			{ return null; }
		}

		private static Coordinate testCircles(BoundingCircle bc1, BoundingCircle bc2)
		{
			double d = (bc1.Radius + bc2.Radius) -
				distance(bc1.Center, bc2.Center);
			if (d > 0)
			{
				Coordinate temp = bc2.Center - bc1.Center;
				double direction = Math.Atan2(temp.getY(), temp.getX());
				return new Coordinate(Math.Cos(direction) * d, Math.Sin(direction) * d);
			}
			else
			{ return null; }
		}

		public static double distance(Coordinate c1, Coordinate c2)
		{ return Math.Sqrt(Math.Pow(c2.getX() - c1.getX(), 2) + Math.Pow(c2.getY() - c1.getY(), 2)); }

		private static Coordinate findCollision(StaticEntity tester, Surface surface)
		{
			Coordinate temp = testCircleOnSurface(new BoundingCircle(tester.BigBoundingCircle.Radius,
				tester.BigBoundingCircle.Center + tester.RoomPosition), surface);
			if (temp != null)
			{
				if (tester.BoundingCircles == null)
				{ return temp; }
				else
				{
					List<Coordinate> coordinates = new List<Coordinate>();
					for (int i = 0; i < tester.BoundingCircles.Length; ++i)
					{
						temp = testCircleOnSurface(new BoundingCircle(tester.BoundingCircles[i].Radius,
							tester.BoundingCircles[i].Center + tester.RoomPosition), surface);
						if (temp != null)
						{ coordinates.Add(temp); }
					}

					if (coordinates.Count == 0)
					{ return null; }
					else if (coordinates.Count == 1)
					{ return coordinates[0]; }
					else
					{
						double bestX = 0;
						double bestY = 0;
						for (int i = 0; i < coordinates.Count; ++i)
						{
							if (Math.Abs(bestX) < Math.Abs(coordinates[i].getX()))
							{ bestX = coordinates[i].getX(); }
							if (Math.Abs(bestY) < Math.Abs(coordinates[i].getY()))
							{ bestY = coordinates[i].getY(); }
						}

						return new Coordinate(bestX, bestY);
					}
				}
			}
			else
			{ return null; }
		}

		private static Coordinate testCircleOnSurface(BoundingCircle circle, Surface surface)
		{
			Coordinate sc1 = surface.StartCoordinate + surface.RoomPosition;
			Coordinate sc2 = surface.EndCoordinate + surface.RoomPosition;

			double slope = (sc2.getY() - sc1.getY()) / (sc2.getX() - sc1.getX());
			double yInt = sc1.getY() - slope * sc1.getX();

			double perpYInt = circle.Center.getY() + circle.Center.getX() / slope;
			double xIntersect = (perpYInt - yInt) / (slope + 1 / slope);
			double yIntersect = xIntersect * slope + yInt;

			if (slope == 0)
			{
				xIntersect = circle.Center.getX();
				yIntersect = yInt;
			}

			if ((sc1.getX() <= xIntersect && sc2.getX() >= xIntersect ||
				sc1.getX() >= xIntersect && sc2.getX() <= xIntersect) &&
				(sc1.getY() <= yIntersect && sc2.getY() >= yIntersect ||
				sc1.getY() >= yIntersect && sc2.getY() <= yIntersect))
			{
				double d = circle.Radius - distance(circle.Center, new Coordinate(xIntersect, yIntersect));
				if (d > 0)
				{
					Coordinate temp = new Coordinate(xIntersect, yIntersect) - circle.Center;
					double direction = Math.Atan2(temp.getY(), temp.getX());
					return new Coordinate(Math.Cos(direction) * d, Math.Sin(direction) * d);
				}
			}

			double d1 = circle.Radius - distance(circle.Center, sc1);
			if (d1 > 0)
			{
				Coordinate temp = sc1 - circle.Center;
				double direction = Math.Atan2(temp.getY(), temp.getX());
				return new Coordinate(Math.Cos(direction) * d1, Math.Sin(direction) * d1);
			}
			d1 = circle.Radius - distance(circle.Center, sc2);
			if (d1 > 0)
			{
				Coordinate temp = sc2 - circle.Center;
				double direction = Math.Atan2(temp.getY(), temp.getX());
				return new Coordinate(Math.Cos(direction) * d1, Math.Sin(direction) * d1);
			}

			return null;
		}
	}
}