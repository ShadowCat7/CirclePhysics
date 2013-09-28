using CirclePhysics.Physics;
namespace CirclePhysics.Graphics.Interfaces
{
	public interface IDrawer
	{
		void Draw(ISprite sprite, Coordinate screenPosition);
	}
}
