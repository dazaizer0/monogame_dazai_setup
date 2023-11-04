using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace moon_phases
{
    public class Main : Game
    {
        #region VARIABLES
        // ELEMENTARY
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private SpriteFont arial;

        // TEXTURES
        private static Texture2D player_texture;
        private static Texture2D object_texture;

        // OBJECTS
        Classes.PlayerObject player_object = new Classes.PlayerObject("player", new Vector2(0, 0), player_texture, 200f);
        Classes.PrimaryObject primary_object_1 = new Classes.PrimaryObject("object1", new Vector2(248, 248), object_texture);
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
            Window.Title = "Moon Phases";
            player_object.Position = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2); // INITIALIZE PLAYER PROPERTIES
            base.Initialize();
        }
        #endregion

        #region LOAD_CONTETNT
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // UI
            arial = Content.Load<SpriteFont>("fonts/prototype_font"); // INITIALIZE TEXT

            // GAME
            player_object.Texture = Content.Load<Texture2D>("textures/prototype"); // INITIALIZE PLAYER TEXTURE
            primary_object_1.Texture = Content.Load<Texture2D>("textures/prototype"); // INITIALIZE OBECT1 TEXTURE
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
            Vector2 moveDirection = primary_object_1.Position - player_object.Position;
            moveDirection.Normalize();

            var keyboard_state = Keyboard.GetState();

            if (keyboard_state.IsKeyDown(Keys.Up))
            {
                player_object.Position.Y -= player_object.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (keyboard_state.IsKeyDown(Keys.Down))
            {
                player_object.Position.Y += player_object.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (keyboard_state.IsKeyDown(Keys.Right))
            {
                player_object.Position.X += player_object.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (keyboard_state.IsKeyDown(Keys.Left))
            {
                player_object.Position.X -= player_object.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            #endregion

            // PLAYER COLLISIONS
            #region PLAYER_COLLISIONS
            if (player_object.Position.X > graphics.PreferredBackBufferWidth - player_object.Texture.Width / 2)
            {
                player_object.Position.X = graphics.PreferredBackBufferWidth - player_object.Texture.Width / 2;
            }
            else if (player_object.Position.X < player_object.Texture.Width / 2)
            {
                player_object.Position.X = player_object.Texture.Width / 2;
            }

            if (player_object.Position.Y > graphics.PreferredBackBufferHeight - player_object.Texture.Height / 2)
            {
                player_object.Position.Y = graphics.PreferredBackBufferHeight - player_object.Texture.Height / 2;
            }
            else if (player_object.Position.Y < player_object.Texture.Height / 2)
            {
                player_object.Position.Y = player_object.Texture.Height / 2;
            }

            // PLAYER COLLISIONS WITH OBJECT1
            if (player_object.IfCollision(primary_object_1))
            {
                player_object.Position -= moveDirection * player_object.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
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

            spriteBatch.Draw(player_object.Texture, player_object.Position, Color.White); // DRAW PLAYER
            spriteBatch.Draw(primary_object_1.Texture, primary_object_1.Position, Color.White); // DRAW OBJECT1

            spriteBatch.DrawString(arial, "Moon Phases", new Vector2(20, 20), Color.White);
            spriteBatch.End();
            #endregion

            base.Draw(gameTime);
        }
        #endregion
    }
}