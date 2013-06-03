using CirclePhysics.Graphics;
using CirclePhysics.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace CirclePhysics.Entity
{
	public class StaticEntity : Entity
	{
		private BoundingCircle _bigBoundingCircle;
		public BoundingCircle BigBoundingCircle { get { return _bigBoundingCircle; } }

		private BoundingCircle[] _boundingCircles;
		public BoundingCircle[] BoundingCircles { get { return _boundingCircles; } }

		private bool _isSolid;
		public bool IsSolid { get { return _isSolid; } }

		public StaticEntity(Coordinate roomPosition, Dictionary<string, Sprite> sprites)
			: base(roomPosition, sprites)
		{
			_bigBoundingCircle = null;
			_boundingCircles = null;
		}
		public StaticEntity(Coordinate roomPosition, bool isSolid, int radius, Dictionary<string, Sprite> sprites)
			: base(roomPosition, sprites)
		{
			_isSolid = isSolid;

			_bigBoundingCircle = new BoundingCircle(radius, new Coordinate(radius, radius));
			_boundingCircles = null;
		}
		public StaticEntity(Coordinate roomPosition, bool isSolid, BoundingCircle[] boundingCircles, Dictionary<string, Sprite> sprites)
			: base(roomPosition, sprites)
		{
			_isSolid = isSolid;
			_boundingCircles = boundingCircles;

			int xStart = 0;
			int xEnd = 0;
			int yStart = 0;
			int yEnd = 0;
			for (int i = 0; i < boundingCircles.Length; ++i)
			{
				int temp = (int)boundingCircles[i].Center.getX() - boundingCircles[i].Radius;
				if (temp < xStart)
				{ xStart = temp; }

				temp = (int)boundingCircles[i].Center.getX() + boundingCircles[i].Radius;
				if (temp > xEnd)
				{ xEnd = temp; }

				temp = (int)boundingCircles[i].Center.getY() - boundingCircles[i].Radius;
				if (temp < yStart)
				{ yStart = temp; }

				temp = (int)boundingCircles[i].Center.getY() + boundingCircles[i].Radius;
				if (temp > yEnd)
				{ yEnd = temp; }
			}

			int radius = Math.Max((xEnd - xStart) / 2, (yEnd - yStart) / 2);

			_bigBoundingCircle = new BoundingCircle(radius, new Coordinate((xEnd - xStart) / 2, (yEnd - yStart) / 2));
		}

		public override void SetStartingSprite()
		{ SetCurrentSprite("start"); }

		public override void Draw(SpriteBatch spriteBatch)
		{
			if (GetCurrentSprite() != null && OnScreen)
			{
				spriteBatch.Draw(GetCurrentSprite().Image, new Vector2((int)getScreenPosition().getX(),
					(int)getScreenPosition().getY()), Color.White);
			}
		}
	}
}