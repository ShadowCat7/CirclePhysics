namespace CirclePhysics.Graphics.Interfaces
{
	public interface ISprite
	{
		IImage Image { get; }

		DrawingOptions GetDrawingInfo();
		void Update();
	}
}
