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
    }
}
