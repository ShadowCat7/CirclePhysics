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
    public abstract class Entity
    {
        private Coordinate _roomPosition;
        public Coordinate getRoomPosition()
        { return _roomPosition; }

        private Coordinate _screenPosition;
        public Coordinate getScreenPosition()
        { return _screenPosition; }
        public void setScreenPosition(Coordinate positionOfScreen)
        {
            _screenPosition = new Coordinate(_roomPosition.getX() - positionOfScreen.getX(), _roomPosition.getY() - positionOfScreen.getY());
            //TODO onScreen?
        }

        private bool _onScreen;
        public bool getOnScreen()
        { return _onScreen; }

        private bool _solid;
        public bool getSolid()
        { return _solid; }

        private BoundingCircle _boundingCircle;
        public int getBoundingRadius()
        { return _boundingCircle.getRadius(); }

        private Dictionary<string, Sprite> _sprites;
        public Sprite getSpriteFromDict(string spriteTag)
        { return _sprites[spriteTag]; }

        private Sprite _sprite;
        public Sprite getCurrentSprite()
        { return _sprite; }
        public void setCurrentSprite(Sprite sprite)
        { _sprite = sprite; }

        public Entity(Coordinate roomPosition, Dictionary<string, Sprite> sprites)
        {
            _roomPosition = roomPosition;
            _sprites = sprites;

            _boundingCircle = null;
            _solid = false;
            _sprite = sprites["start"];
        }
        public Entity(Coordinate roomPosition, bool solid, BoundingCircle boundingCircle, Dictionary<string, Sprite> sprites)
        {
            _roomPosition = roomPosition;
            _solid = solid;
            _boundingCircle = boundingCircle;
            _sprites = sprites;

            if (sprites != null)
            { _sprite = sprites["start"]; }
        }

        protected virtual void move(Coordinate newPosition)
        { _roomPosition = newPosition; }
        public virtual void update() { }
        public virtual void update(GameTime gameTime, List<Entity> entityList, List<Surface> surfaces) { }
        public virtual void update(GameTime gameTime, KeyboardState newKeyboardState, KeyboardState oldKeyboardState,
            MouseState newMouseState, MouseState oldMouseState, List<Entity> entityList, List<Surface> surfaces) { }
        public virtual void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_sprite.getImage(), 
                new Vector2((int)_roomPosition.getX() - _boundingCircle.getRadius(), (int)_roomPosition.getY() - _boundingCircle.getRadius()), Color.White);
        }
    }
}
