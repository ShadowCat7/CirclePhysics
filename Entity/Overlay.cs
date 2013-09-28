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

			if (Sprites != null)
				CurrentSprite = Sprites[startingSprite];
		}

		public Coordinate ScreenPosition { get; private set; }
		public virtual void SetScreenPosition(Coordinate positionOfScreen)
		{
			ScreenPosition = positionOfScreen;
		}

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

		public virtual void Draw(IDrawer drawer)
		{ drawer.Draw(CurrentSprite, ScreenPosition); }

		public virtual void Update() { }
	}
}
