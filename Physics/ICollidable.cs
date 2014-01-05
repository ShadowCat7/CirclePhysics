namespace CirclePhysics.Physics
{
	public interface ICollidable
	{
		Coordinate RoomPosition { get; }

		void OnCollision(ICollidable collidable);
	}
}
