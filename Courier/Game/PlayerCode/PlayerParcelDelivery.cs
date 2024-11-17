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
        private readonly PlayerFuel playerFuel;

        private const float FuelToAddOnSuccessfulDelivery = 20f;

        public PlayerParcelDelivery(Player player, PlayerFuel playerFuel, Hub hub)
        {
            this.hub = hub;
            this.player = player;
            this.playerFuel = playerFuel;
        }

        /// <summary>
        /// Publishes an event to drop a parcel.
        /// </summary>
        public void DropParcel()
        {
            hub.Publish(new DropParcelEvent { Position = player.GlobalPosition });
        }
    }
}
