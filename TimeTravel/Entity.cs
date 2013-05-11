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
    public abstract class Entity : Overlay
    {
        private Coordinate _roomPosition;
        public Coordinate getRoomPosition()
        { return _roomPosition; }

        private bool _onScreen;
        public bool getOnScreen()
        { return _onScreen; }

        public Entity(Coordinate roomPosition, Dictionary<string, Sprite> sprites)
            : base(null, sprites)
        { _roomPosition = roomPosition; }

        protected virtual void move(Coordinate newPosition)
        { _roomPosition = newPosition; }

        public override void setScreenPosition(Coordinate positionOfScreen)
        {
            base.setScreenPosition(new Coordinate(_roomPosition.getX() - positionOfScreen.getX(), _roomPosition.getY() - positionOfScreen.getY()));

            if (getCurrentSprite() != null)
            {
                if (getRoomPosition().getX() > positionOfScreen.getX() + Screen.X)
                { _onScreen = false; }
                else if (getRoomPosition().getY() > positionOfScreen.getY() + Screen.Y)
                { _onScreen = false; }
                else if (getRoomPosition().getX() + getCurrentSprite().getImage().Bounds.Width < positionOfScreen.getX())
                { _onScreen = false; }
                else if (getRoomPosition().getY() + getCurrentSprite().getImage().Bounds.Height < positionOfScreen.getY())
                { _onScreen = false; }
                else
                { _onScreen = true; }
            }
            else
            {
                if (getRoomPosition().getX() > positionOfScreen.getX() + Screen.X)
                { _onScreen = false; }
                else if (getRoomPosition().getY() > positionOfScreen.getY() + Screen.Y)
                { _onScreen = false; }
                else if (getRoomPosition().getX() < positionOfScreen.getX())
                { _onScreen = false; }
                else if (getRoomPosition().getY() < positionOfScreen.getY())
                { _onScreen = false; }
                else
                { _onScreen = true; }
            }
        }

        protected virtual void setPlayerScreenPosition(Coordinate screenPosition)
        {
            _onScreen = true;
            base.setScreenPosition(screenPosition);
        }

        public virtual void damaged(int damage)
        { }

        public virtual void update(GameTime gameTime) { }
        public virtual void update(GameTime gameTime, List<StaticEntity> entityList, List<Surface> surfaces) { }
    }
}
