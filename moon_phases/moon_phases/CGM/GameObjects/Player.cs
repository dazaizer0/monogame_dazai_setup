using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace moon_phases.CGM.GameObjects
{
    internal class Player : BasicObject
    {
        public float Speed;

        public Player (Vector2 position, Texture2D texture, float speed, Color object_color, bool enabled) : base(position, texture, object_color, enabled)
        {
            Speed = speed;
        }

        public bool IfCollision(BasicObject object1)
        {
            Rectangle rect1 = new Rectangle((int)this.Position.X, (int)this.Position.Y, (int)this.Texture.Width, (int)this.Texture.Height);
            Rectangle rect2 = new Rectangle((int)object1.Position.X, (int)object1.Position.Y, (int)object1.Texture.Width, (int)object1.Texture.Height);

            return (rect1.Intersects(rect2) == true);
        }

        public void AccelerateTopDownMovement(GameTime gameTime)
        {
            var keyboard_state = Keyboard.GetState();

            if (keyboard_state.IsKeyDown(Keys.W))
            {
                this.Position.Y -= this.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (keyboard_state.IsKeyDown(Keys.S))
            {
                this.Position.Y += this.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (keyboard_state.IsKeyDown(Keys.D))
            {
                this.Position.X += this.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (keyboard_state.IsKeyDown(Keys.A))
            {
                this.Position.X -= this.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }
    }
}
