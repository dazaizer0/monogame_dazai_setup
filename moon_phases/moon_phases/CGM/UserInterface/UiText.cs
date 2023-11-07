using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace moon_phases.CGM.UserInterface
{
    internal class UiText : Panel
    {
        public string Text;
        public Color TextColor;
        public SpriteFont Font;

        public UiText(Vector2 screen_center, Vector2 position, SpriteFont font, string text, Color textColor, bool enabled) : base(position, screen_center, enabled)
        {
            Text = text;
            TextColor = textColor;
            Font = font;
        }

        public void RefreshPosition(Vector2 position, Vector2 ui_center)
        {
            this.Position = new Vector2(ui_center.X + position.X, ui_center.Y + position.Y);
        }

        public void TypeIt(SpriteBatch sprite_batch)
        {
            if (this.Enabled)
                sprite_batch.DrawString(this.Font, this.Text, this.Position, this.TextColor);
        }
    }
}
