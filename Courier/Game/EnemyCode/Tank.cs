using Courier.Engine;
using Courier.Engine.Nodes;
using Courier.Game.BulletCode;
using Courier.Game.PlayerCode;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Game.EnemyCode
{
    public class Tank : EnemyBase
    {
        public Tank(Node parent, Player player, BulletPool bulletPool) : base(parent, player, bulletPool, 0.95f, 700f, "Tank")
        {
        }

        /// <summary>
        /// Attempts to create a Bullet and fires it directly up.
        /// </summary>
        public override void TryCreateBullet()
        {
            var vectorToPlayer = player.GlobalPosition - GlobalPosition;
            // Don't shoot if the player is out of range.
            if (vectorToPlayer.Length() > shootRange)
            {
                return;
            }

            bulletPool.ActivateBullet(GlobalPosition, -Vector2.UnitY, BulletType.Large);
        }
    }
}
