using System;
using System.Collections.Generic;
using CirclePhysics.Physics.Interfaces;

namespace CirclePhysics.Physics
{
	public static class Collision
	{
		public static bool Test(IEntity entity1, ICollidable entity2)
		{ return FindColliding(entity1, entity2) != null; }

		public static Coordinate FindColliding(IEntity tester, ICollidable testedAgainst)
		{
			Type type = testedAgainst.GetType();
			if (type == typeof(IEntity))
			{ return FindCollision(tester, testedAgainst as IEntity); }
			else if (type == typeof(ISurface))
			{ return FindCollision(tester, testedAgainst as ISurface); }

			return null;
		}

		private static Coordinate FindCollision(IEntity tester, IEntity testedAgainst)
		{
			Coordinate temp = TestCircles(
				new BoundingCircle(tester.BigBoundingCircle.Radius, tester.BigBoundingCircle.Center + tester.RoomPosition),
				new BoundingCircle(testedAgainst.BigBoundingCircle.Radius, testedAgainst.BigBoundingCircle.Center + testedAgainst.RoomPosition));

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
							temp = TestCircles(new BoundingCircle(tester.BigBoundingCircle.Radius,
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
							temp = TestCircles(new BoundingCircle(tester.BoundingCircles[i].Radius,
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
								temp = TestCircles(new BoundingCircle(tester.BoundingCircles[i].Radius,
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
							if (Math.Abs(bestX) < Math.Abs(coordinates[i].X))
							{ bestX = coordinates[i].X; }
							if (Math.Abs(bestY) < Math.Abs(coordinates[i].Y))
							{ bestY = coordinates[i].Y; }
						}

						return new Coordinate(bestX, bestY);
					}
				}
			}

			return null;
		}

		private static Coordinate TestCircles(BoundingCircle bc1, BoundingCircle bc2)
		{
			double d = (bc1.Radius + bc2.Radius) -
				Distance(bc1.Center, bc2.Center);
			if (d > 0)
			{
				Coordinate temp = bc2.Center - bc1.Center;
				double direction = Math.Atan2(temp.Y, temp.X);
				return new Coordinate(Math.Cos(direction) * d, Math.Sin(direction) * d);
			}
			else
			{ return null; }
		}

		public static double Distance(Coordinate c1, Coordinate c2)
		{ return Math.Sqrt(Math.Pow(c2.X - c1.X, 2) + Math.Pow(c2.Y - c1.Y, 2)); }

		private static Coordinate FindCollision(IEntity tester, ISurface surface)
		{
			Coordinate temp = TestCircleOnSurface(new BoundingCircle(tester.BigBoundingCircle.Radius,
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
						temp = TestCircleOnSurface(new BoundingCircle(tester.BoundingCircles[i].Radius,
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
							if (Math.Abs(bestX) < Math.Abs(coordinates[i].X))
							{ bestX = coordinates[i].X; }
							if (Math.Abs(bestY) < Math.Abs(coordinates[i].Y))
							{ bestY = coordinates[i].Y; }
						}

						return new Coordinate(bestX, bestY);
					}
				}
			}
			else
			{ return null; }
		}

		private static Coordinate TestCircleOnSurface(BoundingCircle circle, ISurface surface)
		{
			Coordinate sc1 = surface.StartCoordinate + surface.RoomPosition;
			Coordinate sc2 = surface.EndCoordinate + surface.RoomPosition;

			double slope = (sc2.Y - sc1.Y) / (sc2.X - sc1.X);
			double yInt = sc1.Y - slope * sc1.X;

			double perpYInt = circle.Center.Y + circle.Center.X / slope;
			double xIntersect = (perpYInt - yInt) / (slope + 1 / slope);
			double yIntersect = xIntersect * slope + yInt;

			if (slope == 0)
			{
				xIntersect = circle.Center.X;
				yIntersect = yInt;
			}

			if ((sc1.X <= xIntersect && sc2.X >= xIntersect ||
				sc1.X >= xIntersect && sc2.X <= xIntersect) &&
				(sc1.Y <= yIntersect && sc2.Y >= yIntersect ||
				sc1.Y >= yIntersect && sc2.Y <= yIntersect))
			{
				double d = circle.Radius - Distance(circle.Center, new Coordinate(xIntersect, yIntersect));
				if (d > 0)
				{
					Coordinate temp = new Coordinate(xIntersect, yIntersect) - circle.Center;
					double direction = Math.Atan2(temp.Y, temp.X);
					return new Coordinate(Math.Cos(direction) * d, Math.Sin(direction) * d);
				}
			}

			double d1 = circle.Radius - Distance(circle.Center, sc1);
			if (d1 > 0)
			{
				Coordinate temp = sc1 - circle.Center;
				double direction = Math.Atan2(temp.Y, temp.X);
				return new Coordinate(Math.Cos(direction) * d1, Math.Sin(direction) * d1);
			}
			d1 = circle.Radius - Distance(circle.Center, sc2);
			if (d1 > 0)
			{
				Coordinate temp = sc2 - circle.Center;
				double direction = Math.Atan2(temp.Y, temp.X);
				return new Coordinate(Math.Cos(direction) * d1, Math.Sin(direction) * d1);
			}

			return null;
		}
	}
}