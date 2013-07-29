using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CirclePhysics.Graphics
{
	public class AnimatedSprite : Sprite
	{
		private int _frames;

		private int _currentFrame;
		public int CurrentFrame { get { return _currentFrame; } }

		private int _elapsedCount;
		public int ElapsedCount { get { return _elapsedCount; } }

		public AnimatedSprite(Texture2D image, int frames)
			: base(image)
		{
			_frames = frames;
			_currentFrame = 0;
			_elapsedCount = 0;
		}

		public override void Reset()
		{
			_currentFrame = 0;
			_elapsedCount = 0;
		}

		public override void Update()
		{
			if (_elapsedCount == 4)
			{
				++_currentFrame;

				if (_currentFrame == _frames)
				{ _currentFrame = 0; }

				_elapsedCount = 0;
			}
			else
			{ ++_elapsedCount; }
		}

		public override void Draw(SpriteBatch spriteBatch, Coordinate screenPosition)
		{
			spriteBatch.Draw(Image, new Vector2((float)screenPosition.getX(), (float)screenPosition.getY()),
				new Rectangle(_currentFrame * Image.Width / _frames, 0, Image.Width / _frames, Image.Height),
				Color.White);
		}

		public override void Draw(SpriteBatch spriteBatch, Coordinate screenPosition, bool reverse)
		{
			SpriteEffects effect;
			if (reverse)
			{ effect = SpriteEffects.None; }
			else
			{ effect = SpriteEffects.FlipHorizontally; }

			spriteBatch.Draw(Image, new Vector2((float)screenPosition.getX(), (float)screenPosition.getY()),
				new Rectangle(_currentFrame * Image.Width / _frames, 0, Image.Width / _frames, Image.Height),
				Color.White, 0, new Vector2(), 1, effect, 0);
		}

		public override void Draw(SpriteBatch spriteBatch, Coordinate screenPosition, double directionFacing)
		{
			spriteBatch.Draw(Image, new Vector2((float)screenPosition.getX(), (float)screenPosition.getY()),
				new Rectangle(_currentFrame * Image.Width / _frames, 0, Image.Width / _frames, Image.Height),
				Color.White, (float)directionFacing, new Vector2(0, 0), 1, SpriteEffects.None, 0);
		}
	}
}
