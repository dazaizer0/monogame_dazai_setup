using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using moon_phases.Classes;

namespace moon_phases
{
    public class Main : Game
    {
        #region VARIABLES
        // ELEMENTARY
        private GLobalVariables gLobal_variables;
        private GraphicsDeviceManager graphics;
        private SpriteBatch sprite_batch;

        // UI
        Classes.UserInterfacePanel user_interface_panel;
        Classes.UiText text1;

        // TEXTURES
        private static Texture2D player_texture;
        private static Texture2D object_texture;
        private Texture2D pixel_texture;

        // OBJECTS
        Classes.PlayerObject player_object = new Classes.PlayerObject("player", new Vector2(0, 0), player_texture, Color.White, 200f);
        Classes.PrimaryObject primary_object_1 = new Classes.PrimaryObject("object1", new Vector2(248, 248), object_texture, Color.White);

        // CAMERA
        Classes.CameraObject camera;
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
            // GAME PROPERTIES
            Window.Title = "Moon Phases";
            gLobal_variables.GridSize = 32;

            // INITIALIZE PLAYER PROPERTIES
            player_object.Position = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);

            // CAMERA
            camera = new CameraObject("camera", GraphicsDevice.Viewport, new Vector2(0, 0), 0.8f);
            camera.CenterProperties = new Vector2(410, 250);

            // UI
            user_interface_panel = new UserInterfacePanel("mainui", new Vector2(0, 0), gLobal_variables.GlobalScreenCenter);
            text1 = new UiText("text1", "Hello World", gLobal_variables.GlobalScreenCenter, new Vector2(20, 20), Content.Load<SpriteFont>("fonts/prototype_font"), Color.Black);

            base.Initialize();
        }
        #endregion

        #region LOAD_CONTETNT
        protected override void LoadContent()
        {
            sprite_batch = new SpriteBatch(GraphicsDevice);

            // GAME
            player_object.Texture = Content.Load<Texture2D>("textures/prototype"); // INITIALIZE PLAYER TEXTURE
            primary_object_1.Texture = Content.Load<Texture2D>("textures/prototype"); // INITIALIZE OBECT1 TEXTURE

            // PIXEL TEXTURE
            pixel_texture = new Texture2D(GraphicsDevice, 1, 1);
            pixel_texture.SetData(new Color[] { Color.White });
        }
        #endregion

        #region UPDATE
        protected override void Update(GameTime gameTime)
        {
            #region ELEMENTARY_PROPERTIES
            // EXIT 
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // GLOBAL CENTER
            gLobal_variables.GlobalScreenCenter = new Vector2(camera.Position.X  - camera.CenterProperties.X, camera.Position.Y - camera.CenterProperties.Y); // GLOBAL 0 POSITION
            user_interface_panel.Position = gLobal_variables.GlobalScreenCenter;
            #endregion

            #region UI
            // UI
            text1.RefreshPosition(new Vector2(0, 0), user_interface_panel.Position);
            #endregion

            #region CAMERA
            // CAMERA
            camera.Refresh(new Vector2(player_object.Position.X + camera.CenterProperties.X, player_object.Position.Y + camera.CenterProperties.Y));
            #endregion

            #region PLAYER_MANAGEMENT
            // MOUSE CONTROLL
            MouseState mouse_state = Mouse.GetState();
            if (mouse_state.LeftButton == ButtonState.Pressed)
            {
                gLobal_variables.MouseClickPosition = new Vector2(mouse_state.X, mouse_state.Y);
                text1.Text = $"{(int)gLobal_variables.MouseClickPosition.X / gLobal_variables.GridSize}, {(int)gLobal_variables.MouseClickPosition.Y / gLobal_variables.GridSize}";
            }

            // PLAYER MOVEMENT
            Vector2 moveDirection = primary_object_1.Position - player_object.Position;
            moveDirection.Normalize();

            var keyboard_state = Keyboard.GetState();

            if (keyboard_state.IsKeyDown(Keys.W))
            {
                player_object.Position.Y -= player_object.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (keyboard_state.IsKeyDown(Keys.S))
            {
                player_object.Position.Y += player_object.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (keyboard_state.IsKeyDown(Keys.D))
            {
                player_object.Position.X += player_object.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (keyboard_state.IsKeyDown(Keys.A))
            {
                player_object.Position.X -= player_object.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            #region PLAYER_COLLISIONS
            // PLAYER COLLISIONS
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
            #endregion

            base.Update(gameTime);
        }
        #endregion

        #region DRAW
        protected override void Draw(GameTime gameTime)
        {
            // BACKGROUND DEFAULT COLOR
            GraphicsDevice.Clear(Color.CornflowerBlue);

            #region SPRITE_BATCH_DRAWING
            // DRAW
            sprite_batch.Begin(transformMatrix: camera.Transform);

            #region WORLD 
            // WORLD
            for (int x = 0; x < gLobal_variables.GridSize * 16; x += gLobal_variables.GridSize)
            {
                for (int y = 0; y < gLobal_variables.GridSize * 16; y += gLobal_variables.GridSize)
                {
                    bool isVisible = (x / gLobal_variables.GridSize) % 2 == 0 && (y / gLobal_variables.GridSize) % 2 == 0;
                    if (isVisible)
                    {
                        sprite_batch.Draw(pixel_texture, new Rectangle(x, y, gLobal_variables.GridSize, gLobal_variables.GridSize), Color.White);
                    }
                }
            }
            #endregion

            // GAME OBJECTS
            player_object.DrawIt(sprite_batch);
            primary_object_1.DrawIt(sprite_batch);

            // UI
            text1.TypeIt(sprite_batch);

            sprite_batch.End();
            #endregion

            base.Draw(gameTime);
        }
        #endregion
    }
}