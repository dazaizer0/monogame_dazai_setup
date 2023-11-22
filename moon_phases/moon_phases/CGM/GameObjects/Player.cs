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
        public float BaseSpeed;
        public float SprintSpeed;

        public float DashForce;
        public float DashCooldown;
        public float DashTimer = 0f;
        public bool DashEnable = true;

        public Vector2 MoveDirection;

        public Player (Vector2 position, float rotation, Texture2D texture, float speed, Color object_color, bool enabled) : base(position, rotation, texture, object_color, enabled)
        {
            BaseSpeed = speed;
            SprintSpeed = speed;
        }

        public bool IfCollision(BasicObject object1)
        {
            Rectangle rect1 = new Rectangle((int)this.Position.X, (int)this.Position.Y, (int)this.Texture.Width, (int)this.Texture.Height);
            Rectangle rect2 = new Rectangle((int)object1.Position.X, (int)object1.Position.Y, (int)object1.Texture.Width, (int)object1.Texture.Height);

            return (rect1.Intersects(rect2) == true);
        }

        public void AccelerateTopDownMovement(GameTime game_time)
        {
            var keyboard_state = Keyboard.GetState();

            if (keyboard_state.IsKeyDown(Keys.W))
            {
                this.Position.Y -= this.Speed * (float)game_time.ElapsedGameTime.TotalSeconds;
                this.MoveDirection = new Vector2(0, -1);
            }

            if (keyboard_state.IsKeyDown(Keys.S))
            {
                this.Position.Y += this.Speed * (float)game_time.ElapsedGameTime.TotalSeconds;
                this.MoveDirection = new Vector2(0, 1);
            }

            if (keyboard_state.IsKeyDown(Keys.D))
            {
                this.Position.X += this.Speed * (float)game_time.ElapsedGameTime.TotalSeconds;
                this.MoveDirection = new Vector2(1, 0);
            }

            if (keyboard_state.IsKeyDown(Keys.A))
            {
                this.Position.X -= this.Speed * (float)game_time.ElapsedGameTime.TotalSeconds;
                this.MoveDirection = new Vector2(-1, 0);
            }

            if (keyboard_state.IsKeyDown(Keys.Space) && DashEnable)
            {
                this.Position += MoveDirection * new Vector2(DashForce, DashForce);
                DashEnable = false;
            }

            if (!this.DashEnable)
            {
                this.DashTimer += (float)game_time.ElapsedGameTime.TotalSeconds;

                if (this.DashTimer > this.DashCooldown)
                {
                    this.DashEnable = true;
                    this.DashTimer = 0f;
                }
            }

            if (keyboard_state.IsKeyDown(Keys.LeftShift))
            {
                this.Speed = SprintSpeed;
            }
            else
            {
                this.Speed = BaseSpeed;
            }
        }
    }
}
