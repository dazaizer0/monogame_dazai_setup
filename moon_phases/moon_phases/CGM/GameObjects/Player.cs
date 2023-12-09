using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using MonoGame.Extended.Timers;

namespace moon_phases.CGM.GameObjects
{
    internal class Player : BasicObject
    {
        public float Speed;
        public float BaseSpeed;
        public float SprintSpeed;

        private bool IsJumping = false;
        private float JumpForce = -700f;
        private float JumpTime = 0f;
        private const float MaxJumpTime = 0.5f;
        public bool JumpEnable = true;
        

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

        public void AccelerateMovement(GameTime game_time, Vector2 gravity)
        {
            var keyboard_state = Keyboard.GetState();

            /*if (keyboard_state.IsKeyDown(Keys.W))
            {
                this.Position.Y -= this.Speed * (float)game_time.ElapsedGameTime.TotalSeconds;
                this.MoveDirection = new Vector2(0, -1);
            }

            if (keyboard_state.IsKeyDown(Keys.S))
            {
                this.Position.Y += this.Speed * (float)game_time.ElapsedGameTime.TotalSeconds;
                this.MoveDirection = new Vector2(0, 1);
            }*/

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

            if (keyboard_state.IsKeyDown(Keys.Space) && JumpEnable && !IsJumping)
            {
                IsJumping = true;
                JumpTime = 0f;
            }

            if (IsJumping)
            {
                JumpTime += (float)game_time.ElapsedGameTime.TotalSeconds;

                if (JumpTime < MaxJumpTime)
                {
                    this.Position += new Vector2(0, JumpForce * (1 - JumpTime / MaxJumpTime)) * (float)game_time.ElapsedGameTime.TotalSeconds;
                }
                else
                {
                    IsJumping = false;
                    JumpEnable = false;
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
