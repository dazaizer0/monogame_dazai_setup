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
    #region OBJECT
    internal class Object
    {
        public string objectName;

        public Vector2 objectPosition;
        public Texture2D objectTexture;

        public Object(string name, Vector2 position, Texture2D texture)
        {
            objectName = name;
            objectPosition = position;
            objectTexture = texture;
        }
    }
    #endregion

    #region PLAYER
    internal class Player
    {
        public string playerName;
        public float playerSpeed;

        public Vector2 playerPosition;
        public Texture2D playerTexture;

        public Player(string name, float speed, Vector2 position, Texture2D texture)
        {
            playerName = name;
            playerSpeed = speed;
            playerPosition = position;
            playerTexture = texture;
        }

        public bool Collision(Player player, Object object1)
        {
            Rectangle rect1 = new Rectangle((int)player.playerPosition.X, (int)player.playerPosition.Y, (int)player.playerTexture.Width, (int)player.playerTexture.Height);
            Rectangle rect2 = new Rectangle((int)object1.objectPosition.X, (int)object1.objectPosition.Y, (int)object1.objectTexture.Width, (int)object1.objectTexture.Height);
            return (rect1.Intersects(rect2) == true);
        }
    }
    #endregion
}
