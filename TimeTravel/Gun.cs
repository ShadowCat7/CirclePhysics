using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeTravel
{
    public class Gun : Overlay
    {
        private Bullet _bullet;
        public int getRateOfFire()
        { return _bullet.getRateOfFire(); }

        public Gun(Coordinate screenPosition, Dictionary<string, Sprite> sprites, Bullet bullet)
            : base(screenPosition, sprites)
        { _bullet = bullet; }

        public override void setStartingSprite()
        { setCurrentSprite("right"); }

        public Bullet createBullet(Coordinate roomPosition, double direction)
        {
            if (_bullet.getFriction() == -1)
            {
                return new Bullet(roomPosition, _bullet.getBigBoundingCircle().getRadius(), _bullet.getSprites(),
                  _bullet.getTopSpeed(), direction, _bullet.getDamage(), _bullet.getRateOfFire());
            }
            else
            {
                return new Bullet(roomPosition, _bullet.getBigBoundingCircle().getRadius(), _bullet.getSprites(), _bullet.getTopSpeed(),
                  _bullet.getFriction(), _bullet.getGravityOn(), new GameVector(_bullet.getVelocity().getMagnitude(), direction), 
                  _bullet.getDamage(), _bullet.getRateOfFire());
            }
        }
    }
}
