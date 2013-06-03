using CirclePhysics.Graphics;
using CirclePhysics.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace CirclePhysics.Entity
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

		public override sealed void SetScreenPosition(Coordinate screenPosition)
		{ setPlayerScreenPosition(screenPosition); }

		public void update(GameTime gameTime, KeyboardState newKeyboardState, KeyboardState oldKeyboardState,
			MouseState newMouseState, MouseState oldMouseState, List<StaticEntity> entityList, List<Surface> surfaces)
		{
			GetCurrentSprite().Update();

			if (newKeyboardState.IsKeyDown(Keys.W))
			{ AddVelocity(new GameVector(TopSpeed / 80, -Math.PI / 2)); }
			if (newKeyboardState.IsKeyDown(Keys.S))
			{ AddVelocity(new GameVector(TopSpeed, Math.PI / 2)); }

			if (newKeyboardState.IsKeyDown(Keys.A))
			{
				if (IsOnGround)
				{ AddVelocity(new GameVector(TopSpeed / 80, Math.PI)); }
				else
				{ AddVelocity(new GameVector(TopSpeed / 120, Math.PI)); }
			}

			if (newKeyboardState.IsKeyDown(Keys.D))
			{
				if (IsOnGround)
				{ AddVelocity(new GameVector(TopSpeed / 80, 0)); }
				else
				{ AddVelocity(new GameVector(TopSpeed / 120, 0)); }
			}

			if (IsOnGround && newKeyboardState.IsKeyDown(Keys.Space) && !oldKeyboardState.IsKeyDown(Keys.Space))
			{
				AddVelocity(new GameVector(275, -Math.PI / 2));
				IsOnGround = false;
				_timer = 0;
			}

			if (newKeyboardState.IsKeyDown(Keys.Space) && oldKeyboardState.IsKeyDown(Keys.Space) && _timer >= 0)
			{
				if (_timer > 4000)
				{ _timer = -1; }
				else
				{
					AddVelocity(new GameVector(Velocity.getYLength() + 275, -Math.PI / 2));
					_timer += gameTime.ElapsedGameTime.Milliseconds;
				}
			}

			if (!newKeyboardState.IsKeyDown(Keys.Space) &&
			oldKeyboardState.IsKeyDown(Keys.Space))
			{
				if (_timer != -1)
				{
					AddVelocity(new GameVector(Velocity.getYLength() + 275, -Math.PI / 2));
					_timer = -1;
				}
			}

			if (newMouseState.LeftButton == ButtonState.Pressed)
			{
				if (_shootTimer == _gun.getRateOfFire())
				{ _bullets.Add(_gun.createBullet(RoomPosition, 0)); }
			}

			if (_shootTimer < _gun.getRateOfFire())
			{ ++_shootTimer; }

			for (int i = 0; i < _bullets.Count; ++i)
			{ _bullets[i].update(gameTime, entityList, surfaces, _bullets); }

			base.update(gameTime, entityList, surfaces);
		}
	}
}
