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
    class PrimaryObject
    {
        public string Name;

        public Vector2 Position;
        public Texture2D Texture;

        public PrimaryObject(string name, Vector2 position, Texture2D texture)
        {
            Name = name;
            Position = position;
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
    #endregion
}
