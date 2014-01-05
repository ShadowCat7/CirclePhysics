using CirclePhysics.Graphics;
using CirclePhysics.Physics;

namespace CirclePhysics.Graphics
{
	public class AnimatedSprite : ISprite
	{
		public AnimatedSprite(IImage image, int spriteWidth, int spriteCount, int framesUntilChange)
		{
			Image = image;
			SpriteWidth = spriteWidth;
			m_spriteCount = spriteCount;
			m_framesUntilChange = framesUntilChange;
			m_index = 0;
		}

		public IImage Image { get; private set; }
		public int SpriteWidth { get; private set; }
		private int m_spriteCount;
		private int m_framesUntilChange;
		private int m_index;
		private int m_timer;

		public DrawingOptions GetDrawingInfo()
		{
			return new DrawingOptions(SpriteWidth, Image.Height, new Coordinate(m_index * SpriteWidth, 0));
		}

		public void Update()
		{
			if (m_timer != m_framesUntilChange)
			{
				m_timer++;
			}
			else
			{
				m_timer = 0;

				m_index++;
				if (m_index == m_spriteCount)
					m_index = 0;
			}
		}
	}
}
