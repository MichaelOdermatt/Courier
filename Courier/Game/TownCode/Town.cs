using Courier.Engine.Nodes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Game.TownCode
{
    public class Town : Node
    {
        public readonly Sprite sprite;

        private bool hasPackageBeenDelivered;

        public Town(Node parent) : base(parent)
        {
            sprite = new Sprite(this, "Town")
            {
                Offset = new Vector2(0, -11)
            };
            Children.Add(sprite);
        }

        public void DeliverPackage()
        {
            hasPackageBeenDelivered = true;
        }
    }
}
