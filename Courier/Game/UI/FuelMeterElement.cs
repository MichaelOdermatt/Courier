using Courier.Engine.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Game.UI
{
    public class FuelMeterElement : Node
    {
        /// <summary>
        /// This Sprite will display the outline of the FuelMeter.
        /// </summary>
        private readonly Sprite meterOutlineSprite;
        /// <summary>
        /// This Sprite will display the inside level of the FuelMeter.
        /// </summary>
        private readonly Sprite meterFillSprite;

        public FuelMeterElement(Node parent) : base(parent)
        {
            meterOutlineSprite = new Sprite(this, "FuelMeterOutline");
            meterOutlineSprite.CullIfNotInView = false;
            Children.Add(meterOutlineSprite);

            meterFillSprite = new Sprite(this, "FuelMeterFill");
            meterFillSprite.CullIfNotInView = false;
            Children.Add(meterFillSprite);
        }
    }
}
