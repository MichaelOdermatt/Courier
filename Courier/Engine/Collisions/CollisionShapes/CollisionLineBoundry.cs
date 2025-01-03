using Courier.Engine.Collisions.Interfaces;
using Courier.Engine.Nodes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Engine.Collisions.CollisionShapes
{
    public class CollisionLineBoundry : ICollisionShape
    {
        private const float LineBoundryHeight = -1000f;
        private const float LineBoundryWidthBufferZone = 250f;

        /// <summary>
        /// The direction in which the collision shape should bound off.
        /// </summary>
        public CollisionBoundryDirection Direction { get; private set; }

        public CollisionLineBoundry(CollisionBoundryDirection direction)
        {
            Direction = direction;
        }

        public CollisionBoundingRect GetBoundingRect(Matrix transformMatrix)
        {
            float rightValue;
            float topValue;
            switch (Direction) {
                case CollisionBoundryDirection.Right:
                    // Give the line boundry a specified width, so collisions can be detected up to that amount of pixels past it.
                    rightValue = LineBoundryWidthBufferZone;
                    // Give the line boundry a specified height, so collisions can be detected up to that amount of pixels above it.
                    topValue = LineBoundryHeight;
                    break;
                default:
                    throw new NotImplementedException();
            }

            return new CollisionBoundingRect 
            {
                Left = 0,
                Right = rightValue,
                Top = topValue,
                Bottom = 0,
            };
        }
    }
}
