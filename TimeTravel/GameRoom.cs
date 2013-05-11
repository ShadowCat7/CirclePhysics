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

        private List<StaticEntity> _entityList;

        private List<Surface> _surfaces;

        public GameRoom(Coordinate size, Sprite background, Player player, List<StaticEntity> entityList, List<Surface> surfaces)
            : base(size, background)
        {
            _player = player;
            _entityList = entityList;
            _surfaces = surfaces;

            _onScreen = new Coordinate(0, 0);
            _paused = false;
        }

        public override void update(GameTime gameTime, KeyboardState newKeyboardState, KeyboardState oldKeyboardState, 
            MouseState newMouseState, MouseState oldMouseState)
        {
            if (newKeyboardState.IsKeyDown(Keys.Escape))
            { _paused = !_paused; }

            if (!_paused)
            {
                for (int k = 0; k < 10; ++k)
                {
                    for (int i = 0; i < _entityList.Count; i++)
                    {
                        _entityList[i].update(gameTime);
                        _entityList[i].update(gameTime, _entityList, _surfaces);
                    }

                    _player.update(gameTime, newKeyboardState, oldKeyboardState, newMouseState, oldMouseState, _entityList, _surfaces);
                }

                _player.setScreenPosition(getScreenPositionFromEntity(_player));

                for (int i = 0; i < _entityList.Count; ++i)
                { _entityList[i].setScreenPosition(_onScreen); }

                for (int i = 0; i < _surfaces.Count; ++i)
                { _surfaces[i].setScreenPosition(_onScreen); }
            }
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            //TODO background

            for (int i = 0; i < _surfaces.Count; ++i)
            { _surfaces[i].draw(spriteBatch); }

            for (int i = 0; i < _entityList.Count; ++i)
            { _entityList[i].draw(spriteBatch); }

            _player.draw(spriteBatch);
        }

        public Coordinate getScreenPositionFromEntity(StaticEntity entity)
        {
            double x = 0;
            double y = 0;
            double ex = 0;
            double ey = 0;

            if (entity.getRoomPosition().getX() + entity.getBigBoundingCircle().getRadius() / 2 < Screen.X / 2)
            {
                x = 0;
                ex = entity.getRoomPosition().getX();
            }
            else if (entity.getRoomPosition().getX() + entity.getBigBoundingCircle().getRadius() / 2 > getSize().getX() - Screen.X / 2)
            {
                x = getSize().getX() - Screen.X;
                ex = entity.getRoomPosition().getX() - (getSize().getX() - Screen.X);
            }
            else
            {
                x = entity.getRoomPosition().getX() + entity.getBigBoundingCircle().getRadius() / 2 - Screen.X / 2;
                ex = Screen.X / 2 - entity.getBigBoundingCircle().getRadius() / 2;
            }

            if (entity.getRoomPosition().getY() + entity.getBigBoundingCircle().getRadius() / 2 < Screen.Y / 2)
            {
                y = 0;
                ey = entity.getRoomPosition().getY();
            }
            else if (entity.getRoomPosition().getY() + entity.getBigBoundingCircle().getRadius() / 2 > getSize().getY() - Screen.Y / 2)
            {
                y = getSize().getY() - Screen.Y;
                ey = entity.getRoomPosition().getY() - getSize().getY() + Screen.Y;
            }
            else
            {
                y = entity.getRoomPosition().getY() + entity.getBigBoundingCircle().getRadius() / 2 - Screen.Y / 2;
                ey = Screen.Y / 2 - entity.getBigBoundingCircle().getRadius() / 2;
            }

            _onScreen = new Coordinate(x, y);
            return new Coordinate(ex, ey);
        }
    }
}
