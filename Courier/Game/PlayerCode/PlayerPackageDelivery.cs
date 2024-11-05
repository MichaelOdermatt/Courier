using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Game.PlayerCode
{
    public class PlayerPackageDelivery
    {
        private readonly Player player;
        /// <summary>
        /// A reference to all the Towns in the current level.
        /// </summary>
        private readonly List<Town> levelTowns;

        /// <summary>
        /// The range that the Player must be in of a Town to successfully deliver a package.
        /// </summary>
        private const float SuccessfulDeliveryTownRange = 200f;

        public PlayerPackageDelivery(List<Town> towns, Player player)
        {
            this.player = player;
            levelTowns = towns;
        }

        /// <summary>
        /// Attempts to deliver a package to the nearest Town to the Player.
        /// </summary>
        public void AttemptDeliverPackage()
        {
            Town nearestTown = GetNearestTown();
            float distanceToNearestTown = (nearestTown.GlobalPosition - player.GlobalPosition).Length();

            if (distanceToNearestTown <= SuccessfulDeliveryTownRange)
            {
                nearestTown.DeliverPackage();
                return;
            }
        }

        /// <summary>
        /// Returns the closest town to the player from the levelTowns list.
        /// </summary>
        private Town GetNearestTown()
        {
            Town nearestTown = levelTowns.First();
            float distanceToNearestTown = (nearestTown.GlobalPosition - player.GlobalPosition).Length();

            foreach (Town town in levelTowns)
            {
                float distanceTo = (town.GlobalPosition - player.GlobalPosition).Length();
                if (distanceTo <= distanceToNearestTown)
                {
                    nearestTown = town;
                    distanceToNearestTown = distanceTo;
                }
            }

            return nearestTown;
        }
    }
}
