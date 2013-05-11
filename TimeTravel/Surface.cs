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
    public class Surface : Entity
    {
        private Coordinate _endCoordinate;
        public Coordinate getEndCoordinate()
        { return _endCoordinate; }

        private Coordinate _startCoordinate;
        public Coordinate getStartCoordinate()
        { return _startCoordinate; }

        public Surface(Coordinate roomPosition, Dictionary<string, Sprite> sprites, Coordinate startCoordinate, Coordinate endCoordinate)
            : base(roomPosition, sprites)
        {
            _endCoordinate = endCoordinate;
            _startCoordinate = startCoordinate;
        }

        public override void setStartingSprite()
        {
            if (getSpriteFromDict("start") != null)
            { setCurrentSprite("start"); }
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            if (getCurrentSprite() != null)
            {
                spriteBatch.Draw(getCurrentSprite().getImage(),
                    new Vector2((int)(getRoomPosition().getX()), (int)(getRoomPosition().getY())), Color.White);
            }
        }
    }
}
