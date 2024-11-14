using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Courier.Game.TownCode;

namespace Courier.Game.PlayerCode
{
    public class PlayerPackageDelivery
    {
        private readonly TownManager townManager;
        private readonly Player player;

        /// <summary>
        /// The range that the Player must be in of a Town to successfully deliver a package.
        /// </summary>
        private const float SuccessfulDeliveryTownRange = 150f;

        public PlayerPackageDelivery(TownManager townManager, Player player)
        {
            this.townManager = townManager;
            this.player = player;
        }

        /// <summary>
        /// Attempts to deliver a package to the nearest Town to the Player.
        /// </summary>
        public void AttemptDeliverPackage()
        {
            Town nearestTown = townManager.GetNearestTown(player.GlobalPosition);
            float distanceToNearestTown = (nearestTown.GlobalPosition - player.GlobalPosition).Length();

            if (distanceToNearestTown <= SuccessfulDeliveryTownRange)
            {
                nearestTown.DeliverPackage();
                return;
            }
        }
    }
}
