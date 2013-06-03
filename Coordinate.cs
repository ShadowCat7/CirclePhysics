using CirclePhysics.Physics;

namespace CirclePhysics
{
	public class Coordinate
	{
		private double _x;
		private double _y;

		public Coordinate(double x, double y)
		{
			_x = x;
			_y = y;
		}

		public double getX()
		{ return _x; }
		public double getY()
		{ return _y; }

		// TODO
		public static Coordinate makeFromVector(GameVector vector)
		{ return new Coordinate(vector.getXLength(), vector.getYLength()); }

		public static Coordinate operator +(Coordinate c1, Coordinate c2)
		{ return new Coordinate(c1._x + c2._x, c1._y + c2._y); }

		public static Coordinate operator -(Coordinate c1, Coordinate c2)
		{ return new Coordinate(c1._x - c2._x, c1._y - c2._y); }
	}
}
