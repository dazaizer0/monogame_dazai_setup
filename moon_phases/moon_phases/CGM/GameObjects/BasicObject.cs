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

        public BasicObject(Vector2 position, Texture2D texture, Color object_color, bool enabled) : base(position, enabled)
        {
            Texture = texture;
            ObjectColor = object_color;
        }

        public void DrawIt(SpriteBatch sprite_batch)
        {
            if (this.Enabled)
                sprite_batch.Draw(this.Texture, this.Position, this.ObjectColor);
        }

        public void GetPlayerMoveDirection(Player player)
        {
            this.PlayerMoveDirection = this.Position - player.Position;
            this.PlayerMoveDirection.Normalize();
        }
    }
}
