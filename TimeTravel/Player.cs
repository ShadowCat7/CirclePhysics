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
    public class Player : MovingEntity
    {
        private const int _TOP_SPEED = 200;
        private const double _FRICTION = 1;

        private bool horizontalFacingRight;
        private double verticalFacing; //Up is -1, Down is 1, else 0

        private double _timer;
        private double _shootTimer;

        private Gun _gun;
        private List<Bullet> _bullets;

        public Player(Coordinate roomPosition, BoundingCircle[] boundingCircles, Dictionary<string, Sprite> sprites, Gun gun)
            : base(roomPosition, boundingCircles, sprites, _TOP_SPEED, _FRICTION, true)
        {
            horizontalFacingRight = true;
            verticalFacing = 0;
            _gun = gun;

            _timer = -1;
            _shootTimer = 0;
            _bullets = new List<Bullet>();
        }

        public override void setScreenPosition(Coordinate screenPosition)
        { setPlayerScreenPosition(screenPosition); }

        public void update(GameTime gameTime, KeyboardState newKeyboardState, KeyboardState oldKeyboardState, 
            MouseState newMouseState, MouseState oldMouseState, List<StaticEntity> entityList, List<Surface> surfaces)
        {
            getCurrentSprite().update();

            if (newKeyboardState.IsKeyDown(Keys.W))
            { addVelocity(new GameVector(getTopSpeed() / 80, -Math.PI / 2)); }
            if (newKeyboardState.IsKeyDown(Keys.S))
            { addVelocity(new GameVector(getTopSpeed(), Math.PI / 2)); }

            if (newKeyboardState.IsKeyDown(Keys.A))
            {
                if (getOnGround())
                { addVelocity(new GameVector(getTopSpeed() / 80, Math.PI)); }
                else
                { addVelocity(new GameVector(getTopSpeed() / 120, Math.PI)); }
            }

            if (newKeyboardState.IsKeyDown(Keys.D))
            {
                if (getOnGround())
                { addVelocity(new GameVector(getTopSpeed() / 80, 0)); }
                else
                { addVelocity(new GameVector(getTopSpeed() / 120, 0)); }
            }

            if (getOnGround() && newKeyboardState.IsKeyDown(Keys.Space) && !oldKeyboardState.IsKeyDown(Keys.Space))
            {
                addVelocity(new GameVector(275, -Math.PI / 2));
                setOnGround(false);
                _timer = 0;
            }

            if (newKeyboardState.IsKeyDown(Keys.Space) && oldKeyboardState.IsKeyDown(Keys.Space) && _timer >= 0)
            {
                if (_timer > 4000)
                { _timer = -1; }
                else
                {
                    addVelocity(new GameVector(getVelocity().getYLength() + 275, -Math.PI / 2));
                    _timer += gameTime.ElapsedGameTime.Milliseconds;
                }
            }

            if (!newKeyboardState.IsKeyDown(Keys.Space) &&
            oldKeyboardState.IsKeyDown(Keys.Space))
            {
                if (_timer != -1)
                {
                    addVelocity(new GameVector(getVelocity().getYLength() + 275, -Math.PI / 2));
                    _timer = -1;
                }
            }

            if (newMouseState.LeftButton == ButtonState.Pressed)
            {
                if (_shootTimer == _gun.getRateOfFire())
                { _bullets.Add(_gun.createBullet(getRoomPosition(), 0)); }
            }

            if (_shootTimer < _gun.getRateOfFire())
            { ++_shootTimer; }

            for (int i = 0; i < _bullets.Count; ++i)
            { _bullets[i].update(gameTime, entityList, surfaces, _bullets); }

            base.update(gameTime, entityList, surfaces);
        }
    }
}
