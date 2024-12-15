using Courier.Engine.Collisions;
using Courier.Engine.Nodes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Game.BulletCode
{
    public class LargeBullet : BulletBase
    {
        public LargeBullet(Node parent, float speed, Vector2 initialPos, Vector2 direction) : base(parent, "BulletLarge", 5f, CollisionNodeType.LargeBullet, speed, initialPos, direction)
        {
        }
    }
}
