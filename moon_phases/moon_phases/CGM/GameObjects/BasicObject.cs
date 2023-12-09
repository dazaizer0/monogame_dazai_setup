using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace moon_phases.CGM.GameObjects
{
    internal class BasicObject : Object
    {
        public Texture2D Texture;
        public Color ObjectColor;
        public Vector2 PlayerMoveDirection;

        public float RotationSpeed = 1f;

        public BasicObject(Vector2 position, float rotation, Texture2D texture, Color object_color, bool enabled) : base(position, rotation, enabled)
        {
            Texture = texture;
            ObjectColor = object_color;
        }

        public void DrawIt(SpriteBatch sprite_batch)
        {
            if (this.Enabled)
                sprite_batch.Draw(this.Texture, this.Position, null, this.ObjectColor, this.Rotation, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
        }

        public void GetPlayerMoveDirection(Player player)
        {
            this.PlayerMoveDirection = this.Position - player.Position;
            this.PlayerMoveDirection.Normalize();
        }

        public void LookAt(Vector2 target, GameTime gameTime)
        {
            Vector2 direction = target - Position;
            float target_rotation = (float)Math.Atan2(direction.Y, direction.X);

            Rotation = CGM.FunctionalAdditives.CGMath.LerpAngle(Rotation, target_rotation, RotationSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        public void AcceleratePhysics(GameTime game_time, Vector2 gravity)
        {
            Position += gravity * (float)game_time.ElapsedGameTime.TotalSeconds;
        }
    }
}
