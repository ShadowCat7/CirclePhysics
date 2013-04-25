using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TimeTravel
{
    public class AnimatedSprite : Sprite
    {
        private int _columns;

        private int _currentColumn;
        public int getCurrentColumn()
        { return _currentColumn; }

        private int _counter;
        public int getCounter()
        { return _counter; }

        public AnimatedSprite(Texture2D image, int columns)
            :base(image)
        {
            _columns = columns;
            _currentColumn = 0;
            _counter = 0;
        }

        public override void reset()
        {
            _currentColumn = 0;
            _counter = 0;
        }

        public override void update()
        {
            if (_counter == 4)
            {
                ++_currentColumn;

                if (_currentColumn == _columns)
                { _currentColumn = 0; }

                _counter = 0;
            }
            else
            { ++_counter; }
        }

        public override void draw(SpriteBatch spriteBatch, Coordinate screenPosition)
        {
            spriteBatch.Draw(getImage(), new Vector2((float)screenPosition.getX(), (float)screenPosition.getY()),
                new Rectangle(_currentColumn * getImage().Width / _columns, 0, getImage().Width / _columns, getImage().Height), 
                Color.White);
        }

        public override void draw(SpriteBatch spriteBatch, Coordinate screenPosition, bool reverse)
        {
            SpriteEffects effect;
            if (reverse)
            { effect = SpriteEffects.None; }
            else
            { effect = SpriteEffects.FlipHorizontally; }

            spriteBatch.Draw(getImage(), new Vector2((float)screenPosition.getX(), (float)screenPosition.getY()),
                new Rectangle(_currentColumn * getImage().Width / _columns, 0, getImage().Width / _columns, getImage().Height),
                Color.White, 0, new Vector2(), 1, effect, 0);
        }

        public override void draw(SpriteBatch spriteBatch, Coordinate screenPosition, double directionFacing)
        {
            spriteBatch.Draw(getImage(), new Vector2((float)screenPosition.getX(), (float)screenPosition.getY()),
                new Rectangle(_currentColumn * getImage().Width / _columns, 0, getImage().Width / _columns, getImage().Height),
                Color.White, (float)directionFacing, new Vector2(0, 0), 1, SpriteEffects.None, 0);
        }
    }
}
