namespace CirclePhysics.Graphics
{
	public class Sprite
	{
		private Texture2D _image;
		public Texture2D Image { get { return _image; } }

		private Coordinate _origin;
		public Coordinate Origin { get { return _origin; } }

		public Sprite(Texture2D image)
		{
			_image = image;
		}

		public virtual void Update() { }
		public virtual void Reset() { }

		public virtual void Draw(SpriteBatch spriteBatch, Coordinate screenPosition)
		{
			spriteBatch.Draw(_image, new Vector2((float)screenPosition.getX(), (float)screenPosition.getY()), Color.White);
		}

		public virtual void Draw(SpriteBatch spriteBatch, Coordinate screenPosition, bool reverse)
		{
			SpriteEffects effect;
			if (reverse)
			{ effect = SpriteEffects.None; }
			else
			{ effect = SpriteEffects.FlipHorizontally; }

			spriteBatch.Draw(_image, new Vector2((float)screenPosition.getX(), (float)screenPosition.getY()),
				null, Color.White, 0, new Vector2(0, 0), 0, effect, 0);
		}

		public virtual void Draw(SpriteBatch spriteBatch, Coordinate screenPosition, double directionFacing)
		{
			spriteBatch.Draw(_image, new Vector2((float)screenPosition.getX(), (float)screenPosition.getY()),
				null, Color.White, (float)directionFacing, new Vector2(0, 0), 0, SpriteEffects.None, 0);
		}
	}
}
