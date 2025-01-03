using Auios.QuadTree;
using Courier.Engine.Collisions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Engine.Collisions
{
    public class CollisionNodeQuadTreeBounds : IQuadTreeObjectBounds<ICollisionNode>
    {
        public float GetBottom(ICollisionNode node) => node.GetCollisionBoundingRect().Bottom;
        public float GetTop(ICollisionNode node) => node.GetCollisionBoundingRect().Top;
        public float GetLeft(ICollisionNode node) => node.GetCollisionBoundingRect().Left;
        public float GetRight(ICollisionNode node) => node.GetCollisionBoundingRect().Right;
    }
}
