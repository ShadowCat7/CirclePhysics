namespace CirclePhysics.Physics
{
	public interface IEntity : ICollidable
	{
		BoundingCircle BigBoundingCircle { get; }
		BoundingCircle[] BoundingCircles { get; }
	}
}
