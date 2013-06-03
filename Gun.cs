using CirclePhysics.Entity;
using CirclePhysics.Graphics;
using CirclePhysics.Physics;
using System.Collections.Generic;

namespace CirclePhysics
{
	public class Gun : Overlay
	{
		private Bullet _bullet;
		public int getRateOfFire()
		{ return _bullet.getRateOfFire(); }

		public Gun(Coordinate screenPosition, Dictionary<string, Sprite> sprites, Bullet bullet)
			: base(screenPosition, sprites)
		{ _bullet = bullet; }

		public override void SetStartingSprite()
		{ SetCurrentSprite("right"); }

		public Bullet createBullet(Coordinate roomPosition, double direction)
		{
			if (_bullet.Friction == -1)
			{ // TODO _bullet.getSprites == wrong
				return new Bullet(roomPosition, _bullet.BigBoundingCircle.Radius, _bullet.Sprites,
					_bullet.TopSpeed, direction, _bullet.getDamage(), _bullet.getRateOfFire());
			}
			else
			{
				return new Bullet(roomPosition, _bullet.BigBoundingCircle.Radius, _bullet.Sprites, _bullet.TopSpeed,
					_bullet.Friction, _bullet.IsGravityOn, new GameVector(_bullet.Velocity.Magnitude, direction),
					_bullet.getDamage(), _bullet.getRateOfFire());
			}
		}
	}
}
