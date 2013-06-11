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
		public double Y { get { return Magnitude*Math.Sin(Direction); } }

		public Coordinate Coordinate { get { return new Coordinate(X, Y); } }

		public static GameVector operator +(GameVector v1, GameVector v2)
		{
			return new GameVector(v1.);
		}
	}
}
