﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Engine.Collisions
{
    public enum CollisionNodeType
    {
        SmallBullet,
        LargeBullet,
        Missile,
        Ground,
        Player,
        Parcel,
        Town,
        Enemy,
        RefuelPickup,
    }
}
