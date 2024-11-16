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
        private readonly PlayerFuel playerFuel;

        /// <summary>
        /// The range that the Player must be in of a Town to successfully deliver a package.
        /// </summary>
        private const float SuccessfulDeliveryTownRange = 150f;
        private const float FuelToAddOnSuccessfulDelivery = 20f;

        public PlayerPackageDelivery(TownManager townManager, Player player, PlayerFuel playerFuel)
        {
            this.townManager = townManager;
            this.player = player;
            this.playerFuel = playerFuel;
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
                var wasDeliverySuccessful = nearestTown.AttemptDeliverPackage();
                if (wasDeliverySuccessful)
                {
                    playerFuel.AddFuel(FuelToAddOnSuccessfulDelivery);
                }
                return;
            }
        }
    }
}
