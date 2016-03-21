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

    //positionX = cos(angle in radians) * speed
    //positionY = sin(angle in radians) * speed


    //pixels = px
    //seconds = s(or any time)
    //position = px
    //velocity = px/s 
    //accelleration = px/s^2
    //v * s = px
    //a * s = v

    //position = position + velocity * time
    public class Sprite
    {
        private Color _color;
        private Vector2 _velocity;
        private Vector2 _position;
        private Rectangle _rectangle;
        private Texture2D _texture;
        private Rectangle? _bounds;

        public Sprite(Vector2 position, float speed = 0, float angle = 0, Rectangle? bounds = null)
        {
            _position = position;
            _velocity = new Vector2((float) (speed*Math.Cos(angle)), (float) (speed*Math.Sin(angle)));

            _texture = null;
            _color = Color.White;

            _bounds = bounds;
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
            CheckBounds();
        }

        private void CheckBounds()
        {
            if (_bounds == null)
            {
                return;
            }

            Vector2 change = Vector2.Zero;

            if (_rectangle.Left <= _bounds.Value.X)
            {
                change.X = _bounds.Value.X - _rectangle.Left;
            }
            else if (_rectangle.Right >= _bounds.Value.Right)
            {
                change.X = _bounds.Value.Right - _rectangle.Right;
            }
            if (_rectangle.Top <= _bounds.Value.Y)
            {
                change.Y = _bounds.Value.Y - _rectangle.Top;
            }
            else if (_rectangle.Bottom >= _bounds.Value.Bottom)
            {
                change.Y = _bounds.Value.Bottom - _rectangle.Bottom;
            }

            if (change == Vector2.Zero)
            {
                return;
            }

            _position = new Vector2((int) _position.X + change.X, (int) _position.Y + change.Y);
            UpdateRectangle();

        }

        private void UpdateRectangle()
        {
            _rectangle = new Rectangle((int) _position.X, (int) _position.Y, _texture.Width, _texture.Height);
        }

        public virtual void Unload()
        {
            _texture.Dispose();
        }

        public void Update(GameTime gameTime)
        {
            _position += _velocity*(float) gameTime.ElapsedGameTime.TotalSeconds;

            UpdateRectangle();
            CheckBounds();
        }

        public bool Collision(Sprite target)
        {
            var intersects = _rectangle.Intersects(target._rectangle) && PerPixelCollision(target);
            Collided = intersects;
            target.Collided = intersects;
            return intersects;

        }

        private bool PerPixelCollision(Sprite target)
        {
            var sourceColors = new Color[_texture.Width*_texture.Height];
            _texture.GetData((sourceColors));

            var targetColors = new Color[target._texture.Width*target._texture.Height];
            target._texture.GetData(targetColors);

            var left = Math.Max(_rectangle.Left, target._rectangle.Left);
            var top = Math.Max(_rectangle.Top, target._rectangle.Top);
            var width = Math.Min(_rectangle.Right, target._rectangle.Right) - left;
            var height = Math.Min(_rectangle.Bottom, target._rectangle.Bottom) - top;


            var intersectingRectangle = new Rectangle(left, top, width, height);

            for (var x = intersectingRectangle.Left; x < intersectingRectangle.Right; x++)
            {
                for (var y = intersectingRectangle.Top; y < intersectingRectangle.Bottom; y++)
                {
                    var sourceColor = sourceColors[(x - _rectangle.Left) + (y - _rectangle.Top)*_texture.Width];
                    var targetColor =
                        targetColors[(x - target._rectangle.Left) + (y - target._rectangle.Top) * target._texture.Width];

                    if (sourceColor.A > 0 && targetColor.A > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

    public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(_texture, _position, _color);
        }
    }
}
