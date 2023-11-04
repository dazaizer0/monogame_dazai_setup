using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace moon_phases.Classes
{
    #region NEW OBJECT
    struct GLobalVariables
    {
        public Vector2 GlobalZeroPosition;
    }

    internal class Node
    {
        public string Name;
        public Vector2 Position;

        public Node(string name, Vector2 position)
        {
            Name = name;
            Position = position;
        }
    }

    internal class PrimaryObject : Node
    {
        public Texture2D Texture;

        public PrimaryObject(string name, Vector2 position, Texture2D texture) : base(name, position)
        {
            Texture = texture;
        }
    }

    internal class PlayerObject : PrimaryObject
    {
        public float Speed;

        public PlayerObject(string name, Vector2 position, Texture2D texture, float speed) : base(name, position, texture)
        {
            this.Speed = speed;
        }

        public bool IfCollision(PrimaryObject object1)
        {
            Rectangle rect1 = new Rectangle((int)this.Position.X, (int)this.Position.Y, (int)this.Texture.Width, (int)this.Texture.Height);
            Rectangle rect2 = new Rectangle((int)object1.Position.X, (int)object1.Position.Y, (int)object1.Texture.Width, (int)object1.Texture.Height);

            return (rect1.Intersects(rect2) == true);
        }
    }

    internal class Camera : Node
    {
        public Matrix Transform { get; private set; }
        private Viewport Viewport;
        private float Zoom;

        public Camera(string name, Viewport viewport, Vector2 position, float zoom) : base(name, position)
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

    class TextNode : Node
    {
        string Text;
        Color TextColor;
        SpriteFont Font;

        public TextNode(string name, string text, Color textColor, Vector2 position, SpriteFont font) : base(name, position)
        {
            Text = text;
            TextColor = textColor;
            Font = font;
        }

        public void Refresh(Vector2 global_zero_positon)
        {
            this.Position = global_zero_positon + this.Position;
        }
    }
    #endregion
}
