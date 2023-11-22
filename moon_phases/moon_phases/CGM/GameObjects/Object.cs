using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using moon_phases.CGM.Components;

namespace moon_phases.CGM.GameObjects
{
    internal class Object
    {
        public Vector2 Position;
        public float Rotation;
        public bool Enabled;

        public List<BasicComponent> Components;

        public Object(Vector2 position, float rotation, bool enabled)
        {
            Position = position;
            Enabled = enabled;
            Rotation = rotation;
        }
    }
}
