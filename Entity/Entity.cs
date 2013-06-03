using CirclePhysics.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace CirclePhysics.Entity
{
	public abstract class Entity : Overlay
	{
		private Coordinate _roomPosition;
		public Coordinate RoomPosition { get { return _roomPosition; } }

		private bool _onScreen;
		public bool OnScreen { get { return _onScreen; } }

		public Entity(Coordinate roomPosition, Dictionary<string, Sprite> sprites)
			: base(null, sprites)
		{ _roomPosition = roomPosition; }

		protected virtual void move(Coordinate newPosition)
		{ _roomPosition = newPosition; }

		public override void SetScreenPosition(Coordinate positionOfScreen)
		{
			base.SetScreenPosition(new Coordinate(_roomPosition.getX() - positionOfScreen.getX(), _roomPosition.getY() - positionOfScreen.getY()));

			if (GetCurrentSprite() != null)
			{
				if (_roomPosition.getX() > positionOfScreen.getX() + Screen.X)
				{ _onScreen = false; }
				else if (_roomPosition.getY() > positionOfScreen.getY() + Screen.Y)
				{ _onScreen = false; }
				else if (_roomPosition.getX() + GetCurrentSprite().Image.Bounds.Width < positionOfScreen.getX())
				{ _onScreen = false; }
				else if (_roomPosition.getY() + GetCurrentSprite().Image.Bounds.Height < positionOfScreen.getY())
				{ _onScreen = false; }
				else
				{ _onScreen = true; }
			}
			else
			{
				if (_roomPosition.getX() > positionOfScreen.getX() + Screen.X)
				{ _onScreen = false; }
				else if (_roomPosition.getY() > positionOfScreen.getY() + Screen.Y)
				{ _onScreen = false; }
				else if (_roomPosition.getX() < positionOfScreen.getX())
				{ _onScreen = false; }
				else if (_roomPosition.getY() < positionOfScreen.getY())
				{ _onScreen = false; }
				else
				{ _onScreen = true; }
			}
		}

		protected virtual void setPlayerScreenPosition(Coordinate screenPosition)
		{
			_onScreen = true;
			base.SetScreenPosition(screenPosition);
		}

		public virtual void damaged(int damage) { }

		public virtual void update(GameTime gameTime) { }
		public virtual void update(GameTime gameTime, List<StaticEntity> entityList, List<Surface> surfaces) { }
	}
}
