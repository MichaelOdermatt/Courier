using Courier.Engine.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Game.BulletCode
{
    public class SmallBullet : BulletBase
    {
        public SmallBullet(Node parent) : base(parent, "BulletSmall", 2f, 250f)
        {
        }
    }
}
