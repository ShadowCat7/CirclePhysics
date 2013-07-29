using CirclePhysics.Graphics.Interfaces;

namespace CirclePhysics.Graphics.Implementations
{
	public class Sprite : ISprite
	{
		public Sprite(IImage image)
		{ Image = image; }

		public IImage Image { get; private set; }

		public void Draw(IDrawer drawer)
		{ drawer.Draw(Image); }

		public void Update() { }
	}
}
