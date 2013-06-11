﻿using System.Collections.Generic;
using CirclePhysics.Graphics.Interfaces;
using CirclePhysics.Physics;

namespace CirclePhysics.Entity
{
	public interface IOverlay
	{
		Coordinate ScreenPosition { get; }
		Dictionary<string, ISprite> Sprites { get; }
		ISprite CurrentSprite { get; }

		void Draw(IDrawer drawer);
		void Update();
	}
}
