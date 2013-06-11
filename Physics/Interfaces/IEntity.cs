namespace CirclePhysics.Physics.Interfaces
{
	public interface IEntity : ICollidable
	{
		BoundingCircle BigBoundingCircle { get; }
		BoundingCircle[] BoundingCircles { get; }
	}
}
