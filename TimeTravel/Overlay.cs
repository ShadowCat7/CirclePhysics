using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TimeTravel
{
    public abstract class Overlay
    {
        private Coordinate _screenPosition;
        public Coordinate getScreenPosition()
        { return _screenPosition; }
        public virtual void setScreenPosition(Coordinate screenPosition)
        { _screenPosition = screenPosition; }

        private Dictionary<string, Sprite> _sprites;
        public Sprite getSpriteFromDict(string spriteTag)
        {
            if (_sprites == null || _sprites.ContainsKey(spriteTag))
            { return null; }

            return _sprites[spriteTag];
        }
        public Dictionary<string, Sprite> getSprites()
        { return _sprites; }

        private Sprite _sprite;
        public Sprite getCurrentSprite()
        { return _sprite; }
        public void setCurrentSprite(string key)
        { _sprite = _sprites[key]; }

        public Overlay(Coordinate screenPosition, Dictionary<string, Sprite> sprites)
        {
            _screenPosition = screenPosition;
            _sprites = sprites;

            _sprite = null;
            setStartingSprite();
        }

        public abstract void setStartingSprite();

        public virtual void draw(SpriteBatch spriteBatch)
        { spriteBatch.Draw(_sprite.getImage(), new Vector2((int)_screenPosition.getX(), (int)_screenPosition.getY()), Color.White); }
    }
}
