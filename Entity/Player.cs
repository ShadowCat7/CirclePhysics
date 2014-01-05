using System;
using System.Collections.Generic;
using CirclePhysics.Controls;
using CirclePhysics.Graphics;
using CirclePhysics.Physics;

namespace CirclePhysics.Entity
{
	public class Player : MovingEntity
	{
		private const int _TOP_SPEED = 200;
		private const double _FRICTION = 1;

		private double _timer;

		public Player(Coordinate roomPosition, BoundingCircle[] boundingCircles, Dictionary<string, ISprite> sprites, string startingSprite)
			: base(roomPosition, sprites, startingSprite, true, boundingCircles, _TOP_SPEED, _FRICTION, true)
		{
			_timer = -1;
		}

		public override sealed void SetScreenPosition(Coordinate screenPosition)
		{
			SetPlayerScreenPosition(screenPosition);
		}

		public virtual void Update(int elapsedTime, CircleControls oldControls, CircleControls newControls, List<StaticEntity> entityList,
			List<Surface> surfaces)
		{
			CurrentSprite.Update();

			if (newControls.HasFlag(CircleControls.Action))
				OnControlAction();
			if (newControls.HasFlag(CircleControls.Up))
				OnControlUp();
			if (newControls.HasFlag(CircleControls.Down))
				OnControlDown();
			if (newControls.HasFlag(CircleControls.Left))
				OnControlLeft();
			if (newControls.HasFlag(CircleControls.Right))
				OnControlRight();

			if (IsOnGround && newControls.HasFlag(CircleControls.Jump) && !oldControls.HasFlag(CircleControls.Jump))
				OnJump();

			if (newControls.HasFlag(CircleControls.Jump) && oldControls.HasFlag(CircleControls.Jump) && _timer >= 0)
				OnJumpingUp(elapsedTime);

			if (!newControls.HasFlag(CircleControls.Jump) && oldControls.HasFlag(CircleControls.Jump))
				OnCancelingJump();

			base.Update(elapsedTime, entityList, surfaces);
		}

		public virtual void OnControlAction() { }

		public virtual void OnControlUp()
		{
			AddVelocity(new GameVector(TopSpeed / 80, -Math.PI / 2));
		}

		public virtual void OnControlDown()
		{
			AddVelocity(new GameVector(TopSpeed, Math.PI / 2));
		}

		public virtual void OnControlLeft()
		{
			if (IsOnGround)
			{ AddVelocity(new GameVector(TopSpeed / 80, Math.PI)); }
			else
			{ AddVelocity(new GameVector(TopSpeed / 120, Math.PI)); }
		}

		public virtual void OnControlRight()
		{
			if (IsOnGround)
			{ AddVelocity(new GameVector(TopSpeed / 80, 0)); }
			else
			{ AddVelocity(new GameVector(TopSpeed / 120, 0)); }
		}

		public virtual void OnJump()
		{
			AddVelocity(new GameVector(275, -Math.PI / 2));
			IsOnGround = false;
			_timer = 0;
		}

		public virtual void OnJumpingUp(int elapsedTime)
		{
			if (_timer > 4000)
			{ _timer = -1; }
			else
			{
				AddVelocity(new GameVector(Velocity.Y + 275, -Math.PI / 2));
				_timer += elapsedTime;
			}
		}

		public virtual void OnCancelingJump()
		{
			if (_timer != -1)
			{
				AddVelocity(new GameVector(Velocity.Y + 275, -Math.PI / 2));
				_timer = -1;
			}
		}
	}
}
