namespace CirclePhysics.Physics
{
	public interface ISurface : ICollidable
	{
		Coordinate StartCoordinate { get; }
		Coordinate EndCoordinate { get; }
	}
}
