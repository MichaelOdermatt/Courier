using Courier.Engine;
using Courier.Engine.Nodes;
using Courier.Game.EventData;
using Microsoft.Xna.Framework;
using Courier.Engine.PubSubCustom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Game.ParcelCode
{
    public class ParcelManager : Node
    {
        private readonly Hub hub;

        private readonly List<Parcel> parcels = new List<Parcel>();
        private readonly List<GameTimer> parcelTimers = new List<GameTimer>();

        private const float ParcelActiveTime = 5f;

        public ParcelManager(Node parent) : base(parent)
        {
            hub = Hub.Default;
            hub.Subscribe<DropParcelEvent>(this, OnDropParcel);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // Remove any parcels that have been marked as ShouldDestroy
            var parcelsToRemove = parcels.Where(parcel => parcel.ShouldDestroy);
            Children.RemoveAll(c => parcelsToRemove.Contains(c));
            parcels.RemoveAll(p => parcelsToRemove.Contains(p));

            parcelTimers.ForEach(timer => timer.Tick(gameTime));
            // Remove any parcel timers from the list that are no longer running.
            parcelTimers.RemoveAll(p => !p.IsRunning);
        }

        /// <summary>
        /// Creates a new Parcel at the position in the given event data.
        /// Also creates a game timer to destroy the parcel after a set amount of time.
        /// </summary>
        private void OnDropParcel(DropParcelEvent eventData)
        {
            var newParcel = new Parcel(this);
            newParcel.LocalPosition = eventData.Position;
            Children.Add(newParcel);
            parcels.Add(newParcel);

            var newTimer = new GameTimer(ParcelActiveTime, () => DestroyParcel(newParcel));
            newTimer.Start();
            parcelTimers.Add(newTimer);
        }

        /// <summary>
        /// Function to destroy the given parcel.
        /// </summary>
        private void DestroyParcel(Parcel parcel)
        {
            Children.Remove(parcel);
            parcels.Remove(parcel);
        }
    }
}
