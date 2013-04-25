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
    public abstract class Room
    {
        private Coordinate _size;
        public Coordinate getSize()
        { return _size; }

        private List<Entity> _entityList;
        public List<Entity> getEntityList()
        { return _entityList; }

        private Sprite _background;
        public Sprite getBackground()
        { return _background; }

        public Room(Coordinate size, Sprite background, List<Entity> entityList)
        {
            _size = size;
            _background = background;
            _entityList = entityList;
        }

        public virtual void update(GameTime gameTime, KeyboardState newKeyboardState, KeyboardState oldKeyboardState, 
            MouseState newMouseState, MouseState oldMouseState) { }

        public abstract void draw(SpriteBatch spriteBatch);
    }
}
