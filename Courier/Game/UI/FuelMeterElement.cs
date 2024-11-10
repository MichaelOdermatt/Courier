using Courier.Engine.Nodes;
using Microsoft.Xna.Framework;
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

        private readonly float minFillWidth = 0f;
        private readonly float maxFillWidth = 151f;

        public FuelMeterElement(Node parent) : base(parent)
        {
            meterFillSprite = new Sprite(this, "FuelMeterFill");
            meterFillSprite.IsWorldSpaceSprite = false;
            meterFillSprite.Offset = new Vector2(2, 2);
            meterFillSprite.Origin = Vector2.Zero;
            Children.Add(meterFillSprite);

            meterOutlineSprite = new Sprite(this, "FuelMeterOutline");
            meterOutlineSprite.IsWorldSpaceSprite = false;
            meterOutlineSprite.Origin = Vector2.Zero;
            Children.Add(meterOutlineSprite);
        }

        /// <summary>
        /// Updates the fill amount displayed by the meter.
        /// </summary>
        /// <param name="fillAmount">A value between 0 and 1.0, the fill amount the meter should display as.</param>
        public void UpdateMeterFill(float fillAmount)
        {
            var clampedFillAmount = Math.Clamp(fillAmount, 0, 1);
            var fillAmountWidth = (clampedFillAmount * maxFillWidth) - minFillWidth;
            meterFillSprite.Scale = new Vector2(fillAmountWidth, 1);
        }
    }
}
