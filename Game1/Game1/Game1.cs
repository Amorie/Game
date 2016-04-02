using System;
using System.Collections.Generic;
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
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private MouseState _mouseState;
        private KeyboardState _keyboardState;
        private SpriteSheet _spriteSheet;
        private DebugSprite _ball, _paddle;
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
            
            this.IsMouseVisible = true;
            _mouseState = new MouseState();
            _keyboardState = new KeyboardState();
            //_ball = new DebugSprite(new Vector2(0, (_graphics.GraphicsDevice.Viewport.Height / 2.0f) - 160), Color.White, 100, MathHelper.ToRadians(10), MathHelper.TwoPi, 1, _gameDimensions );
            _ball = new DebugSprite(new Vector2(_graphics.GraphicsDevice.Viewport.Height - 150, 160), Color.White, 100,0, 2);
            _paddle = new DebugSprite(new Vector2(_graphics.GraphicsDevice.Viewport.Width - 150, _graphics.GraphicsDevice.Viewport.Height / 2.0f),Color.White );
            _spriteSheet = new SpriteSheet("Graphics\\PlayerPaperAnimated", 58, 86, 6 ,11, true);

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
            _spriteSheet.LoadContent(Content, GraphicsDevice);
            _ball.LoadContent(Content, GraphicsDevice, "Graphics\\ball");
            _paddle.LoadContent(Content, GraphicsDevice, "Graphics\\paddle");

            
            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
           _ball.Unload();
           _paddle.Unload();
            
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
            _ball.Update((gameTime));

            if (gameTime.TotalGameTime.TotalSeconds > 5)
            {
                _spriteSheet.Update(gameTime);
            }

            _ball.Collision(_paddle);
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
               
            }
            _mouseState = Mouse.GetState();
            if (_mouseState.RightButton == ButtonState.Pressed)
            {
                _ball.Move(_mouseState.Position);
                
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            if (_ball.Collided)
            {
                GraphicsDevice.Clear(_collisionColor);
            }
            else
            {
                GraphicsDevice.Clear(_clearColor);
            }
            

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _ball.Draw(_spriteBatch, gameTime);
            
            _paddle.Draw(_spriteBatch, gameTime);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
