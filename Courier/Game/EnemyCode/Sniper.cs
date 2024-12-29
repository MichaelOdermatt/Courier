using System;
using Courier.Engine.Nodes;
using Courier.Game.PlayerCode;
using Microsoft.Xna.Framework;

namespace Courier.Game.EnemyCode
{
    public class Sniper : Node
    {
        private const float sniperShootRange = 500f;
        private const float aimLineThickness = 1f;
        private Sprite sniperSprite;
        private Sprite aimSprite;
        private Player player;

        public Sniper(Node parent, Player player) : base(parent)
        {
            this.player = player;            

            sniperSprite = new Sprite(this, "Gunner");
            Children.Add(sniperSprite);

            aimSprite = new Sprite(this, "LineTextureRed");
            aimSprite.NeverCull = true;
            Children.Add(aimSprite);
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

            var vectorToPlayer = player.GlobalPosition - GlobalPosition;
            if (vectorToPlayer.Length() > sniperShootRange) {
                aimSprite.Visible = false;
                return;
            }
            
            aimSprite.Visible = true;
            var angleToPoint = MathF.Atan2(vectorToPlayer.Y, vectorToPlayer.X);
            aimSprite.Rotation = angleToPoint;
            aimSprite.Scale = new Vector2(vectorToPlayer.Length(), aimLineThickness);
        }
    }
}