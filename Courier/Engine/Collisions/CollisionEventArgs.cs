using Courier.Engine.Collisions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Engine.Collisions
{
    public class CollisionEventArgs : EventArgs
    {
        public ICollisionNode collisionNode { get; set; }
    }
}
