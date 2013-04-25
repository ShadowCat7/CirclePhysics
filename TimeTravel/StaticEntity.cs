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
    public class StaticEntity : Entity
    {
        public StaticEntity(Coordinate roomPosition, Dictionary<string, Sprite> sprites)
            :base(roomPosition, sprites) { }
        public StaticEntity(Coordinate roomPosition, bool solid, BoundingCircle boundingCircle, Dictionary<string, Sprite> sprites)
            :base(roomPosition, solid, boundingCircle, sprites) { }

        protected override void move(Coordinate newPosition)
        { throw new Exception("StaticEntity cannot move."); }
    }
}
