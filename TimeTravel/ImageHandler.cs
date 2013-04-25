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
    public static class ImageHandler
    {
        public static Dictionary<string, Sprite> player;
        public static Dictionary<string, Sprite> wall;
        public static Dictionary<string, Sprite> surface;
    }
}
