using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Engine
{
    /// <summary>
    /// Struct used for drawing the Ground.
    /// </summary>
    public struct LineSegment
    {
        public readonly Vector2 StartPos;
        public readonly Vector2 EndPos;
        /// <summary>
        /// A Vector2 representing the segment locally, where start pos is (0,0).
        /// </summary>
        public Vector2 LocalVector { get => EndPos - StartPos; }
        public float SegmentLength { get => LocalVector.Length(); }
        public LineSegment(Vector2 startPos, Vector2 endPos)
        {
            StartPos = startPos;
            EndPos = endPos;
        }
    }
}
