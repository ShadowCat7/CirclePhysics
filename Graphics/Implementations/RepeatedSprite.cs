using CirclePhysics.Graphics.Interfaces;
using CirclePhysics.Physics;

namespace CirclePhysics.Graphics.Implementations
{
	public class RepeatedSprite : ISprite
	{
		public RepeatedSprite(IImage image, int xRepeat, int yRepeat)
		{
			Image = image;
			m_xRepeat = xRepeat;
			m_yRepeat = yRepeat;
		}

		public IImage Image { get; private set; }
		private int m_xRepeat;
		private int m_yRepeat;

		public DrawingOptions GetDrawingInfo()
		{
			return new DrawingOptions(m_xRepeat * Image.Width, m_yRepeat * Image.Height, new Coordinate(m_xRepeat * Image.Width, m_yRepeat * Image.Height));
		}

		public void Update() { }
	}
}
