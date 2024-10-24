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

        private Action CreateBulletAction;

        public Gunner(Node parent, Node playerNode) : base(parent)
        {
            this.playerNode = playerNode;
            CreateBulletAction = CreateBullet;

            sprite = new Sprite(this, "Gunner");
            Children.Add(sprite);

            shootTimer = new GameTimer(5f, CreateBulletAction);
            shootTimer.Loop = true;
            shootTimer.Start();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            var vectorToPlayer = GlobalPosition - playerNode.GlobalPosition;

            // Rotate the sprite to face the Player.
            var angleToPlayer = MathF.Atan2(vectorToPlayer.Y, vectorToPlayer.X);
            sprite.Rotation = angleToPlayer;

            // Tick the shootTimer.
            shootTimer.Tick(gameTime);
        }

        public void CreateBullet()
        {
            var vectorToPlayer = playerNode.GlobalPosition - GlobalPosition;
            vectorToPlayer.Normalize();

            var bullet = new Bullet(this, GlobalPosition, vectorToPlayer);
            Children.Add(bullet);
        }
    }
}
