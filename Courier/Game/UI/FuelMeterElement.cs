using Courier.Engine.Nodes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PubSub;
using Courier.Game.EventData;

namespace Courier.Game.UI
{
    public class FuelMeterElement : Node
    {
        private readonly Hub hub;
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
            meterFillSprite.Scale = new Vector2(maxFillWidth, 1);
            Children.Add(meterFillSprite);

            meterOutlineSprite = new Sprite(this, "FuelMeterOutline");
            meterOutlineSprite.IsWorldSpaceSprite = false;
            meterOutlineSprite.Origin = Vector2.Zero;
            Children.Add(meterOutlineSprite);

            hub = Hub.Default;
            hub.Subscribe<UpdateFuelEvent>(this, OnUpdateFuelEvent);
        }

        /// <summary>
        /// Updates the fill amount displayed by the meter.
        /// </summary>
        private void OnUpdateFuelEvent(UpdateFuelEvent eventData)
        {
            var clampedFillAmount = Math.Clamp(eventData.NewFuelLevel, 0, 1);
            var fillAmountWidth = (clampedFillAmount * maxFillWidth) - minFillWidth;
            meterFillSprite.Scale = new Vector2(fillAmountWidth, 1);
        }
    }
}
