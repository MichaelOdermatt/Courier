using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Engine
{
    public static class VectorExtensions
    {
        /// <summary>
        /// Returns the projection of this Vector2 onto the given Vector2.
        /// </summary>
        public static Vector2 Project(this Vector2 a, Vector2 b)
        {
            if (b == Vector2.Zero)
            {
                return Vector2.Zero;
            }

            float dotProduct = Vector2.Dot(a, b);
            float bMagnitudeSquared = Vector2.Dot(b, b);
            return (dotProduct / bMagnitudeSquared) * b;
        }

        /// <summary>
        /// Returns the Vector2 rotated by the given radian value.
        /// </summary>
        public static Vector2 Rotate(this Vector2 a, float radians)
        {
            var rotationMatrix = Matrix.CreateRotationZ(radians);
            return Vector2.Transform(a, rotationMatrix);
        }
    }
}
