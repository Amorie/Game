using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Game1
{
    public class Sprite
    {
        private Color _color;
        private Vector2 _velocity;
        private Vector2 _position;
        private Rectangle _rectangle;
        private Texture2D _texture;

        public Sprite (Vector2 position, float speed = 0, float angle = 0)
        {
            _position = position;
            _velocity = new Vector2((float)(speed * Math.Cos(angle)), (float)(speed * Math.Sin(angle)));

            _texture = null;
            _color = Color.White;
        }

        protected Texture2D Texture => _texture;

        public Vector2 Position => _position;

        public Rectangle Rectangle => _rectangle;

        public bool Collided { get; private set; }

        public void LoadContent(ContentManager content, GraphicsDevice graphicDevice, string assetName)
        {
            _texture = content.Load<Texture2D>(assetName);

            OnContentLoaded(content, graphicDevice);
            
        }

        protected virtual void OnContentLoaded(ContentManager content, GraphicsDevice graphicsDevice)
        {
            UpdateRectangle();
        }
        
        private void UpdateRectangle()
        {
            _rectangle = new Rectangle((int)_position.X, (int)_position.Y, _texture.Width, _texture.Height);
        }

        public virtual void Unload()
        {
            _texture.Dispose();
        }

        public void Update(GameTime gameTime)
        {
            _position += _velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            UpdateRectangle();
        }
        
        public bool Collision(Sprite target)
        {
            bool intersects = _rectangle.Intersects(target._rectangle);
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(_texture, _position, _color);
        }
    }
}
