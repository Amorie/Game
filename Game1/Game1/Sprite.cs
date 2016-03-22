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
        private float _rotationSpeed;
        private float _angle;
        private Vector2 _position;
        private Rectangle _rectangle;
        private Texture2D _texture;
        private Rectangle? _bounds;
        private Vector2 _orgin;
        private Vector2 _scale;
        private Matrix _transform;
        private float _scaleValue;
        

        public Sprite(Vector2 position, float speed = 0, float angle = 0, float rotationSpeed = 0, float scale = 1.0f, Rectangle? bounds = null)
        {
            _position = position;
            _rotationSpeed = rotationSpeed;
            _angle = angle;
            _scale = new Vector2(scale, scale);
            _scaleValue = scale;
            _velocity = new Vector2((float) (speed*Math.Cos(angle)), (float) (speed*Math.Sin(angle)));

            _texture = null;
            _color = Color.White;
            _orgin = Vector2.Zero;

            _bounds = bounds;
        }

        protected Texture2D Texture => _texture;

        public Vector2 Position => _position;

        public Vector2 Velocity => _velocity;

        public Rectangle Rectangle => _rectangle;

        public Vector2 Orgin => _orgin;

        public bool Collided { get; private set; }

        public void LoadContent(ContentManager content, GraphicsDevice graphicDevice, string assetName)
        {
            _texture = content.Load<Texture2D>(assetName);

            OnContentLoaded(content, graphicDevice);

        }

        protected virtual void OnContentLoaded(ContentManager content, GraphicsDevice graphicsDevice)
        {
            _orgin = new Vector2(_texture.Width / 2.0f, _texture.Height / 2.0f);
            UpdateTransformMatrix();
            UpdateRectangle();
            
        }

        private void UpdateTransformMatrix()
        {
            //SRT = Reverse Orgin * Scale * Rotation * Translation
            _transform = Matrix.CreateTranslation(new Vector3(-_orgin, 0)) * 
                Matrix.CreateScale(_scaleValue) * 
                Matrix.CreateRotationZ(_angle) *
                Matrix.CreateTranslation(new Vector3(_position, 0));
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
            Vector2 topLeft = Vector2.Transform(Vector2.Zero, _transform);
            Vector2 topRight = Vector2.Transform( new Vector2(_texture.Width, 0), _transform);
            Vector2 bottomLeft = Vector2.Transform(new Vector2(0, _texture.Height), _transform);
            Vector2 bottomeRight = Vector2.Transform(new Vector2(_texture.Width, _texture.Height), _transform);

            Vector2 min = new Vector2(MathEx.Min(topLeft.X, topRight.X, bottomeRight.X, bottomLeft.X ),
                MathEx.Min(topLeft.Y, topRight.Y, bottomeRight.Y, bottomLeft.Y));

            Vector2 max = new Vector2(MathEx.Max(topLeft.X, topRight.X, bottomeRight.X, bottomLeft.X),
                MathEx.Max(topLeft.Y, topRight.Y, bottomeRight.Y, bottomLeft.Y));

            _rectangle = new Rectangle((int)min.X, (int)min.Y, (int)(max.X - min.X), (int)(max.Y - min.Y));
        }

        public virtual void Unload()
        {
            _texture.Dispose();
        }

        public void Update(GameTime gameTime)
        {
            _position += _velocity*(float) gameTime.ElapsedGameTime.TotalSeconds;

            UpdateRotation(gameTime);
            UpdateTransformMatrix();
            UpdateRectangle();
            CheckBounds();
        }

        private void UpdateRotation(GameTime gameTime)
        {
            _angle += (float)(_rotationSpeed * gameTime.ElapsedGameTime.TotalSeconds);

            //Keep angle between 0 and 360
            // if angle is negative then subtract angle from 360 degree ie 2pi - angle 2pi = 360
            //if anagle is greater than 360 subtract 360.. 370 == 10 degrees
            if (_angle < 0)
            {
                _angle = MathHelper.TwoPi - Math.Abs(_angle);
            }
            else if (_angle > MathHelper.TwoPi)
            {
                _angle = _angle - MathHelper.TwoPi;
            }
        }

        public bool Collision(Sprite target)
        {
            Vector2 normal;
            var intersects = CollisionClass.RectangularCollision(this, target, out normal) ;
            Collided = intersects;
            target.Collided = intersects;

            if (intersects)
            {
                _velocity = _velocity.Reflect(normal);
            }
            return intersects;

        }

//        private bool PerPixelCollision(Sprite target)
//        {
//            //relativeToB * transformB = relativeToA * transformA
//            //relativeToB = relativeToA * transformA * (invert)TranformB
//            //atob = transformA * (invert)TransformB
//            //relativeToB = relativeToA * atob
//
//            Matrix atob = _transform*Matrix.Invert(target._transform);
//
//            var sourceColors = new Color[_texture.Width * _texture.Height];
//            _texture.GetData(sourceColors);
//
//            var targetColors = new Color[target._texture.Width * target._texture.Height];
//            _texture.GetData(targetColors);
//
//            Vector2 stepX = Vector2.TransformNormal(Vector2.UnitX, atob);
//            Vector2 stepY = Vector2.TransformNormal(Vector2.UnitY, atob);
//
//            Vector2 targetPosition = Vector2.Transform(Vector2.Zero, atob);
//
//            for (int x = 0; x < _texture.Width; x++)
//            {
//                Vector2 currentTargetPosition = targetPosition;
//
//                for (int y = 0; y < _texture.Height; y++)
//                {
//                    int targetX = (int) currentTargetPosition.X;
//                    int targetY = (int) currentTargetPosition.Y;
//
//                    if (targetX >= 0 && targetX < target._texture.Width && targetY >= 0 &&
//                        targetY < target._texture.Height)
//                    {
//                        Color colorSource = sourceColors[x + y * _texture.Width];
//                        Color colorTarget = targetColors[targetX + targetY*target._texture.Width];
//
//                        if (colorSource.A != 0 && colorTarget.A != 0)
//                        {
//                            return true;
//                        }
//                    }
//                    currentTargetPosition += stepY;
//                }
//                targetPosition += stepX;
//            }
//            return false;
//        }

    public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(_texture, _position, null, null, _orgin, _angle, _scale, _color);
        }
    }

    
}