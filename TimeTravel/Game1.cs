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
            tempDict = new Dictionary<string, Sprite>();
            tempDict.Add("right", null);
            ImageHandler.gun = tempDict;
            tempDict = new Dictionary<string, Sprite>();
            tempDict.Add("start", null);
            ImageHandler.bullet = tempDict;

            List<StaticEntity> entities = new List<StaticEntity>();
            entities.Add(new StaticEntity(new Coordinate(200, 130), true, 32, ImageHandler.wall));

            List<Surface> surfaces = new List<Surface>();

            surfaces.Add(new Surface(new Coordinate(0, 300), null, new Coordinate(0, 0), new Coordinate(800, 0)));
            surfaces.Add(new Surface(new Coordinate(0, 240), null, new Coordinate(0, 0), new Coordinate(400, 0)));
            //surfaces.Add(new Surface(new Coordinate(400, 300), ImageHandler.surface, new Coordinate(0, 0), new Coordinate(180, 180)));

            BoundingCircle[] boundingCircles = new BoundingCircle[2];
            boundingCircles[0] = new BoundingCircle(16, new Coordinate(15.5, 15.5));
            boundingCircles[1] = new BoundingCircle(16, new Coordinate(15.5, 47.5));

            currentRoom = new GameRoom(new Coordinate(1600, 480), ImageHandler.wall["start"],
                new Player(new Coordinate(0, 0), boundingCircles, ImageHandler.player, new Gun(new Coordinate(0, 0), ImageHandler.gun, 
                    new Bullet(new Coordinate(0, 0), 2, ImageHandler.bullet, 300, 0, 1, 100))), entities, surfaces);
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
