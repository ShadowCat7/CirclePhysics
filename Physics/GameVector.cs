using System;

namespace CirclePhysics.Physics
{
	public class GameVector
	{
		public GameVector(double magnitude, double direction)
		{
			Magnitude = magnitude;
			Direction = direction;
		}

		public double Magnitude { get; private set; }
		public double Direction { get; private set; }

		public double X { get { return Magnitude * Math.Cos(Direction);} }
		public double Y { get { return Magnitude * Math.Sin(Direction); } }

		public Coordinate ToCoordinate { get { return new Coordinate(X, Y); } }

		public static GameVector operator +(GameVector v1, GameVector v2)
		{
			double x = v1.X + v2.X;
			double y = v1.Y + v2.Y;
			double magnitude = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
			double direction = Math.Atan2(y, y);

			return new GameVector(magnitude, direction);
		}
	}
}
