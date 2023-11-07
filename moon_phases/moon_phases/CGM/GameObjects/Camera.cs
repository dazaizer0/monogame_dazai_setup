using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace moon_phases.CGM.GameObjects
{
    internal class Camera : Object
    {
        public Matrix Transform { get; private set; }
        public Vector2 CenterProperties;
        private Viewport Viewport;
        private float Zoom;

        public Camera(Vector2 position, Viewport viewport, float zoom, bool enabled) : base(position, enabled)
        {
            Viewport = viewport;
            Zoom = zoom;
        }

        public void Refresh(Vector2 playerPosition)
        {
            Position = new Vector2(playerPosition.X - Viewport.Width / 2, playerPosition.Y - Viewport.Height / 2);

            Transform = Matrix.CreateTranslation(new Vector3(-Position, 0)) *
                        Matrix.CreateScale(Zoom, Zoom, 1) *
                        Matrix.CreateTranslation(new Vector3(Viewport.Width / 2, Viewport.Height / 2, 0));
        }
    }
}
