using CirclePhysics.Graphics.Interfaces;
using CirclePhysics.Physics;
using CirclePhysics.Physics.Interfaces;
using System;
using System.Collections.Generic;
using CirclePhysics.Utility;

namespace CirclePhysics.Entity
{
	public class MovingEntity : StaticEntity
	{
		public bool IsOnGround { get; protected set; }
		public int TopSpeed { get; private set; }

		public GameVector Velocity { get; private set; }
		protected void AddVelocity(GameVector velocity)
		{ Velocity += velocity; }

		public double Friction { get; private set; }

		public bool IsGravityOn { get; private set; }

		public MovingEntity(Coordinate roomPosition, Dictionary<string, ISprite> sprites, string startingSprite, bool isSolid, int radius,
			int topSpeed, double friction = -1, bool isGravityOn = false)
			: base(roomPosition, sprites, startingSprite, isSolid, radius)
		{
			TopSpeed = topSpeed;
			Friction = friction;
			IsGravityOn = isGravityOn;

			Velocity = new GameVector(0, 0);
		}

		public MovingEntity(Coordinate roomPosition, Dictionary<string, ISprite> sprites, string startingSprite, bool isSolid,
			BoundingCircle[] boundingCircles, int topSpeed, double friction = -1, bool isGravityOn = false)
			: base(roomPosition, sprites, startingSprite, isSolid, boundingCircles)
		{
			TopSpeed = topSpeed;
			Friction = friction;
			IsGravityOn = isGravityOn;

			Velocity = new GameVector(0, 0);
		}

		protected override sealed void Move(Coordinate newPosition)
		{ base.Move(new Coordinate(RoomPosition.X + newPosition.X, RoomPosition.Y + newPosition.Y)); }

		protected void CheckVelocity()
		{
			if (!IsGravityOn)
			{
				if (Velocity.Magnitude > TopSpeed)
				{ Velocity = new GameVector(TopSpeed, Velocity.Direction); }
			}
			else
			{
				if (Math.Abs(Velocity.X) > TopSpeed)
				{ Velocity = new Coordinate(TopSpeed * Math.Sign(Velocity.X), Velocity.Y).ToGameVector(); }
				//if (_velocity.getYLength() < -_topSpeed)
				//{
				//    _velocity = GameVector.makeFromCoordinate(new Coordinate(_velocity.getXLength(), -_topSpeed));
				//}
			}
		}

		public override void Update(int gameTime, List<StaticEntity> entityList, List<Surface> surfaces)
		{
			if (gameTime != 0)
			{
				CheckVelocity();

				if (IsGravityOn)
				{ Velocity = Velocity + new GameVector(1, Math.PI / 2); }

				if (IsOnGround && Velocity.Magnitude < 1.5)
				{ Velocity = new GameVector(0, Velocity.Direction); }

				Coordinate add = new Coordinate(Velocity.X * gameTime * 0.0001, Velocity.Y * gameTime * 0.0001);

				Move(add);
				double addX = 0;
				double addY = 0;

				bool onGround = false;

				if (IsSolid)// && _velocity.getMagnitude() != 0
				{
					for (int i = 0; i < entityList.Count; ++i)
					{
						if (!object.ReferenceEquals(this, entityList[i]) && entityList[i].IsSolid)
						{
							Coordinate resultCoordinate = CheckCollision(entityList[i], add);
							if (resultCoordinate != null)
							{
								addX = resultCoordinate.X;
								addY = resultCoordinate.Y;

								if (addY > 0.0001)
								{ onGround = true; }
							}
						}
					}

					for (int i = 0; i < surfaces.Count; ++i)
					{
						Coordinate resultCoordinate = CheckCollision(surfaces[i], add);
						if (resultCoordinate != null)
						{
							addX = resultCoordinate.X;
							addY = resultCoordinate.Y;

							if (addY > 0.0001)
							{ onGround = true; }
						}
					}
				}

				IsOnGround = onGround; //TODO onGround should always be true if gravityOn is false

				addX = add.X - addX;
				addY = add.Y - addY;

				if (Math.Abs(addX) < 0.0001)
				{ addX = 0; }
				if (Math.Abs(addY) < 0.0001)
				{ addY = 0; }
				addX /= gameTime * 0.0001;
				addY /= gameTime * 0.0001;
				Velocity = new Coordinate(addX, addY).ToGameVector();

				if (Friction != -1 && IsOnGround && Velocity.Magnitude > Friction)
				{ Velocity = new GameVector(Velocity.Magnitude - Friction, Velocity.Direction); }
			}
		}

		private Coordinate CheckCollision(ICollidable collideWith, Coordinate add)
		{
			double x, y;

			Coordinate moveTo = Collision.FindColliding(this, collideWith);
			if (moveTo != null)
			{
				x = moveTo.X;
				y = moveTo.Y;

				if (Math.Abs(x - add.X) < 0.0001)
				{ x = add.X; }
				if (Math.Abs(y - add.Y) < 0.0001)
				{ y = add.Y; }
				Move(new Coordinate(0, 0) - moveTo);

				return new Coordinate(x, y);
			}
			else
			{ return null; }
		}
	}
}