using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TimeTravel
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
            addVelocity(new GameVector(topSpeed, direction));
            _damage = damage;
            _rateOfFire = rateOfFire;
        }

        public Bullet(Coordinate roomPosition, int radius, Dictionary<string, Sprite> sprites, int topSpeed, double friction, bool gravityOn,
            GameVector startVelocity, int damage, int rateOfFire)
            : base(roomPosition, radius, sprites, topSpeed, friction, gravityOn)
        {
            addVelocity(startVelocity);
            _damage = damage;
            _rateOfFire = rateOfFire;
        }

        public void update(GameTime gameTime, List<StaticEntity> entityList, List<Surface> surfaces, List<Bullet> bullets)
        {
            checkVelocity();
            move(Coordinate.makeFromVector(getVelocity()));

            bool collided = false;

            for (int i = 0; !collided && i < entityList.Count; ++i)
            {
                if (this != entityList[i] && entityList[i].getSolid())
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