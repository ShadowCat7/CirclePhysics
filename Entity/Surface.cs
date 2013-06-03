using CirclePhysics.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace CirclePhysics.Entity
{
	public class Surface : Entity
	{
		private Coordinate _endCoordinate;
		public Coordinate EndCoordinate { get { return _endCoordinate; } }

		private Coordinate _startCoordinate;
		public Coordinate StartCoordinate { get { return _startCoordinate; } }

		public Surface(Coordinate roomPosition, Dictionary<string, Sprite> sprites, Coordinate startCoordinate, Coordinate endCoordinate)
			: base(roomPosition, sprites)
		{
			_endCoordinate = endCoordinate;
			_startCoordinate = startCoordinate;
		}

		public override void SetStartingSprite()
		{
			if (getSpriteFromDict("start") != null)
			{
				SetCurrentSprite("start");
			}
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			if (GetCurrentSprite() != null)
			{
				spriteBatch.Draw(GetCurrentSprite().Image, new Vector2((int)(RoomPosition.getX()), (int)(RoomPosition.getY())), Color.White);
			}
		}
	}
}
