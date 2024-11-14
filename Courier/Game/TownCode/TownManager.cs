using Courier.Engine.Nodes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Courier.Game.TownCode
{
    public class TownManager : Node
    {
        private readonly List<Town> towns;

        public TownManager(Node parent, List<Town> towns) : base(parent)
        {
            this.towns = towns;
            Children.AddRange(towns);
        }

        /// <summary>
        /// Returns the town nearest to the given position.
        /// </summary>
        public Town GetNearestTown(Vector2 position)
        {
            Town nearestTown = towns.First();
            float distanceToNearestTown = (nearestTown.GlobalPosition - position).Length();

            foreach (Town town in towns)
            {
                float distanceTo = (town.GlobalPosition - position).Length();
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
