using Courier.Engine;
using Courier.Engine.Nodes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Courier.Game
{
    public class Gunner : Node
    {
        private readonly Node playerNode;
        private readonly Sprite sprite;

        private readonly GameTimer shootTimer;
        private readonly float timeBetweenBullets = 0.75f;

        private Action CreateBulletAction;

        public Gunner(Node parent, Node playerNode) : base(parent)
        {
            this.playerNode = playerNode;
            CreateBulletAction = CreateBullet;

            sprite = new Sprite(this, "Gunner");
            Children.Add(sprite);

            // Create the shoot timer.
            shootTimer = new GameTimer(timeBetweenBullets, CreateBulletAction);
            shootTimer.Loop = true;
            shootTimer.Start();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // Rotate the sprite to face the Player.
            var vectorToPlayer = GlobalPosition - playerNode.GlobalPosition;
            var angleToPlayer = MathF.Atan2(vectorToPlayer.Y, vectorToPlayer.X);
            sprite.Rotation = angleToPlayer;

            // Tick the shootTimer.
            shootTimer.Tick(gameTime);
        }

        /// <summary>
        /// Creates a Bullet and fires it in the direction of the player.
        /// </summary>
        public void CreateBullet()
        {
            var vectorToPlayer = playerNode.GlobalPosition - GlobalPosition;
            vectorToPlayer.Normalize();

            var bullet = new Bullet(this, GlobalPosition, vectorToPlayer);
            Children.Add(bullet);
        }
    }
}
