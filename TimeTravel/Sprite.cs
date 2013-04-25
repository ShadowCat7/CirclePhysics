using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TimeTravel
{
    public class Sprite
    {
        private Texture2D _image;
        public Texture2D getImage()
        { return _image; }

        private Coordinate _topLeft;
        public Coordinate getTopLeft()
        { return _topLeft; }

        public Sprite(Texture2D image)
        { _image = image; }

        public virtual void update() { }
        public virtual void reset() { }

        public virtual void draw(SpriteBatch spriteBatch, Coordinate screenPosition)
        {
            spriteBatch.Draw(_image, new Vector2((float)screenPosition.getX(), (float)screenPosition.getY()),
                Color.White);
        }

        public virtual void draw(SpriteBatch spriteBatch, Coordinate screenPosition, bool reverse)
        {
            SpriteEffects effect;
            if (reverse)
            { effect = SpriteEffects.None; }
            else
            { effect = SpriteEffects.FlipHorizontally; }

            spriteBatch.Draw(_image, new Vector2((float)screenPosition.getX(), (float)screenPosition.getY()),
                null, Color.White, 0, new Vector2(0, 0), 0, effect, 0);
        }

        public virtual void draw(SpriteBatch spriteBatch, Coordinate screenPosition, double directionFacing)
        {
            spriteBatch.Draw(_image, new Vector2((float)screenPosition.getX(), (float)screenPosition.getY()),
                null, Color.White, (float)directionFacing, new Vector2(0, 0), 0, SpriteEffects.None, 0);
        }
    }
}
