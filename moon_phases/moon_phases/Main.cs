using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace moon_phases
{
    public class Main : Game
    {
        #region VARIABLES
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private SpriteFont arial;

        private static Texture2D player_texture;
        Classes.Player player = new Classes.Player("player", 100f, new Vector2(0, 0), player_texture);

        private static Texture2D object_texture;
        Classes.Object object1 = new Classes.Object("object1", new Vector2(200, 200), object_texture);
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
            Window.Title = "Moon phases";
            player.playerPosition = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2); // INITIALIZE PLAYER PROPERTIES
            base.Initialize();
        }
        #endregion

        #region LOAD_CONTETNT
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            arial = Content.Load<SpriteFont>("fonts/prototype_font");

            player.playerTexture = Content.Load<Texture2D>("textures/prototype"); // INITIALIZE PLAYER TEXTURE
            object1.objectTexture = Content.Load<Texture2D>("textures/prototype"); // INITIALIZE OBECT1 TEXTURE
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
            Vector2 moveDirection = object1.objectPosition - player.playerPosition;
            moveDirection.Normalize();

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
            else if (player.playerPosition.Y < player.playerTexture.Height / 2)
            {
                player.playerPosition.Y = player.playerTexture.Height / 2;
            }

            // PLAYER COLLISIONS WITH OBJECT1
            if (player.Collision(player, object1) == true)
            {
                player.playerPosition -= moveDirection * player.playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
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
            spriteBatch.Draw(object1.objectTexture, object1.objectPosition, Color.White); // DRAW OBJECT1

            spriteBatch.DrawString(arial, "TestTestTest", new Vector2(20, 20), Color.White);
            spriteBatch.End();
            #endregion

            base.Draw(gameTime);
        }
        #endregion
    }
}