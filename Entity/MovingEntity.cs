using CirclePhysics.Graphics.Interfaces;
using CirclePhysics.Physics;
using System;
using System.Collections.Generic;

namespace CirclePhysics.Entity
{
	public class MovingEntity : StaticEntity
	{
		public bool IsOnGround { get; protected set; }
		public int TopSpeed { get; private set; }

		public GameVector Velocity { get; private set; }
		protected void AddVelocity(GameVector velocity)
		{ Velocity += velocity; }

		private double _friction;
		public double Friction { get { return _friction; } }

		private bool _isGravityOn;
		public bool IsGravityOn { get { return _isGravityOn; } }

		public MovingEntity(Coordinate roomPosition, Dictionary<string, ISprite> sprites, string startingSprite, int topSpeed)
			: base(roomPosition, sprites, startingSprite)
		{
			TopSpeed = topSpeed;

			Velocity = new GameVector(0, 0);
			_friction = -1;
			_isGravityOn = false;
		}

		public MovingEntity(Coordinate roomPosition, Dictionary<string, ISprite> sprites, string startingSprite, bool solid, int radius, int topSpeed)
			: base(roomPosition, sprites, startingSprite, solid, radius)
		{
			TopSpeed = topSpeed;

			Velocity = new GameVector(0, 0);
			_friction = -1;
			_isGravityOn = false;
		}

		public MovingEntity(Coordinate roomPosition, Dictionary<string, ISprite> sprites, string startingSprite, bool solid, BoundingCircle[] boundingCircles, int topSpeed)
			: base(roomPosition, sprites, startingSprite, solid, boundingCircles)
		{
			TopSpeed = topSpeed;

			Velocity = new GameVector(0, 0);
			_friction = -1;
			_isGravityOn = false;
		}

		public MovingEntity(Coordinate roomPosition, Dictionary<string, ISprite> sprites, string startingSprite, int radius,
			int topSpeed, double friction, bool isGravityOn)
			: base(roomPosition, sprites, startingSprite, true, radius)
		{
			TopSpeed = topSpeed;
			_friction = friction;
			_isGravityOn = isGravityOn;

			Velocity = new GameVector(0, 0);
		}

		public MovingEntity(Coordinate roomPosition, Dictionary<string, ISprite> sprites, string startingSprite, BoundingCircle[] boundingCircles,
			int topSpeed, double friction, bool isGravityOn)
			: base(roomPosition, sprites, startingSprite, true, boundingCircles)
		{
			TopSpeed = topSpeed;
			_friction = friction;
			_isGravityOn = isGravityOn;

			Velocity = new GameVector(0, 0);
		}

		protected override sealed void Move(Coordinate newPosition)
		{ base.Move(new Coordinate(RoomPosition.X + newPosition.X, RoomPosition.Y + newPosition.Y)); }

		protected void checkVelocity()
		{
			if (!_isGravityOn)
			{
				if (Velocity.Magnitude > TopSpeed)
				{ Velocity = new GameVector(TopSpeed, Velocity.Direction); }
			}
			else
			{
				if (Math.Abs(Velocity.getXLength()) > TopSpeed)
				{
					Velocity = GameVector.makeFromCoordinate(new Coordinate(TopSpeed * Math.Abs(Velocity.getXLength()) / Velocity.getXLength(),
					  Velocity.getYLength()));
				}
				//if (_velocity.getYLength() < -_topSpeed)
				//{
				//    _velocity = GameVector.makeFromCoordinate(new Coordinate(_velocity.getXLength(), -_topSpeed));
				//}
			}
		}

		public override void update(GameTime gameTime, List<StaticEntity> entityList, List<Surface> surfaces)
		{
			if (gameTime.ElapsedGameTime.Milliseconds != 0)
			{
				checkVelocity();

				if (_isGravityOn)
				{ Velocity = Velocity + new GameVector(1, Math.PI / 2); }

				if (IsOnGround && Velocity.Magnitude < 1.5)
				{ Velocity = new GameVector(0, Velocity.Direction); }

				double addX = Velocity.getXLength() * gameTime.ElapsedGameTime.Milliseconds * 0.0001;
				double addY = Velocity.getYLength() * gameTime.ElapsedGameTime.Milliseconds * 0.0001;

				Coordinate add = new Coordinate(addX, addY);

				move(new Coordinate(addX, addY));
				addX = addY = 0;

				bool onGround = false;

				if (IsSolid && gameTime.ElapsedGameTime.Milliseconds != 0)// && _velocity.getMagnitude() != 0
				{
					for (int i = 0; i < entityList.Count; ++i)
					{
						if (this != entityList[i] && entityList[i].IsSolid)
						{
							Coordinate moveTo = Collision.findColliding(this, entityList[i]);
							if (moveTo != null)
							{
								addX = moveTo.getX();
								addY = moveTo.getY();

								if (Math.Abs(addX - add.getX()) < 0.0001)
								{ addX = add.getX(); }
								if (Math.Abs(addY - add.getY()) < 0.0001)
								{ addY = add.getY(); }
								move(new Coordinate(0, 0) - moveTo);

								if (addY > 0.0001)
								{ onGround = true; }
							}
						}
					}

					for (int i = 0; i < surfaces.Count; ++i)
					{
						Coordinate moveTo = Collision.findColliding(this, surfaces[i]);
						if (moveTo != null)
						{
							addX = moveTo.getX();
							addY = moveTo.getY();

							if (Math.Abs(addX - add.getX()) < 0.0001)
							{ addX = add.getX(); }
							if (Math.Abs(addY - add.getY()) < 0.0001)
							{ addY = add.getY(); }
							move(new Coordinate(0, 0) - moveTo);

							if (addY > 0.0001)
							{ onGround = true; }
						}
					}
				}

				IsOnGround = onGround; //TODO onGround should always be true if gravityOn is false

				addX = add.getX() - addX;
				addY = add.getY() - addY;

				if (Math.Abs(addX) < 0.0001)
				{ addX = 0; }
				if (Math.Abs(addY) < 0.0001)
				{ addY = 0; }
				addX /= gameTime.ElapsedGameTime.Milliseconds * 0.0001;
				addY /= gameTime.ElapsedGameTime.Milliseconds * 0.0001;
				Velocity = GameVector.makeFromCoordinate(new Coordinate(addX, addY));

				if (_friction != -1 && IsOnGround && Velocity.Magnitude > _friction)
				{ Velocity = new GameVector(Velocity.Magnitude - _friction, Velocity.Direction); }
			}
		}
	}
}