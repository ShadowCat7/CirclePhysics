using CirclePhysics.Graphics;
using CirclePhysics.Physics;
using CirclePhysics.Graphics.Interfaces;
using CirclePhysics.Controls;

namespace CirclePhysics.Rooms
{
	public abstract class Room
	{
		public Coordinate Size { get; private set; }

		public ISprite Background { get; private set; }

		public static Coordinate ViewPort { get; set; }

		public Room(Coordinate size, ISprite background)
		{
			Size = size;
			Background = background;
		}

		public abstract void Update(int elapsedTime, CircleControls oldControls, CircleControls newControls);

		public abstract void Draw(IDrawer drawer);
	}
}
