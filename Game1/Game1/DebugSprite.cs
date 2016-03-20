﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    public class DebugSprite : Sprite 
    {
        private Color _rectangleColor;
        private Texture2D _rectangleTexture;

        public DebugSprite(Vector2 position, Color rectangleColor, float speed = 0, float angle = 0 ) :
        base(position, speed, angle)
        {
            _rectangleColor = rectangleColor;
        }

        protected override void OnContentLoaded(ContentManager content, GraphicsDevice graphicsDevice)
        {
            Color[] colors = new Color[Texture.Width * Texture.Height];
            colors[0] = _rectangleColor;
            colors[Texture.Width - 1] = _rectangleColor;
            colors[(Texture.Width * Texture.Height) - Texture.Width] = _rectangleColor;
            colors[Texture.Width * Texture.Height - 1] = _rectangleColor;
            _rectangleTexture = new Texture2D(graphicsDevice, Texture.Width, Texture.Height);
            _rectangleTexture.SetData(colors);

            base.OnContentLoaded(content, graphicsDevice);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(_rectangleTexture, null, Rectangle, null, null, 0, null, Color.White);
            base.Draw(spriteBatch, gameTime);
        }

    }


   
}