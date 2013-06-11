namespace CirclePhysics.Physics.Interfaces
{
	public interface ISurface : ICollidable
	{
		Coordinate StartCoordinate { get; }
		Coordinate EndCoordinate { get; }
	}
}
