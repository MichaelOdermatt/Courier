using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courier.Engine.Collisions;

namespace Courier.Engine
{
    public class StaticBody : Node, ICollisionNode
    {
        public ICollisionShape CollisionShape { get; set; }

        public StaticBody(ICollisionShape collisionShape)
        {
            CollisionShape = collisionShape;
        }

    }
}
