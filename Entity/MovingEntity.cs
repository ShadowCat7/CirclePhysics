using CirclePhysics.Graphics.Interfaces;
using CirclePhysics.Physics;
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

		public MovingEntity(Coordinate roomPosition, Dictionary<string, ISprite> sprites, string startingSprite, int topSpeed)
			: base(roomPosition, sprites, startingSprite)
		{
			TopSpeed = topSpeed;

			Velocity = new GameVector(0, 0);
			Friction = -1;
			IsGravityOn = false;
		}

		public MovingEntity(Coordinate roomPosition, Dictionary<string, ISprite> sprites, string startingSprite, bool solid, int radius, int topSpeed)
			: base(roomPosition, sprites, startingSprite, solid, radius)
		{
			TopSpeed = topSpeed;

			Velocity = new GameVector(0, 0);
			Friction = -1;
			IsGravityOn = false;
		}

		public MovingEntity(Coordinate roomPosition, Dictionary<string, ISprite> sprites, string startingSprite, bool solid, BoundingCircle[] boundingCircles, int topSpeed)
			: base(roomPosition, sprites, startingSprite, solid, boundingCircles)
		{
			TopSpeed = topSpeed;

			Velocity = new GameVector(0, 0);
			Friction = -1;
			IsGravityOn = false;
		}

		public MovingEntity(Coordinate roomPosition, Dictionary<string, ISprite> sprites, string startingSprite, int radius,
			int topSpeed, double friction, bool isGravityOn)
			: base(roomPosition, sprites, startingSprite, true, radius)
		{
			TopSpeed = topSpeed;
			Friction = friction;
			IsGravityOn = isGravityOn;

			Velocity = new GameVector(0, 0);
		}

		public MovingEntity(Coordinate roomPosition, Dictionary<string, ISprite> sprites, string startingSprite, BoundingCircle[] boundingCircles,
			int topSpeed, double friction, bool isGravityOn)
			: base(roomPosition, sprites, startingSprite, true, boundingCircles)
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
				{ Velocity = new Coordinate(TopSpeed * Velocity.X.GetSign(), Velocity.Y).ToGameVector(); }
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

				double addX = Velocity.Y * gameTime * 0.0001;
				double addY = Velocity.Y * gameTime * 0.0001;

				Coordinate add = new Coordinate(addX, addY);

				Move(new Coordinate(addX, addY));
				addX = addY = 0;

				bool onGround = false;

				if (IsSolid && gameTime != 0)// && _velocity.getMagnitude() != 0
				{
					for (int i = 0; i < entityList.Count; ++i)
					{
						if (this != entityList[i] && entityList[i].IsSolid)
						{
							Coordinate moveTo = Collision.FindColliding(this, entityList[i]);
							if (moveTo != null)
							{
								addX = moveTo.X;
								addY = moveTo.Y;

								if (Math.Abs(addX - add.X) < 0.0001)
								{ addX = add.X; }
								if (Math.Abs(addY - add.Y) < 0.0001)
								{ addY = add.Y; }
								Move(new Coordinate(0, 0) - moveTo);

								if (addY > 0.0001)
								{ onGround = true; }
							}
						}
					}

					for (int i = 0; i < surfaces.Count; ++i)
					{
						Coordinate moveTo = Collision.FindColliding(this, surfaces[i]);
						if (moveTo != null)
						{
							addX = moveTo.X;
							addY = moveTo.Y;

							if (Math.Abs(addX - add.X) < 0.0001)
							{ addX = add.X; }
							if (Math.Abs(addY - add.Y) < 0.0001)
							{ addY = add.Y; }
							Move(new Coordinate(0, 0) - moveTo);

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
	}
}