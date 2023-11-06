using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

using static System.Net.Mime.MediaTypeNames;

namespace moon_phases.Classes
{
    #region NEW UI OBJECT SYSTEM
    internal class UserInterfacePanel : Object
    {
        public Vector2 ScreenCenter;

        public UserInterfacePanel(string name, Vector2 position, Vector2 screen_center, bool enabled) : base(name, position, enabled)
        {
            ScreenCenter = screen_center;
        }
    }

    internal class UserInterfaceText : UserInterfacePanel
    {
        public string Text;
        public Color TextColor;
        public SpriteFont Font;

        public UserInterfaceText(string name, string text, Vector2 screen_center, Vector2 position, SpriteFont font, Color textColor, bool enabled) : base(name, position, screen_center, enabled)
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
    #endregion
}
