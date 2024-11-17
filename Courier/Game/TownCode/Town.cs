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
        public Sprite sprite;
        public readonly Vector2 spriteOffset;

        private bool hasParcelBeenDelivered;

        public Town(Node parent) : base(parent)
        {
            spriteOffset = new Vector2(0, -23);
            sprite = new Sprite(this, "Town")
            {
                Offset = spriteOffset,
            };
            Children.Add(sprite);
        }

        /// <summary>
        /// Attempts to deliver the parcel to the town. Returns a bool representing if the delivery was successful or not.
        /// </summary>
        public bool AttemptDeliverPackage()
        {
            if (hasParcelBeenDelivered)
            {
                return false;
            }

            // Update the town sprite
            Children.Remove(sprite);
            sprite = new Sprite(this, "TownCelebrate")
            {
                Offset = spriteOffset,
            };
            Children.Add(sprite);

            hasParcelBeenDelivered = true;
            return true;
        }
    }
}
