using CirclePhysics.Graphics;
using CirclePhysics.Physics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace CirclePhysics.Entity
{
	public class MovingSurface : Surface
	{
		private Coordinate _lowerPivot;
		private Coordinate _higherPivot;
		private bool _movingToHigher;
		private int _speed;

		private GameVector _impartMovement;
		public GameVector ImpartedMovement { get { return _impartMovement; } }

		public MovingSurface(Coordinate roomPosition, Dictionary<string, Sprite> sprites, Coordinate startCoordinate, Coordinate endCoordinate,
			Coordinate lowerPivot, Coordinate higherPivot, bool movingToHigher, int speed)
			: base(roomPosition, sprites, startCoordinate, endCoordinate)
		{
			_movingToHigher = movingToHigher;
			_speed = speed;

			GameVector lower = GameVector.makeFromCoordinate(lowerPivot);
			GameVector higher = GameVector.makeFromCoordinate(higherPivot);
			if (higher.Direction < lower.Direction)
			{
				_lowerPivot = higherPivot;
				_higherPivot = lowerPivot;
				_movingToHigher = !_movingToHigher;
			}
			else
			{
				_lowerPivot = lowerPivot;
				_higherPivot = higherPivot;
			}
		}

		public override void update(GameTime gameTime)
		{
			//use GameVectors to find the relative position.
			GameVector lower = GameVector.makeFromCoordinate(_lowerPivot);
			GameVector higher = GameVector.makeFromCoordinate(_higherPivot);
			GameVector position = GameVector.makeFromCoordinate(RoomPosition);

			if (position.Direction < lower.Direction)
			{ _movingToHigher = true; }
			else if (position.Direction > higher.Direction)
			{ _movingToHigher = false; }
			else // ==
			{
				if (RoomPosition.getX() <= _lowerPivot.getX())
				{ _movingToHigher = true; }
				else if (RoomPosition.getX() >= _higherPivot.getX())
				{ _movingToHigher = false; }
			}

			GameVector difference;

			if (_movingToHigher)
			{ difference = GameVector.makeFromCoordinate(_higherPivot - _lowerPivot); }
			else
			{ difference = GameVector.makeFromCoordinate(_lowerPivot - _higherPivot); }

			difference = new GameVector(difference.Magnitude * gameTime.ElapsedGameTime.Milliseconds * 0.0001 * _speed, difference.Direction);

			move(Coordinate.makeFromVector(difference));
			_impartMovement = difference;
		}
	}
}
