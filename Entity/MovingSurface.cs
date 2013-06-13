using CirclePhysics.Graphics.Interfaces;
using CirclePhysics.Physics;
using System.Collections.Generic;
using CirclePhysics.Utility;

namespace CirclePhysics.Entity
{
	public class MovingSurface : Surface
	{
		private readonly Coordinate _lowerPivot;
		private readonly Coordinate _higherPivot;
		private bool _movingToHigher;
		private readonly int _speed;

		private GameVector _impartMovement;
		public GameVector ImpartedMovement { get { return _impartMovement; } }

		public MovingSurface(Coordinate roomPosition, Dictionary<string, ISprite> sprites, string startingSprite, Coordinate startCoordinate, Coordinate endCoordinate,
			Coordinate lowerPivot, Coordinate higherPivot, bool movingToHigher, int speed)
			: base(roomPosition, sprites, startingSprite, startCoordinate, endCoordinate)
		{
			_movingToHigher = movingToHigher;
			_speed = speed;

			GameVector lower = lowerPivot.ToGameVector();
			GameVector higher = higherPivot.ToGameVector();
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

		public override void Update(int gameTime)
		{
			//use GameVectors to find the relative position.
			GameVector lower = _lowerPivot.ToGameVector();
			GameVector higher = _higherPivot.ToGameVector();
			GameVector position = RoomPosition.ToGameVector();

			if (position.Direction < lower.Direction)
			{ _movingToHigher = true; }
			else if (position.Direction > higher.Direction)
			{ _movingToHigher = false; }
			else // ==
			{
				if (RoomPosition.X <= _lowerPivot.X)
				{ _movingToHigher = true; }
				else if (RoomPosition.X >= _higherPivot.X)
				{ _movingToHigher = false; }
			}

			GameVector difference;

			if (_movingToHigher)
			{ difference = (_higherPivot - _lowerPivot).ToGameVector(); }
			else
			{ difference = (_lowerPivot - _higherPivot).ToGameVector(); }

			difference = new GameVector(difference.Magnitude * gameTime * 0.0001 * _speed, difference.Direction);

			Move(difference.ToCoordinate());
			_impartMovement = difference;
		}
	}
}
