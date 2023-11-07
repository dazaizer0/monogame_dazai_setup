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

        public float ShakeDuration = 0;
        public float ShakeIntensity = 10.0f;

        public Vector2 OriginalPosition;
        public Random random;

        public Camera(Vector2 position, Viewport viewport, float zoom, bool enabled) : base(position, enabled)
        {
            Viewport = viewport;
            Zoom = zoom;
            random = new Random();
        }

        public void Refresh(Vector2 playerPosition, GameTime game_time)
        {
            OriginalPosition = new Vector2(playerPosition.X - Viewport.Width / 2, playerPosition.Y - Viewport.Height / 2);

            if (ShakeDuration > 0)
            {
                float xOffset = (float)(random.NextDouble() * 2 - 1) * ShakeIntensity;
                float yOffset = (float)(random.NextDouble() * 2 - 1) * ShakeIntensity;

                OriginalPosition += new Vector2(xOffset, yOffset);

                ShakeDuration -= (float)game_time.ElapsedGameTime.TotalSeconds;
            }

            Position = OriginalPosition;

            Transform = Matrix.CreateTranslation(new Vector3(-Position, 0)) *
                        Matrix.CreateScale(Zoom, Zoom, 1) *
                        Matrix.CreateTranslation(new Vector3(Viewport.Width / 2, Viewport.Height / 2, 0));
        }

        public void Shake(float duration)
        {
            ShakeDuration = duration;
            OriginalPosition = Position;
        }
    }

}
