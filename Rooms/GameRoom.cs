using System.Collections.Generic;
using CirclePhysics.Controls;
using CirclePhysics.Entity;
using CirclePhysics.Graphics;
using CirclePhysics.Physics;

namespace CirclePhysics.Rooms
{
	public class GameRoom : Room
	{
		private Player _player;

		private Coordinate _onScreen;

		private bool _paused;

		private List<StaticEntity> _entityList;

		private List<Surface> _surfaces;

		public GameRoom(Coordinate size, ISprite background, Player player, List<StaticEntity> entityList, List<Surface> surfaces)
			: base(size, background)
		{
			_player = player;
			_entityList = entityList;
			_surfaces = surfaces;

			_onScreen = new Coordinate(0, 0);
			_paused = false;
		}

		public override void Update(int elapsedTime, CircleControls oldControls, CircleControls newControls)
		{
			if (newControls.HasFlag(CircleControls.Pause) && !oldControls.HasFlag(CircleControls.Pause))
				_paused = !_paused;

			if (!_paused)
			{
				for (int k = 0; k < 10; ++k)
				{
					for (int i = 0; i < _entityList.Count; i++)
					{
						if (!_entityList[i].IsDeleted)
						{
							_entityList[i].Update(elapsedTime);
							_entityList[i].Update(elapsedTime, _entityList, _surfaces);
						}

						if (_entityList[i].IsDeleted)
						{
							_entityList.RemoveAt(i);
							i--;
						}
					}

					_player.Update(elapsedTime, oldControls, newControls, _entityList, _surfaces);
				}

				_player.SetScreenPosition(GetScreenPositionFromEntity(_player));

				for (int i = 0; i < _entityList.Count; ++i)
					_entityList[i].SetScreenPosition(_onScreen);

				for (int i = 0; i < _surfaces.Count; ++i)
					_surfaces[i].SetScreenPosition(_onScreen);
			}
		}

		public override void Draw(IDrawer drawer)
		{
			//TODO background

			for (int i = 0; i < _surfaces.Count; ++i)
				_surfaces[i].Draw(drawer);

			for (int i = 0; i < _entityList.Count; ++i)
				_entityList[i].Draw(drawer);

			_player.Draw(drawer);
		}

		public Coordinate GetScreenPositionFromEntity(StaticEntity entity)
		{
			double x = 0;
			double y = 0;
			double ex = 0;
			double ey = 0;

			double sizeX = ViewPort.X < Size.X ? Size.X : ViewPort.X;
			double sizeY = ViewPort.Y < Size.Y ? Size.Y : ViewPort.Y;

			if (entity.RoomPosition.X + entity.BigBoundingCircle.Radius < ViewPort.X / 2)
			{
				x = 0;
				ex = entity.RoomPosition.X;
			}
			else if (entity.RoomPosition.X + entity.BigBoundingCircle.Radius > sizeX - ViewPort.X / 2)
			{
				x = sizeX - ViewPort.X;
				ex = entity.RoomPosition.X - (sizeX - ViewPort.X);
			}
			else
			{
				x = entity.RoomPosition.X + entity.BigBoundingCircle.Radius - ViewPort.X / 2;
				ex = ViewPort.X / 2 - entity.BigBoundingCircle.Radius;
			}

			if (entity.RoomPosition.Y + entity.BigBoundingCircle.Radius < ViewPort.Y / 2)
			{
				y = 0;
				ey = entity.RoomPosition.Y;
			}
			else if (entity.RoomPosition.Y + entity.BigBoundingCircle.Radius > sizeY - ViewPort.Y / 2)
			{
				y = sizeY - ViewPort.Y;
				ey = entity.RoomPosition.Y - sizeY + ViewPort.Y;
			}
			else
			{
				y = entity.RoomPosition.Y + entity.BigBoundingCircle.Radius - ViewPort.Y / 2;
				ey = ViewPort.Y / 2 - entity.BigBoundingCircle.Radius;
			}

			_onScreen = new Coordinate(x, y);
			return new Coordinate(ex, ey);
		}
	}
}
