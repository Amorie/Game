using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Game1
{
    public static class Vector2Ex
    {
        public static Vector2 Reflect(this Vector2 v, Vector2 n)
        {
            return v + Vector2.Multiply(n, 2 * Vector2.Dot(-v, n));
        }
    }
}
