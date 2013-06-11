namespace CirclePhysics.Physics
{
	public class BoundingCircle
	{
		public BoundingCircle(int radius, Coordinate center)
		{
			Radius = radius;
			Center = center;
		}

		public int Radius { get; private set; }
		public Coordinate Center { get; private set; }
	}
}
