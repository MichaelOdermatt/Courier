using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Courier.Game.TownCode;
using PubSub;
using Courier.Game.EventData;

namespace Courier.Game.PlayerCode
{
    public class PlayerParcelDelivery
    {
        private readonly Hub hub; 

        private readonly Player player;
        private readonly PlayerWeight playerWeight;

        private float currentParcelDropCooldown = 0f;
        private bool canDropParcel = true;

        private const float FuelToAddOnSuccessfulDelivery = 20f;
        private const float ParcelDropCooldownDuration = 0.1f;

        public PlayerParcelDelivery(Player player, Hub hub)
        {
            this.hub = hub;
            this.player = player;
            playerWeight = new PlayerWeight(hub);
        }

        /// <summary>
        /// Publishes an event to drop a parcel.
        /// </summary>
        public void DropParcel()
        {
            if (!canDropParcel)
            {
                return;
            }

            hub.Publish(new DropParcelEvent { Position = player.GlobalPosition });
            // TODO get this value from somewhere
            playerWeight.ReduceWeight(5f);
            canDropParcel = false;
        }

        /// <summary>
        /// Updates the parcel drop cooldown timer.
        /// </summary>
        /// <param name="gameTime"></param>
        public void UpdateParcelDropCooldown(GameTime gameTime)
        {
            if (currentParcelDropCooldown >= ParcelDropCooldownDuration)
            {
                canDropParcel = true;
                currentParcelDropCooldown = 0;
            } else
            {
                var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                currentParcelDropCooldown += deltaTime;
            }
        }
    }
}
