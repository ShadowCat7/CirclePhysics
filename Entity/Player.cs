using CirclePhysics.Graphics;
using CirclePhysics.Physics;
using System;
using System.Collections.Generic;
using CirclePhysics.Graphics.Interfaces;
using CirclePhysics.Controls;

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

		public Player(Coordinate roomPosition, BoundingCircle[] boundingCircles, Dictionary<string, ISprite> sprites, string startingSprite)
			: base(roomPosition, sprites, startingSprite, true, boundingCircles, _TOP_SPEED, _FRICTION, true)
		{
			horizontalFacingRight = true;
			verticalFacing = 0;

			_timer = -1;
			_shootTimer = 0;
		}

		public override sealed void SetScreenPosition(Coordinate screenPosition)
		{ SetPlayerScreenPosition(screenPosition); }

		public void update(int elapsedTime, CircleControls oldControls, CircleControls newControls, List<StaticEntity> entityList,
			List<Surface> surfaces)
		{
			CurrentSprite.Update();

			if (newControls.HasFlag(CircleControls.Up))
			{ AddVelocity(new GameVector(TopSpeed / 80, -Math.PI / 2)); }
			if (newControls.HasFlag(CircleControls.Down))
			{ AddVelocity(new GameVector(TopSpeed, Math.PI / 2)); }

			if (newControls.HasFlag(CircleControls.Left))
			{
				if (IsOnGround)
				{ AddVelocity(new GameVector(TopSpeed / 80, Math.PI)); }
				else
				{ AddVelocity(new GameVector(TopSpeed / 120, Math.PI)); }
			}

			if (newControls.HasFlag(CircleControls.Right))
			{
				if (IsOnGround)
				{ AddVelocity(new GameVector(TopSpeed / 80, 0)); }
				else
				{ AddVelocity(new GameVector(TopSpeed / 120, 0)); }
			}

			if (IsOnGround && newControls.HasFlag(CircleControls.Jump) && !oldControls.HasFlag(CircleControls.Jump))
			{
				AddVelocity(new GameVector(275, -Math.PI / 2));
				IsOnGround = false;
				_timer = 0;
			}

			if (newControls.HasFlag(CircleControls.Jump) && oldControls.HasFlag(CircleControls.Jump) && _timer >= 0)
			{
				if (_timer > 4000)
				{ _timer = -1; }
				else
				{
					AddVelocity(new GameVector(Velocity.Y + 275, -Math.PI / 2));
					_timer += elapsedTime;
				}
			}

			if (!newControls.HasFlag(CircleControls.Jump) && oldControls.HasFlag(CircleControls.Jump))
			{
				if (_timer != -1)
				{
					AddVelocity(new GameVector(Velocity.Y + 275, -Math.PI / 2));
					_timer = -1;
				}
			}

			base.Update(elapsedTime, entityList, surfaces);
		}
	}
}
