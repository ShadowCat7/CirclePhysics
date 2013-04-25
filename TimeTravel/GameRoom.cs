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
    public class GameRoom : Room
    {
        private Player _player;

        private Coordinate _onScreen;

        private bool _paused;

        private List<Surface> _surfaces;

        public GameRoom(Coordinate size, Sprite background, List<Entity> entityList, Player player, List<Surface> surfaces)
            : base(size, background, entityList)
        {
            _player = player;
            _surfaces = surfaces;

            _onScreen = new Coordinate(0, 0);
            _paused = false;
        }

        public override void update(GameTime gameTime, KeyboardState newKeyboardState, KeyboardState oldKeyboardState, 
            MouseState newMouseState, MouseState oldMouseState)
        {
            List<Entity> entityList = getEntityList();

            for (int k = 0; k < 10; ++k)
            {
                for (int i = 0; i < entityList.Count; i++)
                {
                    entityList[i].update();
                    entityList[i].update(gameTime, entityList, _surfaces);
                }

                _player.update(gameTime, newKeyboardState, oldKeyboardState, newMouseState, oldMouseState, entityList, _surfaces);
            }

            getScreenPositionFromEntity(_player);

            _player.setScreenPosition(_onScreen);

            for (int i = 0; i < entityList.Count; ++i)
            { entityList[i].setScreenPosition(_onScreen); }

            for (int i = 0; i < _surfaces.Count; ++i)
            { _surfaces[i].setScreenPosition(_onScreen); }
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            List<Entity> entityList = getEntityList();

            //TODO background

            for (int i = 0; i < _surfaces.Count; ++i)
            { _surfaces[i].draw(spriteBatch); }

            for (int i = 0; i < entityList.Count; ++i)
            { entityList[i].draw(spriteBatch); }

            _player.draw(spriteBatch);
        }

        public void getScreenPositionFromEntity(Entity entity)
        {
            double x = 0;
            double y = 0;

            if (entity.getRoomPosition().getX() < Screen.X / 2)
            { x = 0; }
            else if (entity.getRoomPosition().getX() > getSize().getX() - Screen.X / 2)
            { x = getSize().getX() - Screen.X; }
            else
            { x = entity.getRoomPosition().getX() - Screen.X / 2; }

            if (entity.getRoomPosition().getY() < Screen.Y / 2)
            { y = 0; }
            else if (entity.getRoomPosition().getY() > getSize().getY() - Screen.Y / 2)
            { y = getSize().getY() - Screen.Y; }
            else
            { y = entity.getRoomPosition().getY() - Screen.Y / 2; }

            _onScreen = new Coordinate(x, y);
        }
    }
}
