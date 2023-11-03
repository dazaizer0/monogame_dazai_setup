using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ino.Classes
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
    }
    #endregion
}
