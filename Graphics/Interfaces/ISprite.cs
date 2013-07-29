namespace CirclePhysics.Graphics.Interfaces
{
	public interface ISprite
	{
		void Draw(IDrawer drawer);
		IImage Image { get; }
		void Update();
	}
}
