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
    public class Player : MovingEntity
    {
        private const int _TOP_SPEED = 200;
        private const double _FRICTION = 1;

        public Player(Coordinate roomPosition, BoundingCircle boundingCircle, Dictionary<string, Sprite> sprites)
            : base(roomPosition, boundingCircle, sprites, _TOP_SPEED, _FRICTION, true)
        { setCurrentSprite(getSpriteFromDict("start")); }

        public override void update(GameTime gameTime, KeyboardState newKeyboardState, KeyboardState oldKeyboardState, 
            MouseState newMouseState, MouseState oldMouseState, List<Entity> entityList, List<Surface> surfaces)
        {
            getCurrentSprite().update();

            if (newKeyboardState.IsKeyDown(Keys.W))
            { addVelocity(new GameVector(getTopSpeed(), -Math.PI / 2)); }
            if (newKeyboardState.IsKeyDown(Keys.S))
            { addVelocity(new GameVector(getTopSpeed(), Math.PI / 2)); }

            if (newKeyboardState.IsKeyDown(Keys.A))
            { addVelocity(new GameVector(getTopSpeed(), Math.PI)); }

            if (newKeyboardState.IsKeyDown(Keys.D))
            { addVelocity(new GameVector(getTopSpeed(), 0)); }

            if (getOnGround() && newKeyboardState.IsKeyDown(Keys.Space))
            { addVelocity(new GameVector(200, -Math.PI / 2)); }

            //if (!newKeyboardState.IsKeyDown(Keys.A) &&
            //    !newKeyboardState.IsKeyDown(Keys.D) &&
            //    !newKeyboardState.IsKeyDown(Keys.W) &&
            //    !newKeyboardState.IsKeyDown(Keys.S))
            //{
            //    if (getVelocity().getMagnitude() > 0.01)
            //    addVelocity(new GameVector(-getVelocity().getXLength(), 0));
            //}

            base.update(gameTime, entityList, surfaces);
        }
    }
}
