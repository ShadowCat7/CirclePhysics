﻿using System;
using System.Collections.Generic;
using CirclePhysics.Graphics;
using CirclePhysics.Graphics.Interfaces;
using CirclePhysics.Physics;
using CirclePhysics.Physics.Interfaces;
using CirclePhysics.Rooms;

namespace CirclePhysics.Entity
{
	public abstract class CollisionObject : ScreenOverlay, ICollidable
	{
		protected CollisionObject(Coordinate roomPosition, Dictionary<string, ISprite> sprites, string startingSprite)
			: base(null, sprites, startingSprite)
		{ RoomPosition = roomPosition; }

		public Coordinate RoomPosition { get; private set; }

		public bool OnScreen { get; private set; }

		protected virtual void Move(Coordinate newPosition)
		{ RoomPosition = newPosition; }

		public override void SetScreenPosition(Coordinate positionOfScreen)
		{
			base.SetScreenPosition(new Coordinate(RoomPosition.X - positionOfScreen.X, RoomPosition.Y - positionOfScreen.Y));

			if (CurrentSprite != null)
			{
				if (RoomPosition.X > positionOfScreen.X + Room.ViewPort.X)
				{ OnScreen = false; }
				else if (RoomPosition.Y > positionOfScreen.Y + Room.ViewPort.Y)
				{ OnScreen = false; }
				else if (RoomPosition.X + CurrentSprite.Image.Width < positionOfScreen.X)
				{ OnScreen = false; }
				else if (RoomPosition.Y + CurrentSprite.Image.Height < positionOfScreen.Y)
				{ OnScreen = false; }
				else
				{ OnScreen = true; }
			}
			else
			{
				if (RoomPosition.X > positionOfScreen.X + Room.ViewPort.X)
				{ OnScreen = false; }
				else if (RoomPosition.Y > positionOfScreen.Y + Room.ViewPort.Y)
				{ OnScreen = false; }
				else if (RoomPosition.X < positionOfScreen.X)
				{ OnScreen = false; }
				else if (RoomPosition.Y < positionOfScreen.Y)
				{ OnScreen = false; }
				else
				{ OnScreen = true; }
			}
		}

		protected virtual void SetPlayerScreenPosition(Coordinate screenPosition)
		{
			OnScreen = true;
			base.SetScreenPosition(screenPosition);
		}

		public virtual void Update(int elapsedTime) { }
		public virtual void Update(int elapsedTime, List<StaticEntity> entityList, List<Surface> surfaces) { }

		public virtual void OnCollision(ICollidable collidable) { }
	}
}
