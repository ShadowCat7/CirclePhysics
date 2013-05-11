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
    public class MovingSurface : Surface
    {
        private Coordinate _lowerPivot;
        private Coordinate _higherPivot;
        private bool _movingToHigher;
        private int _speed;

        private GameVector _impartMovement;
        public GameVector getImpartedMovement()
        { return _impartMovement; }

        public MovingSurface(Coordinate roomPosition, Dictionary<string, Sprite> sprites, Coordinate startCoordinate, Coordinate endCoordinate,
            Coordinate lowerPivot, Coordinate higherPivot, bool movingToHigher, int speed)
            : base(roomPosition, sprites, startCoordinate, endCoordinate)
        {
            _movingToHigher = movingToHigher;
            _speed = speed;

            GameVector lower = GameVector.makeFromCoordinate(lowerPivot);
            GameVector higher = GameVector.makeFromCoordinate(higherPivot);
            if (higher.getDirection() < lower.getDirection())
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
            GameVector position = GameVector.makeFromCoordinate(getRoomPosition());

            if (position.getDirection() < lower.getDirection())
            { _movingToHigher = true; }
            else if (position.getDirection() > higher.getDirection())
            { _movingToHigher = false; }
            else // ==
            {
                if (getRoomPosition().getX() <= _lowerPivot.getX())
                { _movingToHigher = true; }
                else if (getRoomPosition().getX() >= _higherPivot.getX())
                { _movingToHigher = false; }
            }

            GameVector difference;

            if (_movingToHigher)
            { difference = GameVector.makeFromCoordinate(_higherPivot - _lowerPivot); }
            else
            { difference = GameVector.makeFromCoordinate(_lowerPivot - _higherPivot); }

            difference = new GameVector(difference.getMagnitude() * gameTime.ElapsedGameTime.Milliseconds * 0.0001 * _speed, difference.getDirection());

            move(Coordinate.makeFromVector(difference));
            _impartMovement = difference;
        }
    }
}
