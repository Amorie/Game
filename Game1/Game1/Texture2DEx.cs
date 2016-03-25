using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public static class Texture2DEx
    {
        public static void GetData<T>(this Texture2D texture, T[,] data) where T : struct
        {
            T[] dataOneDeminsions = new T[texture.Width * texture.Height];
            texture.GetData(dataOneDeminsions);
            for (int x = 0; x < texture.Width; x++)
            {
                for (int y = 0; y < texture.Height; y++)
                {
                    data[x, y] = dataOneDeminsions[x + y * texture.Width];
                }
            }
        }
    }
}
