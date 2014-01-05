namespace CirclePhysics.Graphics
{
	public interface ISprite
	{
		IImage Image { get; }

		DrawingOptions GetDrawingInfo();
		void Update();
	}
}
