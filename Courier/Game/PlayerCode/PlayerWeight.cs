using Courier.Game.EventData;
using Courier.Engine.PubSubCustom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Game.PlayerCode
{
    public class PlayerWeight
    {
        private readonly Hub hub; 

        private const float InitialWeight = 500f;
        private float currentWeight;

        public PlayerWeight(Hub hub)
        {
            this.hub = hub;
            currentWeight = InitialWeight;
        }

        /// <summary>
        /// Reduces the weight value by the given amount.
        /// </summary>
        public void ReduceWeight(float reduceWeightAmount)
        {
            currentWeight -= reduceWeightAmount;
            hub.Publish(new UpdateWeightEvent
            {
                NewWeightValue = currentWeight,
            });
        }
    }
}