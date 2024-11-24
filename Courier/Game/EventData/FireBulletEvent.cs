using Courier.Game.BulletCode;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Game.EventData
{
    public class FireBulletEvent
    {
        public Vector2 InitialPosition { get; set; }
        public Vector2 Direction { get; set; }
        public BulletType BulletType { get; set; }
    }
}
