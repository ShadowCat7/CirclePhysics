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
        private BoundingCircle _bigBoundingCircle;
        public BoundingCircle getBigBoundingCircle()
        { return _bigBoundingCircle; }

        private BoundingCircle[] _boundingCircles;
        public BoundingCircle[] getBoundingCircles()
        { return _boundingCircles; }

        private bool _solid;
        public bool getSolid()
        { return _solid; }

        public StaticEntity(Coordinate roomPosition, Dictionary<string, Sprite> sprites)
            :base(roomPosition, sprites)
        {
            _bigBoundingCircle = null;
            _boundingCircles = null;
        }
        public StaticEntity(Coordinate roomPosition, bool solid, int radius, Dictionary<string, Sprite> sprites)
            :base(roomPosition, sprites)
        {
            _solid = solid;

            _bigBoundingCircle = new BoundingCircle(radius, new Coordinate(radius, radius));
            _boundingCircles = null;
        }
        public StaticEntity(Coordinate roomPosition, bool solid, BoundingCircle[] boundingCircles, Dictionary<string, Sprite> sprites)
            : base(roomPosition, sprites)
        {
            _solid = solid;
            _boundingCircles = boundingCircles;

            int xStart = 0;
            int xEnd = 0;
            int yStart = 0;
            int yEnd = 0;
            for (int i = 0; i < boundingCircles.Length; ++i)
            {
                int temp = (int)boundingCircles[i].getCenter().getX() - boundingCircles[i].getRadius();
                if (temp < xStart)
                { xStart = temp; }

                temp = (int)boundingCircles[i].getCenter().getX() + boundingCircles[i].getRadius();
                if (temp > xEnd)
                { xEnd = temp; }

                temp = (int)boundingCircles[i].getCenter().getY() - boundingCircles[i].getRadius();
                if (temp < yStart)
                { yStart = temp; }

                temp = (int)boundingCircles[i].getCenter().getY() + boundingCircles[i].getRadius();
                if (temp > yEnd)
                { yEnd = temp; }
            }

            int radius = Math.Max((xEnd - xStart) / 2, (yEnd - yStart) / 2);

            _bigBoundingCircle = new BoundingCircle(radius, new Coordinate((xEnd - xStart) / 2, (yEnd - yStart) / 2));
        }

        public override void setStartingSprite()
        { setCurrentSprite("start"); }

        public override void draw(SpriteBatch spriteBatch)
        {
            if (getCurrentSprite() != null && getOnScreen())
            {
                spriteBatch.Draw(getCurrentSprite().getImage(), new Vector2((int)getScreenPosition().getX(),
                    (int)getScreenPosition().getY()), Color.White);
            }
        }
    }
}
