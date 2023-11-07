﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace moon_phases.CGM
{
    #region NEW OBJECT SYSTEM
    internal class Object
    {
        public string Name;
        public Vector2 Position;
        public bool Enabled;

        public List<Component> Components;

        public Object(string name, Vector2 position, bool enabled)
        {
            Name = name;
            Position = position;
            Enabled = enabled;
        }
    }

    internal class PrimaryObject : Object
    {
        public Texture2D Texture;
        public Color ObjectColor;

        public PrimaryObject(string name, Vector2 position, Texture2D texture, Color object_color, bool enabled) : base(name, position, enabled)
        {
            Texture = texture;
            ObjectColor = object_color;
        }

        public void DrawIt(SpriteBatch sprite_batch)
        {
            if (this.Enabled)
                sprite_batch.Draw(this.Texture, this.Position, this.ObjectColor);
        }
    }

    internal class PlayerObject : PrimaryObject
    {
        public float Speed;
        public Vector2 MoveDirection;

        // ANIMATION

        public PlayerObject(string name, Vector2 position, Texture2D texture, Color object_color, float speed, bool enabled) : base(name, position, texture, object_color, enabled)
        {
            this.Speed = speed;
        }

        public bool IfCollision(PrimaryObject object1)
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

    internal class CameraObject : Object
    {
        public Matrix Transform { get; private set; }
        public Vector2 CenterProperties;
        private Viewport Viewport;
        private float Zoom;

        public CameraObject(string name, Viewport viewport, Vector2 position, float zoom, bool enabled) : base(name, position, enabled)
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
    #endregion
}