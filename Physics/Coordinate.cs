namespace CirclePhysics.Physics
{
	public class Coordinate
	{
		public Coordinate(double x, double y)
		{
			X = x;
			Y = y;
		}

		public double X { get; private set; }

		public double Y { get; private set; }

		public static Coordinate operator +(Coordinate c1, Coordinate c2)
		{
			return new Coordinate(c1.X + c2.X, c1.Y + c2.Y);
		}

		public static Coordinate operator -(Coordinate c1, Coordinate c2)
		{
			return new Coordinate(c1.X - c2.X, c1.Y - c2.Y);
		}
	}
}
