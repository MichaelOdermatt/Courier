﻿using Courier.Engine.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Game.BulletCode
{
    public class LargeBullet : BulletBase
    {
        public LargeBullet(Node parent) : base(parent, "BulletLarge", 5f, 150f)
        {
        }
    }
}