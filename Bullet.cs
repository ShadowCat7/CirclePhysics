using CirclePhysics.Entity;
using CirclePhysics.Graphics;
using CirclePhysics.Physics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace CirclePhysics
{
	public class Bullet : MovingEntity
	{
		private int _damage;
		public int getDamage()
		{ return _damage; }

		private int _rateOfFire;
		public int getRateOfFire()
		{ return _rateOfFire; }

		public Bullet(Coordinate roomPosition, int radius, Dictionary<string, Sprite> sprites, int topSpeed,
			double direction, int damage, int rateOfFire)
			: base(roomPosition, true, radius, sprites, topSpeed)
		{
			AddVelocity(new GameVector(topSpeed, direction));
			_damage = damage;
			_rateOfFire = rateOfFire;
		}

		public Bullet(Coordinate roomPosition, int radius, Dictionary<string, Sprite> sprites, int topSpeed, double friction, bool gravityOn,
			GameVector startVelocity, int damage, int rateOfFire)
			: base(roomPosition, radius, sprites, topSpeed, friction, gravityOn)
		{
			AddVelocity(startVelocity);
			_damage = damage;
			_rateOfFire = rateOfFire;
		}

		public void update(GameTime gameTime, List<StaticEntity> entityList, List<Surface> surfaces, List<Bullet> bullets)
		{
			checkVelocity();
			move(Coordinate.makeFromVector(Velocity));

			bool collided = false;

			for (int i = 0; !collided && i < entityList.Count; ++i)
			{
				if (this != entityList[i] && entityList[i].IsSolid)
				{
					if (Collision.test(this, entityList[i]))
					{
						entityList[i].damaged(_damage);
						bullets.Remove(this);
						collided = true;
					}
				}
			}
		}
	}
}