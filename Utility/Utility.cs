using System;
using CirclePhysics.Physics;

namespace CirclePhysics.Utility
{
	public static class Utility
	{
		public static Coordinate ToCoordinate(this GameVector vector)
		{ return new Coordinate(vector.X, vector.Y); }

		public static GameVector ToGameVector(this Coordinate coordinate)
		{
			return new GameVector(Math.Sqrt(Math.Pow(coordinate.X, 2) + Math.Pow(coordinate.Y, 2)),
				Math.Atan2(coordinate.Y, coordinate.X));
		}
	}
}
