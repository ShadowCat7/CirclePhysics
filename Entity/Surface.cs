using System.Collections.Generic;
using CirclePhysics.Graphics;
using CirclePhysics.Physics;

namespace CirclePhysics.Entity
{
	public class Surface : CollisionObject, ISurface
	{
		public Coordinate EndCoordinate { get; private set; }
		public Coordinate StartCoordinate { get; private set; }

		public Surface(Coordinate roomPosition, Dictionary<string, ISprite> sprites, string startingSprite, Coordinate startCoordinate, Coordinate endCoordinate)
			: base(roomPosition, sprites, startingSprite)
		{
			EndCoordinate = endCoordinate;
			StartCoordinate = startCoordinate;
		}
	}
}
