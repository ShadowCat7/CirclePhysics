using CirclePhysics.Physics;

namespace CirclePhysics.Graphics
{
	public class DrawingOptions
	{
		public DrawingOptions(int width, int height, Coordinate sourcePosition = null)
		{
			Height = height;
			Width = width;
			SourcePosition = sourcePosition ?? new Coordinate(0, 0);
		}

		public int Height { get; private set; }
		public int Width { get; private set; }
		public Coordinate SourcePosition { get; private set; }
	}
}
