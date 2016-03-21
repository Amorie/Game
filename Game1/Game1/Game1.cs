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
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private DebugSprite _ball1, _ball2;
        private Color _clearColor, _collisionColor;

        private readonly Rectangle _gameDimensions;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1280,
                PreferredBackBufferHeight = 720
            };
            

            Content.RootDirectory = "Content";

            _gameDimensions = new Rectangle(0,0, 1280,720);
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
           
            _ball1 = new DebugSprite(new Vector2(0, (_graphics.GraphicsDevice.Viewport.Height /2.0f) - 120), Color.White, 70, 0,_gameDimensions );
            _ball2 = new DebugSprite(new Vector2(_graphics.GraphicsDevice.Viewport.Width, (_graphics.GraphicsDevice.Viewport.Height / 2.0f) + 120),Color.White, 60, MathHelper.Pi, _gameDimensions );
            _clearColor = Color.CornflowerBlue;
            _collisionColor = Color.Red;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            
            _ball1.LoadContent(Content, GraphicsDevice, "Graphics\\arrow");
            _ball2.LoadContent(Content, GraphicsDevice, "Graphics\\arrow");

            
            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
           
            
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            _ball1.Update((gameTime));
            _ball2.Update(gameTime);

            _ball1.Collision(_ball2);
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            if (_ball1.Collided || _ball2.Collided)
            {
                GraphicsDevice.Clear(_collisionColor);
            }
            else
            {
                GraphicsDevice.Clear(_clearColor);
            }
            

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _ball1.Draw(_spriteBatch, gameTime);
            _ball2.Draw(_spriteBatch, gameTime);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
