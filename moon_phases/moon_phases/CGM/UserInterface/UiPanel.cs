using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace moon_phases.CGM.UserInterface
{
    internal class Panel : GameObjects.Object
    {
        public Vector2 ScreenCenter;

        public Panel(Vector2 position, Vector2 screen_center, bool enabled) : base(position, enabled)
        {
            ScreenCenter = screen_center;
        }
    }
}
