using CirclePhysics.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CirclePhysics.Room
{
	public abstract class Room
	{
		private Coordinate _size;
		public Coordinate Size { get { return _size; } }

		//private List<MenuItem> _menuItems;
		//public List<MenuItem> getMenuItems()
		//{ return _menuItems; }

		private Sprite _background;
		public Sprite Background { get { return _background; } }

		public Room(Coordinate size, Sprite background)
		{
			_size = size;
			_background = background;
		}

		public virtual void Update(GameTime gameTime, KeyboardState newKeyboardState, KeyboardState oldKeyboardState,
			MouseState newMouseState, MouseState oldMouseState) { }

		public abstract void draw(SpriteBatch spriteBatch);
	}
}
