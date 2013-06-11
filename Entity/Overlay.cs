using System.Collections.Generic;
using CirclePhysics.Graphics.Interfaces;
using CirclePhysics.Physics;

namespace CirclePhysics.Entity
{
	public class Overlay : IOverlay
	{
		public Overlay(Coordinate screenPosition, Dictionary<string, ISprite> sprites, string startingSprite)
		{
			ScreenPosition = screenPosition;
			Sprites = sprites;
			CurrentSprite = Sprites[startingSprite];
		}

		// Fields
		public Coordinate ScreenPosition { get; private set; }
		public virtual void SetScreenPosition(Coordinate positionOfScreen) { }

		public Dictionary<string, ISprite> Sprites { get; private set; }
		public ISprite GetSpriteFromDict(string spriteTag)
		{
			if (Sprites == null || Sprites.ContainsKey(spriteTag))
			{ return null; }

			return Sprites[spriteTag];
		}

		public ISprite CurrentSprite { get; private set; }
		public void SetCurrentSprite(string key)
		{ CurrentSprite = Sprites[key]; }

		// Methods
		public virtual void Draw(IDrawer drawer)
		{ CurrentSprite.Draw(drawer); }

		public virtual void Update() {}
	}
}
