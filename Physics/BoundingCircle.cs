namespace CirclePhysics.Physics
{
	public class BoundingCircle
	{
		private int _radius;
		public int Radius { get { return _radius; } }

		private Coordinate _center;
		public Coordinate Center { get { return _center; } }

		public BoundingCircle(int radius, Coordinate center)
		{
			_radius = radius;
			_center = center;
		}
	}
}