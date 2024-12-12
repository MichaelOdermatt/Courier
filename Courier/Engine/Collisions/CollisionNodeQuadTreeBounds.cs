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
        public float GetBottom(ICollisionNode node) => node.GetBottom();
        public float GetTop(ICollisionNode node) => node.GetTop();
        public float GetLeft(ICollisionNode node) => node.GetLeft();
        public float GetRight(ICollisionNode node) => node.GetRight();
    }
}
