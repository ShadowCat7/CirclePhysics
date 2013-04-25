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
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        KeyboardState oldKeyboardState;
        MouseState oldMouseState;

        Room currentRoom;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            Screen.X = 800;
            Screen.Y = 480;

            oldKeyboardState = Keyboard.GetState();
            oldMouseState = Mouse.GetState();
        }
        protected override void Initialize()
        { base.Initialize(); }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Dictionary<string, Sprite> tempDict = new Dictionary<string, Sprite>();

            tempDict.Add("start", new Sprite(Content.Load<Texture2D>("player")));
            ImageHandler.player = tempDict;
            tempDict = new Dictionary<string, Sprite>();
            tempDict.Add("start", new Sprite(Content.Load<Texture2D>("wall")));
            ImageHandler.wall = tempDict;
            tempDict = new Dictionary<string, Sprite>();
            tempDict.Add("start", new Sprite(Content.Load<Texture2D>("surface")));
            ImageHandler.surface = tempDict;

            List<Entity> entities = new List<Entity>();
            entities.Add(new StaticEntity(new Coordinate(200, 200), true, new BoundingCircle(31), ImageHandler.wall));

            List<Surface> surfaces = new List<Surface>();

            surfaces.Add(new Surface(new Coordinate(0, 480), null, new Coordinate(0, 0), new Coordinate(800, 0)));
            surfaces.Add(new Surface(new Coordinate(500, 480), ImageHandler.surface, new Coordinate(0, 0), new Coordinate(200, -50)));

            currentRoom = new GameRoom(new Coordinate(800, 480), ImageHandler.wall["start"], entities, 
                new Player(new Coordinate(0, 0), new BoundingCircle(31), ImageHandler.player), surfaces);
        }

        protected override void UnloadContent() { }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState newKeyboardState = Keyboard.GetState();
            MouseState newMouseState = Mouse.GetState();

            currentRoom.update(gameTime, newKeyboardState, oldKeyboardState, newMouseState, oldMouseState);

            oldKeyboardState = newKeyboardState;
            oldMouseState = newMouseState;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            currentRoom.draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
