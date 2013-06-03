using CirclePhysics.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace CirclePhysics.Entity
{
	public abstract class Overlay
	{
		private Coordinate _screenPosition;
		public Coordinate getScreenPosition()
		{ return _screenPosition; }
		public virtual void SetScreenPosition(Coordinate screenPosition)
		{ _screenPosition = screenPosition; }

		private Dictionary<string, Sprite> _sprites;
		public Dictionary<string, Sprite> Sprites { get { return _sprites; } }
		public Sprite getSpriteFromDict(string spriteTag)
		{
			if (_sprites == null || _sprites.ContainsKey(spriteTag))
			{ return null; }

			return _sprites[spriteTag];
		}

		private Sprite _sprite;
		public Sprite GetCurrentSprite()
		{ return _sprite; }
		public void SetCurrentSprite(string key)
		{ _sprite = _sprites[key]; }

		public Overlay(Coordinate screenPosition, Dictionary<string, Sprite> sprites)
		{
			_screenPosition = screenPosition;
			_sprites = sprites;

			_sprite = null;
			SetStartingSprite();
		}

		public abstract void SetStartingSprite();

		public virtual void Draw(SpriteBatch spriteBatch)
		{ spriteBatch.Draw(_sprite.Image, new Vector2((int)_screenPosition.getX(), (int)_screenPosition.getY()), Color.White); }
	}
}
