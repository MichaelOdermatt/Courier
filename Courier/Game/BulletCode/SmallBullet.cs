﻿using Courier.Engine.Collisions;
using Courier.Engine.Nodes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Game.BulletCode
{
    public class SmallBullet : BulletBase
    {
        public SmallBullet(Node parent, Vector2 initialPos, Vector2 direction) : base(parent, "BulletSmall", 2f, 250f, CollisionNodeType.SmallBullet, initialPos, direction)
        {
        }
    }
}
