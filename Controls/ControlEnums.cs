using System;

namespace CirclePhysics.Controls
{
	[Flags]
	public enum CircleControls
	{
		Right = 0x1,
		Left = 0x2,
		Up = 0x4,
		Down = 0x8,
		Jump = 0x10,
		Action = 0x20,
		Pause = 0x40
	}
}
