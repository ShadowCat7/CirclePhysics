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
    public class MovingEntity : Entity
    {
        private bool _onGround;
        public bool getOnGround()
        { return _onGround; }

        private double _topSpeed;
        public double getTopSpeed()
        { return _topSpeed; }

        private GameVector _velocity;
        public GameVector getVelocity()
        { return _velocity; }
        protected void addVelocity(GameVector velocity)
        { _velocity += velocity; }

        private double _friction;
        public double getFriction()
        { return _friction; }

        private bool _gravityOn;
        public bool getGravityOn()
        { return _gravityOn; }

        public MovingEntity(Coordinate roomPosition, Dictionary<string, Sprite> sprites, int topSpeed)
            : base(roomPosition, sprites)
        {
            _topSpeed = topSpeed;

            _velocity = new GameVector(0, 0);
            _friction = -1;
            _gravityOn = false;
        }

        public MovingEntity(Coordinate roomPosition, bool solid, BoundingCircle boundingCircle, Dictionary<string, Sprite> sprites, int topSpeed)
            : base(roomPosition, solid, boundingCircle, sprites)
        {
            _topSpeed = topSpeed;

            _velocity = new GameVector(0, 0);
            _friction = -1;
            _gravityOn = false;
        }

        public MovingEntity(Coordinate roomPosition, BoundingCircle boundingCircle, Dictionary<string, Sprite> sprites, 
            int topSpeed, double friction, bool gravityOn)
            : base(roomPosition, true, boundingCircle, sprites)
        {
            _topSpeed = topSpeed;
            _friction = friction;
            _gravityOn = gravityOn;

            _velocity = new GameVector(0, 0);
        }

        protected override void move(Coordinate newPosition)
        { base.move(new Coordinate(getRoomPosition().getX() + newPosition.getX(), getRoomPosition().getY() + newPosition.getY())); }

        private void checkVelocity()
        {
            if (!_gravityOn)
            {
                if (_velocity.getMagnitude() > _topSpeed)
                { _velocity = new GameVector(_topSpeed, _velocity.getDirection()); }
            }
            else
            {
                if (Math.Abs(_velocity.getXLength()) > _topSpeed)
                { _velocity = GameVector.makeFromCoordinate(new Coordinate(_topSpeed * Math.Abs(_velocity.getXLength()) / _velocity.getXLength(), 
                    _velocity.getYLength())); }
                if (_velocity.getYLength() < -_topSpeed)
                { _velocity = GameVector.makeFromCoordinate(new Coordinate(_velocity.getXLength(), -_topSpeed)); }
            }
        }

        public override void update(GameTime gameTime, List<Entity> entityList, List<Surface> surfaces)
        {
            if (gameTime.ElapsedGameTime.Milliseconds != 0)
            {
                checkVelocity();

                double addX = _velocity.getXLength() * gameTime.ElapsedGameTime.Milliseconds * 0.0001;
                double addY = _velocity.getYLength() * gameTime.ElapsedGameTime.Milliseconds * 0.0001;

                if (_gravityOn)
                { addY += 0.002; }

                Coordinate add = new Coordinate(addX, addY);

                move(new Coordinate(addX, addY));
                addX = addY = 0;

                if (getSolid())
                {
                    if (gameTime.ElapsedGameTime.Milliseconds != 0)// && _velocity.getMagnitude() != 0
                    {
                        for (int i = 0; i < entityList.Count; ++i)
                        {
                            if (this != entityList[i] && entityList[i].getSolid())
                            {
                                Coordinate moveTo = Collision.test(this, entityList[i]);
                                if (moveTo != null)
                                {
                                    addX = moveTo.getX();
                                    addY = moveTo.getY();
                                }
                            }
                        }

                        for (int i = 0; i < surfaces.Count; ++i)
                        {
                            Coordinate moveTo = Collision.test(this, surfaces[i]);
                            if (moveTo != null)
                            {
                                addX = moveTo.getX();
                                addY = moveTo.getY();
                            }
                        }
                    }
                }

                if (Math.Abs(addY) > 0)
                { _onGround = true; }
                else
                { _onGround = false; }

                if (addY < -1)
                { int asdf = 0; }

                move(new Coordinate(-addX, -addY));

                addX = add.getX() - addX;
                addY = add.getY() - addY;
                addX /= gameTime.ElapsedGameTime.Milliseconds * 0.0001;
                addY /= gameTime.ElapsedGameTime.Milliseconds * 0.0001;
                _velocity = GameVector.makeFromCoordinate(new Coordinate(addX, addY));

                //if (_friction != -1)
                //{ _velocity = new GameVector(_velocity.getMagnitude() - _friction, _velocity.getDirection()); }
            }
        }
    }
}
