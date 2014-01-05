using CirclePhysics.Graphics;

namespace CirclePhysics.Graphics
{
	public class Sprite : ISprite
	{
		public Sprite(IImage image)
		{
			Image = image;
		}

		public IImage Image { get; private set; }

		public DrawingOptions GetDrawingInfo()
		{
			return new DrawingOptions(Image.Width, Image.Height);
		}

		public void Update() { }
	}
}
