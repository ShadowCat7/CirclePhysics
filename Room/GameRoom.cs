using CirclePhysics.Entity;
using CirclePhysics.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace CirclePhysics.Room
{
	public class GameRoom : Room
	{
		private Player _player;

		private Coordinate _onScreen;

		private bool _paused;

		private List<StaticEntity> _entityList;

		private List<Surface> _surfaces;

		public GameRoom(Coordinate size, Sprite background, Player player, List<StaticEntity> entityList, List<Surface> surfaces)
			: base(size, background)
		{
			_player = player;
			_entityList = entityList;
			_surfaces = surfaces;

			_onScreen = new Coordinate(0, 0);
			_paused = false;
		}

		public override void Update(GameTime gameTime, KeyboardState newKeyboardState, KeyboardState oldKeyboardState,
			MouseState newMouseState, MouseState oldMouseState)
		{
			if (newKeyboardState.IsKeyDown(Keys.Escape))
			{ _paused = !_paused; }

			if (!_paused)
			{
				for (int k = 0; k < 10; ++k)
				{
					for (int i = 0; i < _entityList.Count; i++)
					{
						_entityList[i].update(gameTime);
						_entityList[i].update(gameTime, _entityList, _surfaces);
					}

					_player.update(gameTime, newKeyboardState, oldKeyboardState, newMouseState, oldMouseState, _entityList, _surfaces);
				}

				_player.SetScreenPosition(getScreenPositionFromEntity(_player));

				for (int i = 0; i < _entityList.Count; ++i)
				{ _entityList[i].SetScreenPosition(_onScreen); }

				for (int i = 0; i < _surfaces.Count; ++i)
				{ _surfaces[i].SetScreenPosition(_onScreen); }
			}
		}

		public override void draw(SpriteBatch spriteBatch)
		{
			//TODO background

			for (int i = 0; i < _surfaces.Count; ++i)
			{ _surfaces[i].Draw(spriteBatch); }

			for (int i = 0; i < _entityList.Count; ++i)
			{ _entityList[i].Draw(spriteBatch); }

			_player.Draw(spriteBatch);
		}

		public Coordinate getScreenPositionFromEntity(StaticEntity entity)
		{
			double x = 0;
			double y = 0;
			double ex = 0;
			double ey = 0;

			if (entity.RoomPosition.getX() + entity.BigBoundingCircle.Radius / 2 < Screen.X / 2)
			{
				x = 0;
				ex = entity.RoomPosition.getX();
			}
			else if (entity.RoomPosition.getX() + entity.BigBoundingCircle.Radius / 2 > Size.getX() - Screen.X / 2)
			{
				x = Size.getX() - Screen.X;
				ex = entity.RoomPosition.getX() - (Size.getX() - Screen.X);
			}
			else
			{
				x = entity.RoomPosition.getX() + entity.BigBoundingCircle.Radius / 2 - Screen.X / 2;
				ex = Screen.X / 2 - entity.BigBoundingCircle.Radius / 2;
			}

			if (entity.RoomPosition.getY() + entity.BigBoundingCircle.Radius / 2 < Screen.Y / 2)
			{
				y = 0;
				ey = entity.RoomPosition.getY();
			}
			else if (entity.RoomPosition.getY() + entity.BigBoundingCircle.Radius / 2 > Size.getY() - Screen.Y / 2)
			{
				y = Size.getY() - Screen.Y;
				ey = entity.RoomPosition.getY() - Size.getY() + Screen.Y;
			}
			else
			{
				y = entity.RoomPosition.getY() + entity.BigBoundingCircle.Radius / 2 - Screen.Y / 2;
				ey = Screen.Y / 2 - entity.BigBoundingCircle.Radius / 2;
			}

			_onScreen = new Coordinate(x, y);
			return new Coordinate(ex, ey);
		}
	}
}
