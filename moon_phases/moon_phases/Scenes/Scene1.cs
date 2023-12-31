﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

using moon_phases.CGM;
using moon_phases.CGM.GameManager;
using moon_phases.CGM.UserInterface;
using moon_phases.CGM.GameObjects;

using TiledSharp;

using moon_phases.CGM.CGMLevel;

namespace moon_phases.Scenes
{
    public class Scene1 : Game
    {
        #region VARIABLES
        // ELEMENTARY
        private GraphicsDeviceManager graphics;
        private SpriteBatch sprite_batch;

        private GLobalSceneVariables gLobal_variables;
        private SceneProperties scene_properties = new SceneProperties(32, 64);

        // USER INTERFACE
        private Panel user_interface;
        private UiText mouse_coordinates_text;

        // STATIC TEXTURES
        private static Texture2D collision_object_texture;
        private static Texture2D collectable_object_texture;
        private static Texture2D pixel_texture;

        // GAME OBJECTS
        BasicObject collision_object = new BasicObject(new Vector2(250, 460), 0.0f, collision_object_texture, Color.White, true);
        BasicObject collectable_object = new BasicObject( new Vector2(700, 460), 0.0f, collectable_object_texture, Color.Magenta, true);

        // PLAYER
        private static Texture2D player_texture;
        Player player = new Player(new Vector2(0, 0), 0.0f, player_texture, 200f, Color.White, true);
        Camera camera; // CAMERA
        #endregion

        #region SETUP
        public Scene1()
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

            #region PLAYER OBJECT AND PLAYER CAMERA INITIALIZE
            // INITIALIZE PLAYER PROPERTIES
            player.Position = new Vector2(
                graphics.PreferredBackBufferWidth / 2 / scene_properties.GridSize * scene_properties.GridSize,
                graphics.PreferredBackBufferHeight / 2 / scene_properties.GridSize * scene_properties.GridSize);

            player.Position = new Vector2(player.Position.X + 96, player.Position.Y + 64);

            // SPRINT
            player.SprintSpeed = player.BaseSpeed + 128f;

            // CAMERA
            camera = new Camera(new Vector2(0, 0), 0.0f, GraphicsDevice.Viewport, 1.25f, true);
            camera.CenterProperties = new Vector2(410, 250);
            #endregion

            // USER INTERFACE
            user_interface = new Panel(new Vector2(0, 0), 0.0f, gLobal_variables.GlobalScreenCenter, true);
            mouse_coordinates_text = new UiText(gLobal_variables.GlobalScreenCenter, new Vector2(20, 20), 0.0f,
                Content.Load<SpriteFont>("fonts/prototype_font"), "Moon Phases", Color.White, true);

            // GAME OBJECTS
            new Vector2((int)(collectable_object.Position.X / scene_properties.GridSize) * scene_properties.GridSize,
                (int)(collectable_object.Position.Y / scene_properties.GridSize) * scene_properties.GridSize);

            collectable_object.Position =
                new Vector2((int)(collectable_object.Position.X / scene_properties.GridSize) * scene_properties.GridSize,
                (int)(collectable_object.Position.Y / scene_properties.GridSize) * scene_properties.GridSize);

            // GRAVITY
            GLobalSceneVariables.Gravity = new Vector2(0, 300.0F);

            base.Initialize();
        }
        #endregion

        #region LOAD_CONTETNT
        protected override void LoadContent()
        {
            // SPRITE BATCH
            sprite_batch = new SpriteBatch(GraphicsDevice);

            // INITIALIZE TEXTURES
            player.Texture = Content.Load<Texture2D>("textures/white_circle32");
            collision_object.Texture = Content.Load<Texture2D>("textures/triangle");
            collectable_object.Texture = Content.Load<Texture2D>("textures/white_circle32");

            // PIXEL TEXTURE
            pixel_texture = new Texture2D(GraphicsDevice, 1, 1);
            pixel_texture.SetData(new Color[] { Color.White });
        }
        #endregion

        #region UPDATE
        protected override void Update(GameTime game_time)
        {
            #region ELEMENTARY_PROPERTIES
            // EXIT 
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // GLOBAL SCREEN CENTER
            gLobal_variables.GlobalScreenCenter = new Vector2(camera.Position.X - camera.CenterProperties.X,
                camera.Position.Y - camera.CenterProperties.Y);
            user_interface.Position = gLobal_variables.GlobalScreenCenter;
            #endregion

            // USER INTERFACE
            mouse_coordinates_text.RefreshPosition(new Vector2(100, 100), user_interface.Position);

            // CAMERA
            camera.Fallow(new Vector2(player.Position.X + camera.CenterProperties.X, player.Position.Y + camera.CenterProperties.Y), game_time);

            #region PLAYER_MANAGEMENT
            // MOUSE CONTROLL
            MouseState mouse_state = Mouse.GetState();
            if (mouse_state.LeftButton == ButtonState.Pressed)
            {
                gLobal_variables.MouseClickPosition = new Vector2(mouse_state.X, mouse_state.Y);
                mouse_coordinates_text.Text = $"{(int)gLobal_variables.MouseClickPosition.X / scene_properties.GridSize}," +
                    $" {(int)gLobal_variables.MouseClickPosition.Y / scene_properties.GridSize}";

                // camera.Shake(0.04f, 6f);
            }

            // PLAYER MOVEMENT
            player.AccelerateMovement(game_time, GLobalSceneVariables.Gravity);
            player.AcceleratePhysics(game_time, GLobalSceneVariables.Gravity);

            #region PLAYER_COLLISIONS
            // PLAYER BORDERS COLLISIONS
            if (player.Position.X > graphics.PreferredBackBufferWidth - player.Texture.Width / 2) // RIGHT
            {
                player.JumpEnable = true;
                player.Position.X = graphics.PreferredBackBufferWidth - player.Texture.Width / 2;
            }
            else if (player.Position.X < player.Texture.Width / 2) // LEFT
            {
                player.JumpEnable = true;
                player.Position.X = player.Texture.Width / 2;
            }

            if (player.Position.Y > graphics.PreferredBackBufferHeight - player.Texture.Height / 2) // BOTTOM
            {
                player.JumpEnable = true;
                player.Position.Y = graphics.PreferredBackBufferHeight - player.Texture.Height / 2;
            }

            // PLAYER COLLISIONS WITH OBJECT1
            if (player.IfCollision(collision_object)) // STOP
            {
                collision_object.GetPlayerMoveDirection(player);
                player.Position -= collision_object.PlayerMoveDirection * player.Speed * (float)game_time.ElapsedGameTime.TotalSeconds;
            }

            if (player.IfCollision(collectable_object)) // COLLECT
            {
                if (collectable_object.Enabled == true)
                    camera.Shake(0.1f, 8f); // SHAKE
                collectable_object.Enabled = false;
            }
            #endregion
            #endregion

            collectable_object.LookAt(player.Position, game_time);
            // collision_object.LookAt(player.Position, game_time);

            base.Update(game_time);
        }
        #endregion

        #region DRAW
        protected override void Draw(GameTime gameTime)
        {
            // BACKGROUND DEFAULT COLOR
            GraphicsDevice.Clear(Color.Magenta);

            #region SPRITE_BATCH_DRAWING
            sprite_batch.Begin(transformMatrix: camera.Transform);

            #region WORLD 

            for (int x = 0; x < scene_properties.GridSize * 32; x += scene_properties.GridSize)
            {
                for (int y = 0; y < scene_properties.GridSize * 24; y += scene_properties.GridSize)
                {
                    // sprite_batch.Draw(pixel_texture, new Rectangle(x, y, scene_properties.GridSize, scene_properties.GridSize), Color.White);
                    sprite_batch.Draw(pixel_texture, new Rectangle(x, y, scene_properties.GridSize, scene_properties.GridSize), Color.Black);
                }
            }

            // TEMPORARY GROUND -------------------------------------------------------------------------------------------------------------------------
            for (int x = 0; x < scene_properties.GridSize * 32; x++)
            {
                sprite_batch.Draw(pixel_texture, new Rectangle(x, graphics.PreferredBackBufferHeight, scene_properties.GridSize, scene_properties.GridSize), Color.Green);
            }
            #endregion

            // GAME OBJECTS
            player.DrawIt(sprite_batch);
            collision_object.DrawIt(sprite_batch);
            collectable_object.DrawIt(sprite_batch);

            // USER INTERFACE
            mouse_coordinates_text.TypeIt(sprite_batch);

            sprite_batch.End();
            #endregion

            base.Draw(gameTime);
        }
        #endregion
    }
}