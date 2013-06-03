using CirclePhysics.Graphics;
using CirclePhysics.Physics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace CirclePhysics.Entity
{
	public class MovingEntity : StaticEntity
	{
		private bool _isOnGround;
		public bool IsOnGround
		{
			get
			{ return _isOnGround; }
			protected set
			{ _isOnGround = value; }
		}

		private int _topSpeed;
		public int TopSpeed { get { return _topSpeed; } }

		private GameVector _velocity;
		public GameVector Velocity { get { return _velocity; } }
		protected void AddVelocity(GameVector velocity)
		{ _velocity += velocity; }

		private double _friction;
		public double Friction { get { return _friction; } }

		private bool _isGravityOn;
		public bool IsGravityOn { get { return _isGravityOn; } }

		public MovingEntity(Coordinate roomPosition, Dictionary<string, Sprite> sprites, int topSpeed)
			: base(roomPosition, sprites)
		{
			_topSpeed = topSpeed;

			_velocity = new GameVector(0, 0);
			_friction = -1;
			_isGravityOn = false;
		}

		public MovingEntity(Coordinate roomPosition, bool solid, int radius, Dictionary<string, Sprite> sprites, int topSpeed)
			: base(roomPosition, solid, radius, sprites)
		{
			_topSpeed = topSpeed;

			_velocity = new GameVector(0, 0);
			_friction = -1;
			_isGravityOn = false;
		}

		public MovingEntity(Coordinate roomPosition, bool solid, BoundingCircle[] boundingCircles, Dictionary<string, Sprite> sprites, int topSpeed)
			: base(roomPosition, solid, boundingCircles, sprites)
		{
			_topSpeed = topSpeed;

			_velocity = new GameVector(0, 0);
			_friction = -1;
			_isGravityOn = false;
		}

		public MovingEntity(Coordinate roomPosition, int radius, Dictionary<string, Sprite> sprites,
			int topSpeed, double friction, bool isGravityOn)
			: base(roomPosition, true, radius, sprites)
		{
			_topSpeed = topSpeed;
			_friction = friction;
			_isGravityOn = isGravityOn;

			_velocity = new GameVector(0, 0);
		}

		public MovingEntity(Coordinate roomPosition, BoundingCircle[] boundingCircles, Dictionary<string, Sprite> sprites,
			int topSpeed, double friction, bool isGravityOn)
			: base(roomPosition, true, boundingCircles, sprites)
		{
			_topSpeed = topSpeed;
			_friction = friction;
			_isGravityOn = isGravityOn;

			_velocity = new GameVector(0, 0);
		}

		protected override void move(Coordinate newPosition)
		{ base.move(new Coordinate(RoomPosition.getX() + newPosition.getX(), RoomPosition.getY() + newPosition.getY())); }

		protected void checkVelocity()
		{
			if (!_isGravityOn)
			{
				if (_velocity.Magnitude > _topSpeed)
				{ _velocity = new GameVector(_topSpeed, _velocity.Direction); }
			}
			else
			{
				if (Math.Abs(_velocity.getXLength()) > _topSpeed)
				{
					_velocity = GameVector.makeFromCoordinate(new Coordinate(_topSpeed * Math.Abs(_velocity.getXLength()) / _velocity.getXLength(),
					  _velocity.getYLength()));
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
				{ _velocity = _velocity + new GameVector(1, Math.PI / 2); }

				if (_isOnGround && _velocity.Magnitude < 1.5)
				{ _velocity = new GameVector(0, _velocity.Direction); }

				double addX = _velocity.getXLength() * gameTime.ElapsedGameTime.Milliseconds * 0.0001;
				double addY = _velocity.getYLength() * gameTime.ElapsedGameTime.Milliseconds * 0.0001;

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

				_isOnGround = onGround; //TODO onGround should always be true if gravityOn is false

				addX = add.getX() - addX;
				addY = add.getY() - addY;

				if (Math.Abs(addX) < 0.0001)
				{ addX = 0; }
				if (Math.Abs(addY) < 0.0001)
				{ addY = 0; }
				addX /= gameTime.ElapsedGameTime.Milliseconds * 0.0001;
				addY /= gameTime.ElapsedGameTime.Milliseconds * 0.0001;
				_velocity = GameVector.makeFromCoordinate(new Coordinate(addX, addY));

				if (_friction != -1 && _isOnGround && _velocity.Magnitude > _friction)
				{ _velocity = new GameVector(_velocity.Magnitude - _friction, _velocity.Direction); }
			}
		}
	}
}