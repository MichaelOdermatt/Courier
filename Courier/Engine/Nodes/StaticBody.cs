﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courier.Engine.Collisions;

namespace Courier.Engine.Nodes
{
    public class StaticBody : Node, ICollisionNode
    {
        public ICollisionShape CollisionShape { get; set; }

        public StaticBody(Node parent, ICollisionShape collisionShape) : base(parent)
        {
            CollisionShape = collisionShape;
            CollisionShape.Parent = this;
        }

    }
}