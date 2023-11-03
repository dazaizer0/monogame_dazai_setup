using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ino.Classes;
using System;

namespace ino
{
    public class Main : Game
    {
        #region VARIABLES
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private static Texture2D player_texture;
        Player player = new Player("player", 100, new Vector2(0, 0), player_texture);
        #endregion

        #region SETUP
        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        #endregion

        #region INITIALIZE
        protected override void Initialize()
        {
            player.playerPosition = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2); // INITIALIZE PLAYER PROPERTIES
            player.playerSpeed = 100f;

            base.Initialize();
        }
        #endregion

        #region LOAD_CONTETNT
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            player.playerTexture = Content.Load<Texture2D>("textures/prototype"); // INITIALIZE PLAYER TEXTURE
        }
        #endregion

        #region UPDATE
        protected override void Update(GameTime gameTime)
        {
            // EXIT 
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // PLAYER MOVEMENT
            #region PLAYER_MOVEMENT
            var keyboard_state = Keyboard.GetState();

            if (keyboard_state.IsKeyDown(Keys.Up))
            {
                player.playerPosition.Y -= player.playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (keyboard_state.IsKeyDown(Keys.Down))
            {
                player.playerPosition.Y += player.playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (keyboard_state.IsKeyDown(Keys.Right))
            {
                player.playerPosition.X += player.playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (keyboard_state.IsKeyDown(Keys.Left))
            {
                player.playerPosition.X -= player.playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            #endregion

            // PLAYER COLLISIONS
            #region PLAYER_COLLISIONS
            if (player.playerPosition.X > graphics.PreferredBackBufferWidth - player.playerTexture.Width / 2)
            {
                player.playerPosition.X = graphics.PreferredBackBufferWidth - player.playerTexture.Width / 2;
            }
            else if (player.playerPosition.X < player.playerTexture.Width / 2)
            {
                player.playerPosition.X = player.playerTexture.Width / 2;
            }

            if (player.playerPosition.Y > graphics.PreferredBackBufferHeight - player.playerTexture.Height / 2)
            {
                player.playerPosition.Y = graphics.PreferredBackBufferHeight - player.playerTexture.Height / 2;
            }
            else if(player.playerPosition.Y < player.playerTexture.Height / 2)
            {
                player.playerPosition.Y = player.playerTexture.Height / 2;
            }
            #endregion

            base.Update(gameTime);
        }
        #endregion

        #region DRAW
        protected override void Draw(GameTime gameTime)
        {
            // BACKGROUND DEFAULT COLOR
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // DRAW
            #region SPRITE_BATCH_DRAWING
            spriteBatch.Begin();
            spriteBatch.Draw(player.playerTexture, player.playerPosition, Color.White); // DRAW PLAYER
            spriteBatch.End();
            #endregion

            base.Draw(gameTime);
        }
        #endregion
    }
}