using Courier.Engine.Nodes;
using Courier.Game.EventData;
using Courier.Game.PlayerCode;
using Microsoft.Xna.Framework;
using PubSub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Game.MissileCode
{
    public class MissileManager : Node
    {
        private const int WantedLevelToStartFiringMissiles = 5;
        private readonly Vector2 firstMissileSpawnPositionRelativeToPlayer = new Vector2(-300, -400);
        private readonly Vector2 secondMissileSpawnPositionRelativeToPlayer = new Vector2(-100, -400);

        private readonly Hub hub;

        private readonly Player player;
        private readonly List<Missile> missiles = new List<Missile>();

        /// <summary>
        /// Keeps track of whether to fire missiles or not.
        /// </summary>
        private bool shouldFireMissiles = false;

        public MissileManager(Node parent, Player player) : base(parent)
        {
            this.player = player;
            this.hub = Hub.Default;
            hub.Subscribe<UpdateWantedLevelEvent>(OnUpdateWantedLevel);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // Remove any Missiles that have been marked as ShouldDestroy
            var missilesToRemove = missiles.Where(missile => missile.ShouldDestroy);
            Children.RemoveAll(c => missilesToRemove.Contains(c));
            missiles.RemoveAll(p => missilesToRemove.Contains(p));

            // If there are no missiles currently active, create more.
            if (missiles.Count() == 0 && shouldFireMissiles)
            {
                FireMissiles();
            }
        }

        /// <summary>
        /// Creates new Missiles.
        /// </summary>
        private void FireMissiles()
        {
            var firstMissile = new Missile(this, player);
            firstMissile.LocalPosition = player.GlobalPosition + firstMissileSpawnPositionRelativeToPlayer;
            Children.Add(firstMissile);
            missiles.Add(firstMissile);

            var secondMissile = new Missile(this, player);
            secondMissile.LocalPosition = player.GlobalPosition + secondMissileSpawnPositionRelativeToPlayer;
            Children.Add(secondMissile);
            missiles.Add(secondMissile);
        }

        private void OnUpdateWantedLevel(UpdateWantedLevelEvent eventData)
        {
            if (eventData.NewWantedLevel >= WantedLevelToStartFiringMissiles)
            {
                shouldFireMissiles = true; 
            }
        }
    }
}
