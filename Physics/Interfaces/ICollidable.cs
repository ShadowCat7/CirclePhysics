namespace CirclePhysics.Physics.Interfaces
{
	public interface ICollidable
	{
		Coordinate RoomPosition { get; }

		void OnCollision(ICollidable collidable);
	}
}
