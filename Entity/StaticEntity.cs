using CirclePhysics.Graphics.Interfaces;
using CirclePhysics.Physics;
using System;
using System.Collections.Generic;
using CirclePhysics.Physics.Interfaces;

namespace CirclePhysics.Entity
{
	public class StaticEntity : CollisionObject, IEntity
	{
		public StaticEntity(Coordinate roomPosition, Dictionary<string, ISprite> sprites, string startingSprite)
			: base(roomPosition, sprites, startingSprite)
		{
			_bigBoundingCircle = null;
			_boundingCircles = null;
		}
		public StaticEntity(Coordinate roomPosition, Dictionary<string, ISprite> sprites, string startingSprite, bool isSolid, int radius)
			: base(roomPosition, sprites, startingSprite)
		{
			_isSolid = isSolid;

			_bigBoundingCircle = new BoundingCircle(radius, new Coordinate(radius, radius));
			_boundingCircles = null;
		}
		public StaticEntity(Coordinate roomPosition, Dictionary<string, ISprite> sprites, string startingSprite, bool isSolid, BoundingCircle[] boundingCircles)
			: base(roomPosition, sprites, startingSprite)
		{
			_isSolid = isSolid;
			_boundingCircles = boundingCircles;

			int xStart = 0;
			int xEnd = 0;
			int yStart = 0;
			int yEnd = 0;
			for (int i = 0; i < boundingCircles.Length; ++i)
			{
				int temp = (int) boundingCircles[i].Center.X - boundingCircles[i].Radius;
				if (temp < xStart)
				{ xStart = temp; }

				temp = (int) boundingCircles[i].Center.X + boundingCircles[i].Radius;
				if (temp > xEnd)
				{ xEnd = temp; }

				temp = (int) boundingCircles[i].Center.Y - boundingCircles[i].Radius;
				if (temp < yStart)
				{ yStart = temp; }

				temp = (int) boundingCircles[i].Center.Y + boundingCircles[i].Radius;
				if (temp > yEnd)
				{ yEnd = temp; }
			}

			int radius = Math.Max((xEnd - xStart) / 2, (yEnd - yStart) / 2);

			_bigBoundingCircle = new BoundingCircle(radius, new Coordinate((xEnd - xStart) / 2.0, (yEnd - yStart) / 2.0));
		}

		private readonly BoundingCircle _bigBoundingCircle;
		public BoundingCircle BigBoundingCircle { get { return _bigBoundingCircle; } }

		private readonly BoundingCircle[] _boundingCircles;
		public BoundingCircle[] BoundingCircles { get { return _boundingCircles; } }

		private readonly bool _isSolid;
		public bool IsSolid { get { return _isSolid; } }
	}
}