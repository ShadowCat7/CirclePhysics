using CirclePhysics.Physics;
namespace CirclePhysics.Graphics
{
	public interface IDrawer
	{
		void Draw(ISprite sprite, Coordinate screenPosition);
	}
}
