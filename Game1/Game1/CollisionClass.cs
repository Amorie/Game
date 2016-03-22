using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Game1
{
    public static class CollisionClass
    {
        public static bool RectangularCollision(Sprite a, Sprite B)
        {
            return RectangularCollision(a.Rectangle, B.Rectangle);
        }

        public static bool RectangularCollision(Sprite a, Sprite b, out Vector2 normal)
        {
            normal = Vector2.Zero;

            if (!RectangularCollision(a.Rectangle, b.Rectangle))
            {
                return false;
            }

            Vector2 delta = a.Position - b.Position;
            float absDeltax = Math.Abs(delta.X);
            float absDeltay = Math.Abs(delta.Y);

            //Right side
            if(delta.X > 0 && absDeltay < b.Rectangle.Height / 2.0 && a.Velocity.X < 0) normal = new Vector2(1, 0);
            //Left side
            if (delta.X < 0 && absDeltay < b.Rectangle.Height / 2.0 && a.Velocity.X > 0) normal = new Vector2(-1, 0);
            //Top side
            if (delta.Y < 0 && absDeltax < b.Rectangle.Width / 2.0 && a.Velocity.Y > 0) normal = new Vector2(0, -1);
            //Bottom side
            if (delta.Y > 0 && absDeltax < b.Rectangle.Width / 2.0 && a.Velocity.Y < 0) normal = new Vector2(0, 1);


            var epsilon = 0.1;
            if ((Math.Abs(delta.X - -b.Rectangle.Width / 2.0) < epsilon && Math.Abs(delta.Y - b.Rectangle.Height / 2.0) < epsilon) ||
                (Math.Abs(delta.X - b.Rectangle.Width / 2.0) < epsilon && Math.Abs(delta.Y - b.Rectangle.Height / 2.0) < epsilon) ||
                (Math.Abs(delta.X - -b.Rectangle.Width / 2.0) < epsilon && Math.Abs(delta.Y - -b.Rectangle.Height / 2.0) < epsilon) ||
                (Math.Abs(delta.X - b.Rectangle.Width / 2.0) < epsilon && Math.Abs(delta.Y - -b.Rectangle.Height / 2.0) < epsilon))
            {
                normal = new Vector2(delta.X, delta.Y);
                normal.Normalize();
            }
            return true;
        }

        public static bool RectangularCollision(Rectangle a, Rectangle b)
        {
            return a.Intersects(b);
        }
        
        
    }
}
