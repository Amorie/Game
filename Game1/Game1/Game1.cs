using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Shooter;
namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;
        private KeyboardState keyState;
       
        private Vector2 position, velocity;
        private Texture2D spaceship;
        private float speed = 120;
        Player player;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            player = new Player();
            keyState = Keyboard.GetState();
            velocity = Vector2.Zero;
            position = new Vector2(100, 200);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Vector2 playerPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y +
                GraphicsDevice.Viewport.TitleSafeArea.Height / 2);
            //player.Initialize(Content.Load<Texture2D>("Graphics\\PlayerPaper"), playerPosition);
            spaceship = Content.Load<Texture2D>("Graphics\\PlayerPaper");
            spriteFont = Content.Load<SpriteFont>("basicFont");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            spaceship.Dispose();
            spriteFont = null;
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            keyState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            //positionX = cos(angle in radians) * speed
            //positionY = sin(angle in radians) * speed

            float? angle = null;
            if (keyState.IsKeyDown(Keys.Up))
            {
                angle = 3.0f * MathHelper.PiOver2;
            }
            if(keyState.IsKeyDown(Keys.Down))
            {
                angle = MathHelper.PiOver2;
            }
            if(keyState.IsKeyDown(Keys.Left))
            {
                angle = MathHelper.Pi;
            }
            if(keyState.IsKeyDown(Keys.Right))
            {
                angle = 0;
            }
            if(angle.HasValue)
            {
                velocity = new Vector2((float)Math.Cos(angle.Value) * speed, (float)Math.Sin(angle.Value) * speed);
            }
            else
            {
                velocity = Vector2.Zero;
            }
            //pixels = px
            //seconds = s(or any time)
            //position = px
            //velocity = px/s 
            //accelleration = px/s^2
            //v * s = px
            //a * s = v

            //position = position + velocity * time
            position = Vector2.Add(position, Vector2.Multiply(velocity, (float)gameTime.ElapsedGameTime.TotalSeconds));
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(spaceship, position, Color.Yellow);
            spriteBatch.DrawString(spriteFont, "Hello World", Vector2.Zero, Color.Black);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
